using System;
using ConsoleApplication1.NS1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 c1 = new Class1();
            c1.run();
        }
    }
}
