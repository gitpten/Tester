using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterModel.Abstract
{
    public interface IResultRepository
    {
        List<TestResult> TestResults { get; }
        void AddAnswerToTestResult(Answer answer, Test test);
        void AddTestResult (TestResult testResult);
        TestResult LastTestThisName(string name);
    }
}
