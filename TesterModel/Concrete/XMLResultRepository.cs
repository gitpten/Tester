using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TesterModel.Abstract;

namespace TesterModel.Concrete
{
    public class XMLResultRepository : IResultRepository
    {
        List<TestResult> results;
        private string fileName;

        private const string expNoFoundFileResult = "Не найден файл с результатами";

        public XMLResultRepository(string fileName)
        {
            this.fileName = fileName;
            XmlSerializer s = new XmlSerializer(typeof(List<TestResult>));
            List<TestResult> results = new List<TestResult>();

            if (!File.Exists(fileName))
            {
                throw new InvalidProgramException(expNoFoundFileResult);
            }
            else
            {
                Stream x = new FileStream(fileName, FileMode.Open);
                try
                {
                    results = (List<TestResult>)s.Deserialize(x);
                }
                catch
                {

                }
                finally
                {
                    x.Dispose();
                }
            }
            this.results = results;
        }

        public List<TestResult> TestResults
        {
            get { return results; }
        }


        public void AddAnswerToTestResult(Answer answer, Test test)
        {
            TestResult tstres = (from tr in results
                                 where tr.TestID == test.TestID
                                 select tr).First();

            tstres.AddAnswer(answer);

            this.UpdateXML();
        }


        public void AddTestResult(TestResult testResult)
        {
            TestResult tstres = results.FirstOrDefault(r => r.UserName == testResult.UserName && r.TestID == testResult.TestID);

            if (tstres != null)
            {
                results.Remove(tstres);
            }

            results.Add(testResult);

            this.UpdateXML();
        }

        private void UpdateXML()
        {
            if (results != null)
            {
                XmlSerializer s = new XmlSerializer(typeof(List<TestResult>));

                Stream w = new FileStream(fileName, FileMode.Create);

                s.Serialize(w, results);
                w.Close();
            }
        }

        public TestResult LastTestThisName(string name)
        {
            return results.LastOrDefault(l => l.UserName == name);
        }
    }
}
