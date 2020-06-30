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
    public class XMLTestRepository: ITestRepository
    {
        List<Test> tests;

        public XMLTestRepository(string filename)
        {
            XmlSerializer s = new XmlSerializer(typeof(List<Test>));
            Stream x = new FileStream(filename, FileMode.Open);
            List<Test> tests = new List<Test>();
            tests = (List<Test>)s.Deserialize(x);
            this.tests = tests;
            x.Close();
        }

        public List<Test> Tests
        {
            get { return tests; }
        }
    }
}
