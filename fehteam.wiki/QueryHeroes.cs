using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.wiki
{
    public class QueryHeroes
    {
        public class Query
        {
            public class Result
            {
                public class Printout
                {
                    public class Weapon
                    {
                        public string fulltext = string.Empty;
                    }

                    public string[] HeroName = new string[] { };
                    public string[] WeaponType = new string[] { };
                    public string[] MoveType = new string[] { };
                    public Weapon[] weapon0 = new Weapon[] { };
                    public Weapon[] weapon1 = new Weapon[] { };
                    public Weapon[] weapon2 = new Weapon[] { };
                    public Weapon[] weapon3 = new Weapon[] { };
                    public Weapon[] weapon4 = new Weapon[] { };
                }

                public Printout printouts = new Printout();
            }

            public Dictionary<string, Result> results = new Dictionary<string, Result>();
        }

        public Query query = new Query();
    }    
}
