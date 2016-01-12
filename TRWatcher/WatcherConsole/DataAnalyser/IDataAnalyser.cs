using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roles

{
    interface IDataAnalyser : IDisposable
    {
        void AnalyzeCases(object Context);
    }
}
