using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.QueryStatement
{
    public interface IStatement
    {
        void Accept(IStatementHandler statementHandler);
    }
}
