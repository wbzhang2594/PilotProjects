using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.DesignPattern;

namespace Roles

{
    class CaseHistory_Analyser : Singleton<CaseHistory_Analyser>, IDataAnalyser
    {
        public void AnalyzeCases(object Context)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
