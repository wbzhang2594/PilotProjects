﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using DataContract;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WatcherConsole.QueryStatement.StatementHandler;
using WatcherConsole.QueryStatement;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]
namespace WatcherConsole
{
    class Program
    {

        static CancellationTokenSource source1 = new CancellationTokenSource();
        static CancellationToken token1 = source1.Token;

        static void Main(string[] args)
        {
            //List<string> CareList = new List<string>()
            //{
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_DebugTest",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_MatRep_R24B",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R24",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R24A_AS",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R251_LA",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R251",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_AS",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_EMEA",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_LA",
            //    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_NA",
            //};

            //foreach (var item_CareList in CareList)
            //{
            //    var testResult = TRFactory.SingleInstance.CreateDataQuerier(RunMode.web).GetHistoryOfFailedCases(item_CareList);

            //    JArray History = (JArray)testResult["History"];

            //    foreach (var item_History in History)
            //    {
            //        string Message = item_History.Value<string>("Message");
            //        Message = Message.Replace("&nbsp;", " ");
            //        if(MessageMatchQuery(Message))
            //        {
            //            File.AppendAllText(@"d:\_test\ThreadSafeIssue\logFailedCasesMessage.json", Message+"\n==========================================\n");
            //        }
            //    }
            //}


            //try
            //{

            //    Task task = new Task(MonitoringFailedCases2, token1);

            //    task.Start();

            //    while ('x' != System.Console.Read())
            //    {
            //        Thread.Sleep(1000);
            //    }

            //    source1.Cancel();
            //    task.Wait();
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    source1.Dispose();
            //}

            MonitoringFailedCases2();
        }

        private static void MonitoringFailedCases2()
        {
            JArray currentFailCases = TRFactory.SingleInstance.CreateDataQuerier(RunMode.web).GetUpToDateFailedCases() as JArray;

            IStatement statement = CreateStatement();
            StatementCalculator sc = new StatementCalculator();

            List<JToken>[] parameters = new List<JToken>[] { new List<JToken>(currentFailCases), new List<JToken>() };
            sc.HandleStatement(statement, parameters);
        }

        private static IStatement CreateStatement()
        {
            ContainerStatement s1 = new ContainerStatement();
            s1.RelLogic = RelationLogic.AND;

            BasisStatement bs1_1 = new BasisStatement("Message", "Name", SearchLogic.Contains);
            BasisStatement bs1_2 = new BasisStatement("Message", "ASTRO", SearchLogic.Contains);
            BasisStatement bs1_3 = new BasisStatement("Message", "Languagepack", SearchLogic.Contains);

            s1.ChildrenStatements.Add(bs1_1);
            s1.ChildrenStatements.Add(bs1_2);
            s1.ChildrenStatements.Add(bs1_3);

            return s1;
        }

        private static void MonitoringFailedCases()
        {
            do
            {

                JArray currentFailCases = TRFactory.SingleInstance.CreateDataQuerier(RunMode.web).GetUpToDateFailedCases() as JArray;
                TRFactory.SingleInstance.CreateDataAnalyser(DataCategory.CurrentFailedCase).AnalyzeCases(currentFailCases);

                System.Console.Write('.');
                for (int i = 0; i < 10; i++)
                {
                    if (token1.IsCancellationRequested)
                    {
                        TRFactory.SingleInstance.CreateDataAnalyser(DataCategory.CurrentFailedCase).Dispose();
                        token1.ThrowIfCancellationRequested();
                    }
                    Thread.Sleep(1000);
                }
            } while (true);
        }

        private static bool MessageMatchQuery(string message)
        {
            if (message.Contains("ThreadSafeIssue"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static IEnumerable<string> QueryFrom(Newtonsoft.Json.Linq.JArray jArray)
        {
            foreach (var item in jArray)
            {
                string message = item.Value<string>("Message");
                string CleanMessage = message.Replace("&nbsp;", " ");

                if (true /*item.Value<string>("Message").StartsWith("[ThreadSafeIssue]", StringComparison.InvariantCultureIgnoreCase)*/)
                {
                    yield return JsonConvert.SerializeObject(item);
                }
            }
        }
    }
}
