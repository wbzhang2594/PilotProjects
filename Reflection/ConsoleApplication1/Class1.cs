using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.NS1
{
    public class Class1
    {
        public void run()
        {

            
            var assembly = this.GetType().Assembly;
            var types = assembly.GetTypes();
            var NS2Types = types.Where(item => item.FullName.Contains("NS2"));

            foreach(Type type in NS2Types)
            {
                var Methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                foreach(MethodInfo methodInfo in Methods)
                {
                    var attributes = methodInfo.GetCustomAttributes(typeof(ConstraintIDAttribute));
                    foreach(var attr in attributes)
                    {
                        var CnsAttr = attr as ConstraintIDAttribute;

                    }
                    string err;
                    object[] parameters = new object[]{"intput1", null};
                    object returnValue = methodInfo.Invoke(null, parameters);
                    err = parameters[1] as string;

                }
            }

            object oResult = null;
            bool result = (bool)(oResult ?? false);
        }


        public string Name { get; set; }
    }
}
