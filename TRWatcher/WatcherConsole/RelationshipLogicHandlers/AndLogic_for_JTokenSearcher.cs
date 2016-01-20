using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcherConsole.RelationshipLogicHandlers
{
    public class AndLogic_for_JTokenSearcher : IRelationshipHandler
    {

        List<JToken> Y_Tokens = new List<JToken>();

        public object Calculate(object item_parameters)
        {

            List<JToken>[] searchResult = item_parameters as List<JToken>[];

            if (Y_Tokens.Count() == 0)
            {
                Y_Tokens.AddRange(searchResult[0]);
            }
            else
            {
                Y_Tokens = new List<JToken>(Y_Tokens.TakeWhile((TItem) => searchResult[0].Contains(TItem)));
            }

            List<JToken> N_Tokens = new List<JToken>(searchResult[1]);
            N_Tokens.AddRange(searchResult[0].TakeWhile(TItem => !Y_Tokens.Contains(TItem)));

            return new List<JToken>[] { Y_Tokens, N_Tokens };
        }
    }
}
