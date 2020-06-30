using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TesterModel
{  

    public class Test
    {
        public string TestID { get; set; }
        public string Name { get; set; }
        private List<Question> questions = new List<Question>();
        [XmlArray("Questions")]
        public List<Question> Questions { get { return questions; } }
        public int TimeSec { get; set; }

        public Test()
        {
            TestID = Guid.NewGuid().ToString();
            Name = string.Empty;
            TimeSec = 0;
        }

        public Test(string name, int timeSec, params Question[] questions)
        {
            TestID = Guid.NewGuid().ToString();
            Name = name;
            TimeSec = timeSec;
            foreach (Question q in questions)
            {
                this.questions.Add(q);
            }
        }
    }

    public class Question
    {
        public string QuestionID { get; set; }
        public string Text { get; set; }
        private List<string> optionAnswers = new List<string>();
        [XmlArray("Answers")]
        public List<string> OptionAnswers { get { return optionAnswers; } }
        public int NumCorrectAnswer { get; set; }

        public Question()
        {
            QuestionID = Guid.NewGuid().ToString();
            Text = string.Empty;
            NumCorrectAnswer = 0;
        }

        public Question(string text, int numCorrectAnswer, params string[] answers)
        {
            QuestionID = Guid.NewGuid().ToString();

            Text = text;
            if (numCorrectAnswer >= answers.Length)
            {
                NumCorrectAnswer = answers.Length - 1;
            }
            else
            {
                NumCorrectAnswer = numCorrectAnswer;
            }
            foreach (string a in answers)
            {
                this.optionAnswers.Add(a);
            }
        }
    }
}
