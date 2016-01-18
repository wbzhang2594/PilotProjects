﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.QueryStatement
{
    public interface IStatementHandler
    {
        bool HandleStatement(IStatement statement, object Context);

        bool Visit(BasisStatement basisStatement);

        bool Visit(ContainerStatement containerStatement)
    }
}