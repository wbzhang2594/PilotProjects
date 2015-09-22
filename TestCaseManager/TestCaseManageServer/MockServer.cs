using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager
{
    public class MockServer : ITestCaseManageServer
    {
        static object pLock = new object();
        List<string> TestCaseList = new List<string>();
        List<string> TestCaseAssigned = new List<string>();
        Dictionary<string, bool> TestResult = new Dictionary<string, bool>();


        public bool RequestRunThisCase(string caseName)
        {
            lock (pLock)
            {
                if (TestCaseAssigned.Contains(caseName) == false)
                {
                    TestCaseAssigned.Add(caseName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //public List<string> GetCaseNameList()
        //{
        //    List<string> result = new List<string>();
        //    string filePath = MockBuild.GetTestCaseFilePath();

        //    using (StreamReader fileReader = new StreamReader(filePath))
        //    {
        //        string line = fileReader.ReadLine();
        //        result.Add(line);
        //    }

        //    return result;
        //}


        public void FinishedOneCase(string caseName, bool result)
        {
            lock (pLock)
            {
                if (TestResult.ContainsKey(caseName))
                {
                    throw new Exception(string.Format("Duplicate assignment of the case {0}.", caseName));
                }
                else
                {
                    TestResult.Add(caseName, result);
                }
            }
        }
    }
}
