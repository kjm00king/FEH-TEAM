using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fehteam.app.Bll
{
    public class session
    {
        public static bool ExistUserData(HttpContextBase context)
        {
            return (context.Session["MyData"] != null);
        }
        
        public static fehteam.dal.UserData GetUserData(HttpContextBase context)
        {
            if (context.Session["MyData"] == null)
            {
                context.Session["MyData"] = new dal.UserData();

                //string content = CML.ComFile.Read(Bll.webcache.DbPath + @"Users\default.data");
                //dal.UserData tmp = CML.ComUtility.FromJson<dal.UserData>(content);
                //if (tmp == null) {
                //    context.Session["MyData"] = new dal.UserData();
                //}
                //else
                //    context.Session["MyData"] = tmp;
            }
            return context.Session["MyData"] as fehteam.dal.UserData;
        }
        
        public static void SetUserData(HttpContextBase context, dal.UserData data)
        {
            context.Session["MyData"] = data;
        }

        public static void SaveUserData(HttpContextBase context)
        {
            if (context.Session["MyData"] != null)
            {
                CML.ComFile.Renew(
                    Bll.webcache.DbPath + @"Users\default.data",
                    CML.ComUtility.ToJson(context.Session["MyData"]));
            }
        }

        public static string GetUserDataRaw(HttpContextBase context)
        {
            return CML.ComUtility.ToJson(GetUserData(context));
        }

        public static IEnumerable<dal.Hero> GetMyHeroes(HttpContextBase context)
        {
            List<int> index = CML.ComUtility.QueryList(
                    (from x in GetUserData(context).Team
                     select x.hid).Distinct());

            List<dal.Hero> rv = CML.ComUtility.QueryList(
                from x in webcache.wiki.heroes
                where index.Contains(x.Id)
                select x);

            return rv;
        }

        public class TRE    //TeamReportEntity
        {
            public class Prop
            {
                public int v = 0;
                public string n = "--";
                public string c = "unkown";
                public int d = 0;
            }
            
            public int id = 0;
            public int H = 0;
            public string N = string.Empty;
            public int W = 0;
            public int M = 0;
            public int L = 0;
            public int R = 0;
            public int P = 0;
            
            public Prop Hp = new Prop();
            public Prop Atk = new Prop();
            public Prop Spd = new Prop();
            public Prop Def = new Prop();
            public Prop Res = new Prop();
        }

        public static void SetTreProp(int h, int r, int var, Func<dal.HeroEntity,int> sel, ref TRE.Prop prop)
        {

            if (var > 0)
            {
                int max_r_max_lv_my_prop = CML.ComUtility.QueryFirst((from x in webcache.wiki.stats
                                                                      where x.HeroId == h
                                                                      where x.Rarity == 5
                                                                      where x.Level == 40
                                                                      where x.Variation == (var - 2)
                                                                      select x).Select(sel), 0);
                int max_r_max_lv_neut_prop = CML.ComUtility.QueryFirst((from x in webcache.wiki.stats
                                                                       where x.HeroId == h
                                                                       where x.Rarity == 5
                                                                       where x.Level == 40
                                                                       where x.Variation == 0
                                                                        select x).Select(sel), 0);
                int my_r_max_lv_my_prop = CML.ComUtility.QueryFirst((from x in webcache.wiki.stats
                                                                    where x.HeroId == h
                                                                    where x.Rarity == r
                                                                    where x.Level == 40
                                                                    where x.Variation == (var - 2)
                                                                     select x).Select(sel), 0);
                int P0 = max_r_max_lv_my_prop;
                int P1 = max_r_max_lv_my_prop - max_r_max_lv_neut_prop;
                int P2 = my_r_max_lv_my_prop - max_r_max_lv_my_prop;

                prop.v = P0;
                prop.n = P0 > 0 ? P0.ToString() : "";
                switch (P1)
                {
                    case -4: prop.c = "n4"; break;
                    case -3: prop.c = "n3"; break;
                    case 3: prop.c = "p4"; break;
                    case 4: prop.c = "p4"; break;
                    default: prop.c = P0 > 0 ? "neut" : "unkown"; break;
                }
                prop.d = (P0 > 0 && P2 < 0) ? P2 : 0;
            }
        }

        public static List<TRE> GetTreArray(HttpContextBase context)
        {
            dal.UserData user = GetUserData(context);
            
            List<TRE> tmp = new List<TRE>();

            foreach (dal.UserData.Hero h in user.Team)
            {
                TRE it = GetTre(h);

                tmp.Add(it);
            }

            return tmp;
        }

        public static TRE GetTre(dal.UserData.Hero h)
        {

            TRE it = new TRE()
            {
                id = h.id,
                H = h.hid,
                L = CML.ComUtility.XInt(h.hl.ToUpper().Replace("LV", ""), 0),
                R = CML.ComUtility.XInt(h.hr.ToUpper().Replace("R", ""), 0),
                P = CML.ComUtility.XInt(h.hp.ToUpper().Replace("+", ""), 0),
            };


            dal.Hero hero = CML.ComUtility.QueryFirst(from x in Bll.webcache.wiki.heroes where x.Id == it.H select x, null);
            if (hero != null)
            {
                it.N = hero.Name;
                it.W = hero.WeaponClassId;
                it.M = hero.MoveTypeId;
            }

            SetTreProp(it.H, it.R, h.var[0], (o => o.HP), ref it.Hp);
            SetTreProp(it.H, it.R, h.var[1], (o => o.ATK), ref it.Atk);
            SetTreProp(it.H, it.R, h.var[2], (o => o.SPD), ref it.Spd);
            SetTreProp(it.H, it.R, h.var[3], (o => o.DEF), ref it.Def);
            SetTreProp(it.H, it.R, h.var[4], (o => o.RES), ref it.Res);
            
            return it;
        }
    }
}