using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.wiki
{
    public class QueryHeroesStats
    {
        public class Query
        {
            public class Result
            {
                public Dictionary<string, string[]> printouts = new Dictionary<string, string[]>();
            }

            public Dictionary<string, Result> results = new Dictionary<string, Result>();
        }

        public Query query = new Query();
    }
}
