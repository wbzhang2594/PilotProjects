using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract
{
    public enum DataCategory
    {
        CaseHistory,
        CurrentFailedCase,
    }

    public enum RunMode
    {
        local,
        web,
    }

    public enum RelationLogic
    {
        AND,
        OR,
        NOT,
    }
}
