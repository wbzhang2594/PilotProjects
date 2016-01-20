using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Arguments
{
    public class ToSearchTokens
    {
        public ToSearchTokens()
        {
            ListOfToSearchTokens = new List<JToken>();
        }

        public List<JToken> ListOfToSearchTokens { get; set; }
    }
}
