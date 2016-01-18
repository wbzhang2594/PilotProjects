using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Roles
{
    public interface IDataQuerier
    {
        JToken GetUpToDateFailedCases();

        JToken GetHistoryOfFailedCases(string caseItem);
    }
}
