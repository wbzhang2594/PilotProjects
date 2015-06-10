using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.NS1;

namespace ConsoleApplication1
{
    class Program
    {


        static void Main(string[] args)
        {
            Class1 c1 = new Class1();
            string propertyName = PropertyHelper.GetPropertyName(() => c1.Name);

            //{
            //    List<string> strList = new List<string>()
            //    {
            //        "1",
            //        "2",
            //    };

            //    string found = strList.First(name => name == "3");
            //}

            //{
            //    List<string> strList = null;
            //    string found = strList.First(name => name == "3");
            //}

            //{
            //    List<string> strList = new List<string>();
            //    string found = strList.First(name => name == "3");
            //}


            string str = typeof(List<string>).ToString();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = Type.GetType(str);

            IDictionary DataSource = null;

            Dictionary<string, string> aaa = new Dictionary<string, string>();

            aaa.Add("1", null);
            aaa.Add("2", null);

        }
    }
}
