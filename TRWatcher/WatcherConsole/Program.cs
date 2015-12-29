using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.Globalization;
using log4net.Core;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace WatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //WebClient MyWebClient = new WebClient();


                //MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                //Byte[] pageData = MyWebClient.DownloadData(@"http://nebula:3000/api/Failed"); //从指定网站下载数据

                //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                //try
                //{
                //    File.WriteAllText(@"d:\_Test\TRWatcher\FailedCases.json", pageHtml);
                //}
                //catch
                //{

                //}
                do
                {
                    DateTime dt = DateTime.Now;
                    string CurrentCultureInfo = Thread.CurrentThread.CurrentCulture.ToString();
                    File.AppendAllText("findResult.json", dt.ToString("G", CultureInfo.InvariantCulture));
                    log4net.LogManager.GetLogger(typeof(Program)).Error(dt.ToString("G", CultureInfo.InvariantCulture));

                    string pageHtml = File.ReadAllText("FailedCases.json");

                    var jArray = JsonConvert.DeserializeObject(pageHtml) as Newtonsoft.Json.Linq.JArray;

                    Newtonsoft.Json.Linq.JArray findResult = new Newtonsoft.Json.Linq.JArray();

                    foreach (string result in QueryFrom(jArray))
                    {
                        File.AppendAllText("findResult.json", result);
                        log4net.LogManager.GetLogger(typeof(Program)).Error(result);

                    }
                    Thread.Sleep(1000);
                } while (true);

            }
            catch (Exception ex)
            {

            }

        }

        private static IEnumerable<string> QueryFrom(Newtonsoft.Json.Linq.JArray jArray)
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
