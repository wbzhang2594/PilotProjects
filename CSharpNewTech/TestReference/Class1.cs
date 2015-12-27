using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestReference
{
    public class Class1
    {
        public void run()
        {

            Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
            string pathof_ExecutingAssembly = ExecutingAssembly.Location;
            System.Console.WriteLine("I'm TestReference, ExecutingAssembly: " + pathof_ExecutingAssembly);

            Assembly CallingAssembly = Assembly.GetCallingAssembly();
            string pathof_CallingAssembly = CallingAssembly.Location;
            System.Console.WriteLine("I'm TestReference, CallingAssembly: " + pathof_CallingAssembly);


            Assembly EntryAssembly = Assembly.GetEntryAssembly();
            string pathof_EntryAssembly = EntryAssembly.Location;
            System.Console.WriteLine("I'm TestReference, EntryAssembly: " + pathof_EntryAssembly);
        }
    }
}
