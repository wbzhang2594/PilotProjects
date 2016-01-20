using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.RelationshipLogicHandlers
{
    public interface IRelationshipHandler
    {
        object Calculate(object item_parameters);
    }
}
