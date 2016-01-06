using DataContract;
using Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;

namespace WatcherConsole
{
    class TRFactory : Singleton<TRFactory>
    {
        public IDataAnalyser CreateDataAnalyser(DataCategory dataCategory)
        {
            switch (dataCategory)
            {
                case DataCategory.CaseHistory:
                    {
                        return CaseHistory_Analyser.SingleInstance;
                    }
                case DataCategory.CurrentFailedCase:
                    {
                        return CurrentFailedCase_Analyser.SingleInstance;
                    }
                default:
                    {
                        throw new NotSupportedException(dataCategory.ToString());
                    }
            }
        }

        public IDataQuerier CreateDataQuerier(RunMode runMode)
        {
            switch (runMode)
            {
                case RunMode.local:
                    {
                        return Local_DataQuerier.SingleInstance;
                    }
                case RunMode.web:
                    {
                        return Web_DataQuerier.SingleInstance;
                    }
                default:
                    {
                        throw new NotSupportedException(runMode.ToString());
                    }
            }
        }
    }
}
