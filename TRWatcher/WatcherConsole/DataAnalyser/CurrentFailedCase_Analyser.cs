using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;

namespace Roles

{
    class CurrentFailedCase_Analyser : Singleton<CurrentFailedCase_Analyser>, IDataAnalyser
    {
        #region singleInstance
        //CurrentFailedCase_Analyser() { }

        //static CurrentFailedCase_Analyser _instance = null;
        //static object singleInstanceLock = new object();

        //public static CurrentFailedCase_Analyser Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (singleInstanceLock)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new CurrentFailedCase_Analyser();
        //                }
        //            }
        //        }

        //        return _instance;
        //    }
        //}

        #endregion
    }
}
