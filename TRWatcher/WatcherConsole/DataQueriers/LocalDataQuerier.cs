using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roles
{
    public class LocalDataQuerier
    {
        #region SingleInstance
        static LocalDataQuerier _instance = null;
        static Object lock_CreateSingleInstance = new Object();
        public static LocalDataQuerier SingleInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lock_CreateSingleInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new LocalDataQuerier();
                        }
                    }
                }

                return _instance;

            }
        }
        private LocalDataQuerier()
        {

        }
        #endregion
    }


}
