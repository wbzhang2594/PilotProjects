using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roles

{
    public class CurrentFailedCase_Analyser : IDataAnalyser
    {
        #region SingleInstance
        static CurrentFailedCase_Analyser _instance = null;
        static Object lock_CreateSingleInstance = new Object();
        public static CurrentFailedCase_Analyser SingleInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lock_CreateSingleInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new CurrentFailedCase_Analyser();
                        }
                    }
                }

                return _instance;

            }
        }
        private CurrentFailedCase_Analyser()
        {

        }
        #endregion
    }
}
