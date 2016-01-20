using DataContract;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcherConsole.RelationshipLogicHandlers;

namespace WatcherConsole.QueryStatement
{
    public class ContainerStatement : IStatement
    {
        public ContainerStatement()
        {
            ChildrenStatements = new List<IStatement>();
        }

        public RelationLogic RelLogic { get; set; }

        public List<IStatement> ChildrenStatements { get; set; }

        public void Accept(IStatementHandler statementHandler, object Context_Of_Statement)
        {
            statementHandler.Visit(this, Context_Of_Statement);

            List<JToken>[] SearchResults = Context_Of_Statement as List<JToken>[];

            IRelationshipHandler relHandler = TRFactory.SingleInstance.CreateRelationshipLogicHandler(this.RelLogic);


            relHandler.Calculate(SearchResults[0]);
            List<JToken> Y_Tokens = new List<JToken>(SearchResults[0]);
            List<JToken> N_Tokens = new List<JToken>(SearchResults[1]);

            foreach (IStatement statementItem in ChildrenStatements)
            {
                List<JToken>[] inputParameter = new List<JToken>[] { Y_Tokens, N_Tokens };
                statementItem.Accept(statementHandler, inputParameter);
                List<JToken>[] output = relHandler.Calculate(inputParameter[0]) as List<JToken>[];
                Y_Tokens = output[0]; N_Tokens = output[1];
            }

            SearchResults[0] = Y_Tokens; SearchResults[1] = N_Tokens;
        }
    }
}
