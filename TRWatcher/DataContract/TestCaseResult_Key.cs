using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract
{
    public class TestCaseResult_Key : IEqualityComparer<TestCaseResult_Key>
    {
        public TestCaseResult_Key(string FullName, string MessageVersion)
        {
            this.CaseFullName = FullName;
            this.MessageVersion = MessageVersion;
        }

        public string CaseFullName { get; private set; }
        public string MessageVersion { get; private set; }


        public bool Equals(TestCaseResult_Key x, TestCaseResult_Key y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.CaseFullName == y.CaseFullName && x.MessageVersion == y.MessageVersion;
        }

        public int GetHashCode(TestCaseResult_Key obj)
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + obj.CaseFullName.GetHashCode();
            hash = hash * 23 + obj.MessageVersion.GetHashCode();
            return hash;
        }



    }
}
