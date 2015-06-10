using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Method_formwork v1 = new Method_formwork(method_Instance);

        }

        private static void method_Instance(TestArgs args) { }
    }

    public class TestArgs
    {

    }

    public delegate void Method_formwork(TestArgs args);
}
