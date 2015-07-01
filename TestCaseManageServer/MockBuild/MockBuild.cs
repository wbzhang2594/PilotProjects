using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager
{
    public class MockBuild
    {
        static string testCaseFilePath = Path.GetFullPath(@"TestCases.txt");

        public static string GetTestCaseFilePath()
        {

            return testCaseFilePath;
        }
    }
}
