using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WatcherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                WebClient MyWebClient = new WebClient();


                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据

                Byte[] pageData = MyWebClient.DownloadData(@"http://nebula:3000/api/Failed"); //从指定网站下载数据

                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            

                string pageHtml2 = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                var jArray = JsonConvert.DeserializeObject(pageHtml) as Newtonsoft.Json.Linq.JArray;
                foreach (var item in jArray)
                {
                    if (item.Value<string>("AssignToEngineer").Equals("nebula", StringComparison.InvariantCultureIgnoreCase))
                    {
                    }
                }
                var jsonString = File.ReadAllText(@"C:\Users\rxt867\Desktop\Failed.json");
            }
            catch
            {

            }

        }

    }
}
