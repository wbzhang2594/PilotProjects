using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Arguments
{
    public class TokensSearchResult
    {
        public TokensSearchResult()
        {
            True_Tokens = new List<JToken>();
            False_Tokens = new List<JToken>();
        }

        public List<JToken> True_Tokens { get; set; }
        public List<JToken> False_Tokens { get; set; }
    }
}
