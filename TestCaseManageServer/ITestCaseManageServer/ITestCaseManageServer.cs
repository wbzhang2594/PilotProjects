using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager
{
    public interface ITestCaseManageServer
    {
        bool RequestRunThisCase(string caseName);

        //void FinishedOneCase(string caseName, bool result);
    }
}
