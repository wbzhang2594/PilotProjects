using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegexTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> whiteList = new List<string>()
            {
                "R12.00.00",
                "11.00.00",
                "11.0.0",
            };

            List<string> blackList = new List<string>()
            {
                "12.00.00-12.00.01",
                "12.00.00-",
                "12.00.",
                "11.00.0.0",
            };

            var vc = new VersionCheck();

            foreach (var item in whiteList)
            {
                bool result = vc.IsValidVersion(item);
                Debug.Assert(result == true);
            }

            foreach (var item in blackList)
            {
                bool result = vc.IsValidVersion(item);
                Debug.Assert(result == false);
            }
        }
    }
}
