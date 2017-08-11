using PMT.RE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.dal
{
    public class UserData
    {
        public string Key = string.Empty;
        
        public class Hero
        {
            public int id = 0;
            public int sort = 0;
            public int hid = 0;
            public int r = 0;
            public int lv = 0;
            public int[] var = new int[] { 0, 0, 0, 0, 0 };
            public string hl = "";
            public string hr = "";
            public string hp = "";

            public bool error
            {
                get
                {
                    if (var.Contains(0))
                    {
                        return true;
                    }
                    else
                    {
                        int n = var.Count(o => o.Equals(1));
                        int p = var.Count(o => o.Equals(3));
                        if (n > 1 || p > 1 || n != p)
                            return true;
                    }

                    return false;
                }
            }

            public void Mod(Hero h)
            {
                r = h.r;
                lv = h.lv;
                var = h.var;
                hl = h.hl;
                hr = h.hr;
                hp = h.hp;
            }
        }

        public List<Hero> Team = new List<Hero>();

        public List<Hero> Trash = new List<Hero>();

        public void Add(Hero h)
        {
            if (Team.Count == 0)
                h.sort = h.id = 1;
            else
                h.sort = h.id = Team.Max(o => o.id) + 1;
            Team.Add(h);
        }
        
        public Hero Mod(Hero h)
        {
            Hero mod = CML.ComUtility.QueryFirst(from x in Team where x.id == h.id select x, null);
            if (mod != null)
            {
                mod.Mod(h);
                return mod;
            }
            else
                return null;
        }

        public Hero Del(int id)
        {
            Hero del = CML.ComUtility.QueryFirst(from x in Team where x.id == id select x, null);
            if (del != null)
            {
                Team.Remove(del);
                Trash.Add(del);
                return del;
            }
            else
                return null;
        }        
    }
}
