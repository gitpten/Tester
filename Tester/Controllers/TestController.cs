using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TesterModel;
using TesterModel.Abstract;
using TesterModel.Concrete;

namespace Tester.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ITestRepository testRepository;
        private IResultRepository resultRepository;

        private const string PathTests = @"C:/Tests.xml";
        private const string PathTestResults = @"C:/TestResults.xml";

        public TestController()
        {
            testRepository = new XMLTestRepository(PathTests);
            resultRepository = new XMLResultRepository(PathTestResults);
        }

        [HttpGet]
        public ActionResult Index(TestResult testResult)
        {

            Test activeTest = testRepository.Tests.FirstOrDefault(t => t.TestID == testResult.TestID);

            if (testResult.UserName == null || activeTest == null)
                return RedirectToAction("LogIn", "Account");

            if (testResult.Answers.Count >= activeTest.Questions.Count
                || testResult.BeginTime.AddSeconds(activeTest.TimeSec) < DateTime.Now)
            {
                testResult.IsDone = true;
                testResult.FinishTime = DateTime.Now;
                testResult.CheckAnswers(activeTest);
                resultRepository.AddTestResult(testResult);
                return RedirectToAction("Result");
            }

            ViewData["UserName"] = testResult.UserName;
            ViewData["numQuestion"] = testResult.Answers.Count;
            ViewData["Question"] = activeTest.Questions[testResult.Answers.Count];
            ViewData["QuestionID"] = activeTest.Questions[testResult.Answers.Count].QuestionID;
            ViewData["TimeLeft"] = Math.Round(activeTest.TimeSec - (DateTime.Now - testResult.BeginTime).TotalSeconds);
            
            return View(activeTest);
        }

        [HttpPost]
        public ActionResult Index(TestResult testResult, Answer answer)
        {

            Test activeTest = testRepository.Tests.FirstOrDefault(t => t.TestID == testResult.TestID);

            if (testResult.UserName == null || activeTest == null)
                return RedirectToAction("LogIn", "Account");

            testResult.AddAnswer(answer);
            testResult.CheckAnswers(activeTest);

            if (testResult.Answers.Count >= activeTest.Questions.Count
                || testResult.BeginTime.AddSeconds(activeTest.TimeSec) < DateTime.Now)
            {
                testResult.IsDone = true;
                testResult.FinishTime = DateTime.Now;
                testResult.CheckAnswers(activeTest);
                resultRepository.AddTestResult(testResult);
                return RedirectToAction("Result");
            }
            resultRepository.AddTestResult(testResult);

            if (Request.IsAjaxRequest())
                return Json(activeTest.Questions[testResult.Answers.Count]);
            else
                return RedirectToAction("Index");
        }

        public ViewResult Result(TestResult testResult)
        {
            Test activeTest = testRepository.Tests.FirstOrDefault(t => t.TestID == testResult.TestID);
            ViewData["TestName"] = activeTest.Name;
            ViewData["Time"] = (testResult.FinishTime - testResult.BeginTime).ToString(@"mm\:ss");
            ViewData["ResultTest"] = Math.Round((double)(testResult.Answers.Count(a => a.IsCorrect)) / activeTest.Questions.Count * 100);
            return View();
        }


    }
}
