using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1;

namespace ConsoleApplication1.NS2
{
    public class Class2
    {
        [ConstraintID("ValidateDataWideBluetoothDUNPeerIP")]
        public static bool Method(string input, out string error)
        {
            error = input + "err";
            return true;
        }
    }
}
