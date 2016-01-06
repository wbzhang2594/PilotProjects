using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Roles
{
    interface IDataQuerier
    {
        JObject GetUpToDateFailedCases();

        JObject GetHistoryOfFailedCases(string caseItem);
    }
}
