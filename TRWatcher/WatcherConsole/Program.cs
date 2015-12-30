using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using DataContract;

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

                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Newtonsoft.Json.Linq.JArray failedLogs = new Newtonsoft.Json.Linq.JArray();

                foreach (string caseItem in CareList)
                {
                    string QueryString = @"http://nebula:3000/api/TestCases/" + caseItem;
                    
                    Byte[] pageData = MyWebClient.DownloadData(QueryString); //从指定网站下载数据

                    string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句

                    var jArray = JsonConvert.DeserializeObject(pageHtml) as Newtonsoft.Json.Linq.JArray;

                    foreach (var jSonItem in jArray)
                    {
                        if (jSonItem.Value<bool>("Passed") == false)
                        {
                            string fullName = jSonItem.Value<string>("FullName");
                            string OriginalMessage = jSonItem.Value<string>("Message");
                            string MessageVersion = GetMessageVersion(OriginalMessage);

                            TestCaseResult_Key key = new TestCaseResult_Key(fullName, MessageVersion);
                        }
                    }

                    //string cleanedPageHtml = pageHtml.Replace("&nbsp;", " ");
                    //try
                    //{
                    //    File.WriteAllText(@"d:\_Test\TRWatcher\OneCases.json", cleanedPageHtml);
                    //}
                    //catch
                    //{

                    //}

                }
                //Byte[] pageData = MyWebClient.DownloadData(@"http://nebula:3000/api/Failed"); //从指定网站下载数据

                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                //try
                //{
                //    File.WriteAllText(@"d:\_Test\TRWatcher\FailedCases.json", pageHtml);
                //}
                //catch
                //{

                //}


                //do
                //{
                //    DateTime dt = DateTime.Now;
                //    string CurrentCultureInfo = Thread.CurrentThread.CurrentCulture.ToString();
                //    File.AppendAllText("findResult.json", dt.ToString("G", CultureInfo.InvariantCulture));
                //    log4net.LogManager.GetLogger(typeof(Program)).Error(dt.ToString("G", CultureInfo.InvariantCulture));

                //    string pageHtml = File.ReadAllText("FailedCases.json");

                //    var jArray = JsonConvert.DeserializeObject(pageHtml) as Newtonsoft.Json.Linq.JArray;

                //    Newtonsoft.Json.Linq.JArray findResult = new Newtonsoft.Json.Linq.JArray();

                //    foreach (string result in QueryFrom(jArray))
                //    {
                //        File.AppendAllText("findResult.json", result);
                //        log4net.LogManager.GetLogger(typeof(Program)).Error(result);

                //    }
                //    Thread.Sleep(1000);
                //} while (true);

            }
            catch (Exception ex)
            {

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
