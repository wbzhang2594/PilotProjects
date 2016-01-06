using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.DesignPattern
{
    public class Singleton<T> where T : new()
    {
        protected Singleton()
        {            
        }

        private static T _SingleInstance = new T();

        public static T SingleInstance
        {
            get
            {
                return _SingleInstance;
            }
        }
    }
}
