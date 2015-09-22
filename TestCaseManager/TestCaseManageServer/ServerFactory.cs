using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager
{
    public class ServerFactory
    {
        static ITestCaseManageServer mockServer = null;
        public static ITestCaseManageServer GetServer(bool NeedMockServer)
        {
            if(NeedMockServer)
            {
                if(mockServer == null)
                {
                    mockServer = new MockServer();
                }
                return mockServer;
            }
            else
            {
                throw new NotSupportedException();
            }

        }
    }
}
