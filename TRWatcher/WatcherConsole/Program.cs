using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using DataContract;
using Newtonsoft.Json.Linq;
using System.IO;

[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]
namespace WatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> CareList = new List<string>()
                {
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_DebugTest",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_MatRep_R24B",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R24",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R24A_AS",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R251_LA",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R251",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_AS",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_EMEA",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_LA",
                    @"BLLTest_PCT:BL.Operation.PostCheckInTest.PCRCompileTest.PCRPackunPack_Par_R260_NA",
                };

                foreach (var item_CareList in CareList)
                {
                    var testResult = TRFactory.SingleInstance.CreateDataQuerier(RunMode.local).GetHistoryOfFailedCases(item_CareList);

                    JArray History = (JArray)testResult["History"];

                    foreach (var item_History in History)
                    {
                        string Message = item_History.Value<string>("Message");
                        if(MessageMatchQuery(Message))
                        {
                            File.AppendAllText(@"d:\_test\ThreadSafeIssue\logFailedCasesMessage.json", Message+"\n==========================================\n");
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        private static bool MessageMatchQuery(string message)
        {
            if(message.Contains("ThreadSafeIssue"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetMessageVersion(string originalMessage)
        {
            throw new NotImplementedException();
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
