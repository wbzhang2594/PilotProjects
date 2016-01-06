using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roles
{
    class OnlineDataQuerier
    {
        #region SingleInstance
        static OnlineDataQuerier _instance = null;
        static Object lock_CreateSingleInstance = new Object();
        public static OnlineDataQuerier SingleInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lock_CreateSingleInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new OnlineDataQuerier();
                        }
                    }
                }

                return _instance;

            }
        }
        private OnlineDataQuerier()
        {

        }
        #endregion
    }


}
