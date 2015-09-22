using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TXT_Trans
{
    class Program
    {
        static void Main(string[] args)
        {
            string SourceFile = Path.GetFullPath(@"D:\_Test\txtFileForTrans\source.txt");

            //Regex regex = new Regex(@"\[(?<FieldName>.*)\] default value ");
            Regex regex = new Regex(@"RMCBLLFactory\.Get_EditorOperations_SingleInstance\(\)\.(?<FieldName>.*)\(");

            string SourceContent = ReadContentFromFile(SourceFile);

            MatchCollection mc = regex.Matches(SourceContent);

            List<string> fieldsNameList = new List<string>();
            foreach (Match match in mc)
            {
                string FieldName = match.Groups["FieldName"].ToString();
                fieldsNameList.Add(FieldName);
            }

            StringBuilder FieldNameList = new StringBuilder();
            foreach (string fieldName in fieldsNameList.Distinct())
            {
                FieldNameList.AppendLine(string.Format("\"{0}\",", fieldName));
            }

            string ResultString = FieldNameList.ToString();
        }

        private static string ReadContentFromFile(string SourceFile)
        {
            string content;

            using (FileStream tr = new FileStream(SourceFile, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(tr))
                {
                    content = sr.ReadToEnd();
                }
            }

            return content;
        }



    }
}
