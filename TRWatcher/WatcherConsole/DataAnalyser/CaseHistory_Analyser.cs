﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roles
{
    public class CaseHistory_Analyser : IDataAnalyser
    {
        #region SingleInstance
        static CaseHistory_Analyser _instance = null;
        static Object lock_CreateSingleInstance = new Object();
        public static CaseHistory_Analyser SingleInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lock_CreateSingleInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new CaseHistory_Analyser();
                        }
                    }
                }

                return _instance;

            }
        }
        private CaseHistory_Analyser()
        {

        }
        #endregion


    }
}
