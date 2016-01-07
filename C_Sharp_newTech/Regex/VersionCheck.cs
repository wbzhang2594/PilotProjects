using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegexTest
{
    public class VersionCheck
    {
        public bool IsValidVersion(string version)
        {
            Regex rgx = new Regex(@"^\w{1}([0-9]{1,2}\.){1,2}[0-9]{1,2}$");
            if(rgx.IsMatch(version))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
