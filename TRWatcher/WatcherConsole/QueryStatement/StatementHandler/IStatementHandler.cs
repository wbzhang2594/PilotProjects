using DataContract.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.QueryStatement
{
    public interface IStatementHandler
    {
        object HandleStatement(IStatement statement, ToSearchTokens Context);

        void Visit(BasisStatement basisStatement, ToSearchTokens Context);

        void Visit(ContainerStatement containerStatement, ToSearchTokens Context);
    }
}
