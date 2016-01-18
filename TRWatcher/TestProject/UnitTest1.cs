using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatcherConsole.QueryStatement.StatementHandler;
using DataContract;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            object currentFailCases = TRFactory.SingleInstance.CreateDataQuerier(RunMode.web).GetUpToDateFailedCases() as JArray;

            StatementCalculator sc = new StatementCalculator()
        }
    }
}
