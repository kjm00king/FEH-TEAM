using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.wiki
{
    public class QueryWeapons
    {
        public class Query
        {
            public class Result
            {
                public Dictionary<string, object[]> printouts = new Dictionary<string, object[]>();
            }

            public Dictionary<string, Result> results = new Dictionary<string, Result>();
        }

        public Query query = new Query();
    }
}
