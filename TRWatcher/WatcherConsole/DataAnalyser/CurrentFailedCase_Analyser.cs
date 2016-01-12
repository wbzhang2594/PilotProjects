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
    public class CurrentFailedCase_Analyser : Singleton<CurrentFailedCase_Analyser>, IDataAnalyser, IDisposable
    {
        static Dictionary<string, string> cases_version = new Dictionary<string, string>();

        public CurrentFailedCase_Analyser() : base()
        {
            string cached_case_version = @"cached_case_version.json";
            if (File.Exists(cached_case_version))
            {
                string ExecutingAssemblyPath = Assembly.GetExecutingAssembly().Location;
                string DirectoryName = (new FileInfo(ExecutingAssemblyPath)).DirectoryName;
                string FilePath = Path.Combine(DirectoryName, cached_case_version);

                string jsonContent = File.ReadAllText(FilePath);
                cases_version = JsonConvert.DeserializeAnonymousType(jsonContent, new Dictionary<string, string>());

                try
                {
                    File.Delete(cached_case_version);
                }
                catch { }
            }
        }

        static Dictionary<string, List<string>> FilterConditions_Level1 = new Dictionary<string, List<string>>()
        {
            //{ "FullName", new List<string>() { "BL.Operation.PostCheckInTest.PCRCompileTest." } },
            {"Message", new List<string>() {"ThreadSafeIssue",}
            },

        };

        static Dictionary<string, List<string>> FilterConditions_Level2 = new Dictionary<string, List<string>>()
        {
            //{ "FullName", new List<string>() { "BL.Operation.PostCheckInTest.PCRCompileTest." } },
            {"Message", new List<string>() {"THREADFAULT",}
            },

        };

        static Dictionary<string, List<string>> FilterConditions_Level2_HasNo = new Dictionary<string, List<string>>()
        {
            //{ "FullName", new List<string>() { "BL.Operation.PostCheckInTest.PCRCompileTest." } },
            {"Message", new List<string>() {"BLCompile_UnPacking_Fix_InvalidDataInRadio",
                                            "SPELLCHECK_FAILED_IN_COMPILE",
                                            "Minimal Options Mismatch",
                                            "Value was either too large or too small for an Int32",}
            },


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
                    string FullName_fileOfCase = Path.Combine(@"\\nebula-01\TestResults\BLLThreadSafeIssue\", NameOfCase + ".log1");

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
            if (MessageMatchQuery_HasAll(message, FilterConditions_Level1))
            {
                return true;
            }



            if (MessageMatchQuery_HasAll(message, FilterConditions_Level2)
            && MessageMatchQuery_HasNo(message, FilterConditions_Level2_HasNo))
            {
                return true;
            }

            return false;

        }

        private bool MessageMatchQuery_HasAll(JToken message, Dictionary<string, List<string>> _filterCondition_AND)
        {
            foreach (var item_FilterKey in _filterCondition_AND.Keys)
            {
                string content = message.Value<string>(item_FilterKey);
                content = content.Replace("&nbsp;", " ");
                foreach (var Item_filterConditions in _filterCondition_AND[item_FilterKey])
                {
                    if (content.Contains(Item_filterConditions) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool MessageMatchQuery_OR(JToken message, Dictionary<string, List<string>> _filterCondition_OR)
        {
            foreach (var item_FilterKey in _filterCondition_OR.Keys)
            {
                string content = message.Value<string>(item_FilterKey);
                content = content.Replace("&nbsp;", " ");
                foreach (var Item_filterConditions in _filterCondition_OR[item_FilterKey])
                {
                    if (content.Contains(Item_filterConditions))
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        private bool MessageMatchQuery_HasNo(JToken message, Dictionary<string, List<string>> _filterCondition_HasNo)
        {
            foreach (var item_FilterKey in _filterCondition_HasNo.Keys)
            {
                string content = message.Value<string>(item_FilterKey);
                content = content.Replace("&nbsp;", " ");
                foreach (var Item_filterConditions in _filterCondition_HasNo[item_FilterKey])
                {
                    if (content.Contains(Item_filterConditions))
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

        public void Dispose()
        {
            string cached_case_version = @"cached_case_version.json";
            if (File.Exists(cached_case_version))
            {
                try
                {
                    File.Delete(cached_case_version);
                }
                catch { }
            }

            string json_cases_version = JsonConvert.SerializeObject(cases_version);

            File.WriteAllText(cached_case_version, json_cases_version);

        }
    }
}
