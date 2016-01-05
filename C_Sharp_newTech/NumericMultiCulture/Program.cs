using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericMultiCulture
{
    class Program
    {
        static void Main(string[] args)
        {
            //double dNum = -2222222.2222222;
            int dNum = -1;
            Dictionary<string, HashSet<CultureInfo>> Dic_strNum_ListCultureInfo = new Dictionary<string, HashSet<CultureInfo>>();
            
            foreach (var cI in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                string strNum = string.Format(cI, "{0}", dNum); //dNum.ToString(cI);
                string strNum2 = string.Format(CultureInfo.InvariantCulture, "{0}", dNum); //dNum.ToString(cI);
                //string strNum3 = string.Format("{0}", dNum); //dNum.ToString(cI);

                if (Dic_strNum_ListCultureInfo.ContainsKey(strNum)==false)
                {
                    Dic_strNum_ListCultureInfo[strNum] = new HashSet<CultureInfo>();
                }

                Dic_strNum_ListCultureInfo[strNum].Add(cI);

            }

            foreach (var NumFormat in Dic_strNum_ListCultureInfo.Keys)
            {
                System.Console.WriteLine(NumFormat);

                StringBuilder sb = new StringBuilder();
                foreach (var cI in Dic_strNum_ListCultureInfo[NumFormat])
                {
                    sb.Append(cI.ToString() + ";  ");
                }
                
                System.Console.WriteLine("    => " + sb.ToString());
            }
        }
    }
}
