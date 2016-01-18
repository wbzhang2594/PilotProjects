using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.QueryStatement
{
    public class BasisStatement : IStatement
    {
        public string FieldName { get; }
        public string searchValue { get; }
        public SearchLogic Logic { get; }

        public BasisStatement(string fieldName, string searchValue, SearchLogic searchLogic)
        {

        }

        public void Accept(IStatementHandler statementHandler)
        {
            statementHandler.Visit(this);
        }
    }
}
