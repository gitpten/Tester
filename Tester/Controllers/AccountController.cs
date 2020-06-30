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
    public class AccountController : Controller
    {
        private IResultRepository resultRepository;
        private ITestRepository testRepository;

        private const string PathTests = @"C:/Tests.xml";
        private const string PathTestResults = @"C:/TestResults.xml";

        public AccountController()
        {
            testRepository = new XMLTestRepository(PathTests);
            resultRepository = new XMLResultRepository(PathTestResults);
        }

        [HttpGet]
        public ViewResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string UserName, TestResult testResult)
        {
            if (testResult.TestID != "")
            {
                resultRepository.AddTestResult(testResult);
            }

            TestResult res = resultRepository.LastTestThisName(UserName);

            Test temp;
            if (res == null || res.IsDone)
            {
                try
                {
                    temp = res == null ? testRepository.Tests[0] : testRepository.Tests[testRepository.Tests.FindIndex(t => t.TestID == res.TestID) + 1];
                }
                catch (IndexOutOfRangeException)
                {
                    return RedirectToAction("AllResults", "Results");
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(e.Message);
                }

                testResult.Reset(UserName, temp.TestID);
            }
            else
            {
                testResult = (TestResult)res.Clone();
            }

            

            FormsAuthentication.SetAuthCookie(UserName, false);

            return RedirectToAction("Index", "Test");
        }

        public ActionResult LogOut(TestResult testResult)
        {
            if (testResult.UserName != null)
            {
                resultRepository.AddTestResult(testResult);
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Login");
        }
    }
}
