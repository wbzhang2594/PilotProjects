﻿using DataContract;
using Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole
{
    class TRFactory
    {
        IDataAnalyser CreateDataAnalyser(DataCategory dataCategory)
        {
            switch(dataCategory)
            {
                case DataCategory.CaseHistory:
                    {
                        return CaseHistory_Analyser.Instance;
                        break;
                    }
                case DataCategory.CurrentFailedCase:
                    {
                        return CurrentFailedCase_Analyser.Instance;
                    }
            }
        }
    }
}
