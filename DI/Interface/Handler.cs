using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public class Handler
    {
        public Handler()
        {
            Assembly MyDLL = Assembly.GetAssembly(this.GetType());
            var types = MyDLL.GetTypes();
        }
    }
}
