using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.wiki
{
    public class Param
    {
        public static string QueryHeroes
        {
            get
            {
                return "action=ask&query=[[Category:Heroes]]|?HeroName|?WeaponType|?MoveType|?Has weapon0=weapon0|?Has weapon1=weapon1|?Has weapon2=weapon2|?Has weapon3=weapon3|?Has weapon4=weapon4|limit=500&format=json";
            }
        }

        public static string QueryWeapon
        {
            get
            {
                return "action=ask&query=[[Category:Weapons]]|?might1|?effect1|?range1|?cost1|?Is%20exclusive|limit=500&format=json";
            }
        }

        public static string QueryHeroIcon(string heroname)
        {
            return string.Format("action=query&prop=imageinfo&iiprop=url&titles=File:Icon_Portrait_{0}.png&format=json", heroname);
        }

        public static string QueryHeroesStats
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("action=ask&query=[[Category:Heroes]]|limit=500");

                string[] Lvs = new string[] { "Lv1", "Lv40" };
                string[] Raritys = new string[] { "R3", "R4", "R5" };
                string[] Props = new string[] { "HP", "ATK", "SPD", "DEF", "RES" };
                string[] Vars = new string[] { "Bane", "Neut", "Boon" };

                foreach (string Lv in Lvs)
                {
                    foreach (string Rarity in Raritys)
                    {
                        foreach (string Prop in Props)
                        {
                            foreach (string Var in Vars)
                            {
                                sb.AppendFormat("|?Has {0} {1} {2} {3}={0}{1}{2}{3}", Lv, Rarity, Prop, Var);
                            }
                        }
                    }
                }
                sb.Append("&format=json");

                return sb.ToString();
            }
        }
    }
}
