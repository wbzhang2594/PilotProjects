using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestCaseManager;

namespace Client
{
    public class TestAgent
    {
        int agentID;
        public TestAgent(int id)
        {
            agentID = id;
        }

        public void RunCases(bool NeedMockServer, int CountPerSecond)
        {
            ITestCaseManageServer Server = ServerFactory.GetServer(NeedMockServer);

            List<string> caseNameList = GetCaseNameList();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach (string casename in caseNameList)
            {
                if (Server.RequestRunThisCase(casename))
                {
                    Thread.Sleep(1000 / CountPerSecond);
                    //Server.FinishedOneCase(casename, true);
                }
            }

            sw.Stop();
            System.Console.WriteLine(string.Format("TestAgent {0} run {1} ms.", agentID, sw.ElapsedMilliseconds));
        }

        private List<string> GetCaseNameList()
        {
            List<string> result = new List<string>();
            string filePath = MockBuild.GetTestCaseFilePath();

            using (StreamReader fileReader = new StreamReader(filePath))
            {
                string line = fileReader.ReadLine();
                result.Add(line);
            }

            return result;
        }
    }
}
