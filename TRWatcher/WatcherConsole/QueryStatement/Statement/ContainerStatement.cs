using DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.QueryStatement
{
    public class ContainerStatement: IStatement
    {
        public ContainerStatement()
        {
            ChildrenStatements = new List<IStatement>();
        }

        public RelationLogic RelLogic { get; set; }

        public List<IStatement> ChildrenStatements { get; set; }
    }
}
