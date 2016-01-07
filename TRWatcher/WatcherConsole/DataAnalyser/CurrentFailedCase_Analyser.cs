using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;
using Newtonsoft.Json.Linq;

namespace Roles

{
    class CurrentFailedCase_Analyser : Singleton<CurrentFailedCase_Analyser>, IDataAnalyser
    {
        static Dictionary<string, string> cases_version = new Dictionary<string, string>();

        static Dictionary<string, List<string>> FilterConditions = new Dictionary<string, List<string>>()
        {
            { "FullName", new List<string>() { "BL.Operation.PostCheckInTest.PCRCompileTest." } },
            {"Message", new List<string>() {"ThreadSafeIssue", } },
        };


        public void AnalyzeCases(object Context)
        {
            JArray currentFailedCases = Context as JArray;
            foreach (var item_currentFailedCases in currentFailedCases)
            {
                string Message = item_currentFailedCases.Value<string>("Message");
                Message = Message.Replace("&nbsp;", " ");
                if (MessageMatchQuery(item_currentFailedCases))
                {
                    string NameOfCase = item_currentFailedCases.Value<string>("FullName");
                    NameOfCase = NameOfCase.Substring(NameOfCase.LastIndexOf('.') + 1);
                    string FullName_fileOfCase = Path.Combine(@"\\nebula-01\TestResults\BLLThreadSafeIssue\", NameOfCase + ".log");

                    string VersionOfMessage = GetMessageVersion(Message);

                    if (cases_version.ContainsKey(NameOfCase)
                        && cases_version[NameOfCase].Equals(VersionOfMessage, StringComparison.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        cases_version[NameOfCase] = VersionOfMessage;
                        File.AppendAllText(FullName_fileOfCase, "\n============================================\n" + Message);
                        System.Console.WriteLine();
                        System.Console.WriteLine("Updated: " + FullName_fileOfCase);
                    }
                }
            }
        }

        private bool MessageMatchQuery(JToken message)
        {
            foreach (var item_FilterKey in FilterConditions.Keys)
            {
                string content = message.Value<string>(item_FilterKey);
                content = content.Replace("&nbsp;", " ");
                foreach (var Item_filterConditions in FilterConditions[item_FilterKey])
                {
                    if (content.Contains(Item_filterConditions) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static string GetMessageVersion(string originalMessage)
        {
            string flag = "Message&nbsp;Version:&nbsp;\r<br/>&nbsp;&nbsp;&nbsp;&nbsp;".Replace("&nbsp;", " ");

            string version = originalMessage.Substring(originalMessage.IndexOf(flag) + flag.Length, 5);
            return version;
        }
    }
}
