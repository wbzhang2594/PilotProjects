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
    class Local_DataQuerier : Singleton<Web_DataQuerier>, IDataQuerier
    {
        public JArray GetHistoryOfFailedCases(string caseItem)
        {
            string MockJsonFile = "FailedCases.json";
            return GetJSonResultFromFile(MockJsonFile);

        }

        private static JArray GetJSonResultFromFile(string MockJsonFile)
        {
            string ExecutingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string FilePath = Path.Combine(ExecutingAssemblyPath, "LocalTestResource", MockJsonFile);

            string jsonContent = File.ReadAllText(FilePath);
            var jArray = JsonConvert.DeserializeObject(jsonContent) as Newtonsoft.Json.Linq.JArray;

            return jArray;
        }

        public JArray GetUpToDateFailedCases()
        {
            string MockJsonFile = "OneCases.json";
            return GetJSonResultFromFile(MockJsonFile);
        }
    }


}
