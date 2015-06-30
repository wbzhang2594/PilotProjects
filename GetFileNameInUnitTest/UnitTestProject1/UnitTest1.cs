using System;
using GetFileNameInUnitTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 p = new Class1();
            string codeBase = p.GetType().Assembly.CodeBase;

        }
    }
}
