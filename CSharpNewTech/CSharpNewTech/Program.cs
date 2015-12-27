using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestReference;

namespace CSharpNewTech
{
    class Program
    {
        static void Main(string[] args)
        {
            //Debugger.Launch();

            Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
            string pathof_ExecutingAssembly = ExecutingAssembly.Location;
            System.Console.WriteLine("ExecutingAssembly: " + pathof_ExecutingAssembly);

            Assembly CallingAssembly = Assembly.GetCallingAssembly();
            string pathof_CallingAssembly = CallingAssembly.Location;
            System.Console.WriteLine("CallingAssembly: " + pathof_CallingAssembly);


            Assembly EntryAssembly = Assembly.GetEntryAssembly();
            string pathof_EntryAssembly = EntryAssembly.Location;
            System.Console.WriteLine("EntryAssembly: " + pathof_EntryAssembly);

            Class1 c1 = new Class1();
            c1.run();
        }
    }
}
