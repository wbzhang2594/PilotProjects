using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            //Version ver1 = new Version("1");
            Version ver1_1 = new Version("1.1");
            //Version ver_R1_1 = new Version("R1.1"); //cannot support

            Version ver1_1_1 = new Version("1.1111.1");

            Nullable<int> nullable = null;
            System.Console.WriteLine(nullable.Value);

        }
    }
}
