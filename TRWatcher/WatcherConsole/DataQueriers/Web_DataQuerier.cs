using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;
using Newtonsoft.Json;

namespace Roles
{
    class Web_DataQuerier : Singleton<Web_DataQuerier>, IDataQuerier
    {

        public Newtonsoft.Json.Linq.JToken GetUpToDateFailedCases()
        {
            string QueryString = @"http://nebula:3000/api/Failed";

            return GetJsonResult(QueryString);
        }

        public Newtonsoft.Json.Linq.JToken GetHistoryOfFailedCases(string caseItem)
        {

            string QueryString = @"http://nebula:3000/api/TestCases/" + caseItem;
            return GetJsonResult(QueryString);
        }

        private static Newtonsoft.Json.Linq.JToken GetJsonResult(string QueryString)
        {
            WebClient MyWebClient = new WebClient();

            Byte[] pageData = MyWebClient.DownloadData(QueryString); //从指定网站下载数据

            string pageHtml = Encoding.UTF8.GetString(pageData);

            var jArray = JsonConvert.DeserializeObject(pageHtml) as Newtonsoft.Json.Linq.JToken;

            return jArray;
        }
    }


}
