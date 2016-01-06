using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Roles
{
    class Local_DataQuerier : Singleton<Local_DataQuerier>, IDataQuerier
    {
        public JObject GetHistoryOfFailedCases(string caseItem)
        {
            string MockJsonFile = "OneCases.json";
            return GetJSonResultFromFile(MockJsonFile);

        }

        private static JObject GetJSonResultFromFile(string MockJsonFile)
        {
            string ExecutingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string DirectoryName = (new FileInfo(ExecutingAssemblyPath)).DirectoryName;
            string FilePath = Path.Combine(DirectoryName, "LocalTestResource", MockJsonFile);

            string jsonContent = File.ReadAllText(FilePath);
            var jArray = JsonConvert.DeserializeObject(jsonContent) as Newtonsoft.Json.Linq.JObject;

            return jArray;
        }

        public JObject GetUpToDateFailedCases()
        {
            string MockJsonFile = "FailedCases.json";
            return GetJSonResultFromFile(MockJsonFile);
        }
    }


}
