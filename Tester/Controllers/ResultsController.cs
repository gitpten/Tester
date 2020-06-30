using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesterModel;
using TesterModel.Abstract;
using TesterModel.Concrete;

namespace Tester.Controllers
{
    public class ResultsController : Controller
    {
        public int PageSize = 10;

        private ITestRepository testRepository;
        private IResultRepository resultRepository;

        private const string PathTests = @"C:/Tests.xml";
        private const string PathTestResults = @"C:/TestResults.xml";

        public ResultsController()
        {
            testRepository = new XMLTestRepository(PathTests);
            resultRepository = new XMLResultRepository(PathTestResults);
        }

        public ViewResult AllResults(int page, string SortField)
        {
            var resIsDone = from r in resultRepository.TestResults
                            where r.IsDone
                            select r;
            List<TestResult> testResultForView = new List<TestResult>(resIsDone);

            int numResults = testResultForView.Count;
            ViewData["TotalPage"] = (int)Math.Ceiling((double)numResults / PageSize);
            ViewData["CurrentPage"] = page;
            ViewData["SortField"] = SortField;
            
            switch (SortField)
            {
                case "UserName":
                    testResultForView.Sort((m1, m2) => m1.UserName.CompareTo(m2.UserName));
                    break;
                case "Date":
                    testResultForView.Sort((m1, m2) => m1.BeginTime.CompareTo(m2.BeginTime));
                    break;
                case "Time":
                    testResultForView.Sort((m1, m2) => (m1.FinishTime - m1.BeginTime).CompareTo(m2.FinishTime - m2.BeginTime));
                    break;
                case "Result":
                    testResultForView.Sort((m1, m2) => ((double)(m1.Answers.Count(a => a.IsCorrect))).CompareTo((double)(m2.Answers.Count(a => a.IsCorrect))));
                    break;
                default:
                    break;
            }

            ViewData["Tests"] = testRepository.Tests;
            return View(testResultForView.Skip((page - 1) * PageSize).Take(PageSize).ToList());
        }
    }
}
