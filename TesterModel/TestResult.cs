using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TesterModel
{
    public class TestResult:ICloneable
    {
        public string UserName { get; set; }
        public string TestID { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsDone { get; set; }
        private List<Answer> answers = new List<Answer>();
        [XmlArray("Answers")]
        public List<Answer> Answers { get { return answers; } }

        public void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }

        public void CheckAnswers(Test test)
        {
            foreach (Answer a in answers)
            {
                try
                {
                    a.IsCorrect = (from q in test.Questions
                                   where q.QuestionID == a.QuestionID
                                   select q).First().NumCorrectAnswer == a.NumOptionAnswer;
                }
                catch
                { }
            }
        }

        public void Reset(string userName, string testID)
        {
            BeginTime = DateTime.Now;
            this.TestID = testID;
            this.UserName = userName;
            Answers.Clear();
            IsDone = false;
        }

        public object Clone()
        {
            TestResult temp = new TestResult();
            temp.BeginTime = BeginTime;
            temp.FinishTime = FinishTime;
            temp.IsDone = IsDone;
            temp.UserName = UserName;
            foreach (Answer item in answers)
            {
                Answer tempAnswer = new Answer();
                tempAnswer.IsCorrect = item.IsCorrect;
                tempAnswer.NumOptionAnswer = item.NumOptionAnswer;
                tempAnswer.QuestionID = item.QuestionID;
                temp.Answers.Add(tempAnswer);
            }
            return temp;
        }
    }

    public class Answer
    {
        public string QuestionID { get; set; }
        public int NumOptionAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
