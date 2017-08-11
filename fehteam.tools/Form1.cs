using fehteam.dal;
using fehteam.wiki;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fehteam.tools
{
    public partial class Form1 : Form
    {
        const string API = "https://feheroes.gamepedia.com/api.php";
        
        public Form1()
        {
            InitializeComponent();

            lbWikiPath.Text = new DirectoryInfo("../../../fehteam.app/Content/").FullName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fehteam.dal.Wiki wikidb = new dal.Wiki();
                string wikicontent = CML.ComFile.Read(lbWikiPath.Text + "Wiki.data");
                wikidb.FromJson(wikicontent);

                updateWeapon(ref wikidb);

                updateHeroes(ref wikidb);

                updateHeroesStats(ref wikidb);

                updateHeroesIcon(wikidb, lbWikiPath.Text);

                CML.ComFile.Write(lbWikiPath.Text + "Wiki.data", wikidb.ToString());
                
                MessageBox.Show("Update Data Success!");
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        private string proxyip = "10.177.226.111";
        private int proxyport = 8090;

        private void updateWeapon(ref Wiki db)
        {
            var responseString = CML.ComUrl.Post(API, Param.QueryWeapon, proxyip, proxyport);
            QueryWeapons tmp = CML.ComUtility.FromJson<QueryWeapons>(responseString);

            foreach (string name in tmp.query.results.Keys)
            {
                Dictionary<string, object[]> props = tmp.query.results[name].printouts;

                dal.Weapon w = new dal.Weapon { Name = name };

                w.Damage = getPrintoutAsInt(props, "Might1");
                w.Effect = db.weaponeffects.And(getPrintoutAsStr(props, "Effect1")).Id;
                w.Range = getPrintoutAsInt(props, "Range1");
                w.Cost = getPrintoutAsInt(props, "Cost1");
                w.IsExclusive = getPrintoutAsStr(props, "Is exclusive").ToUpper().Equals("T");

                db.weapons.And(w);
            }
        }

        private int getPrintoutAsInt(Dictionary<string, object[]> dict, string key)
        {
            return CML.ComUtility.XInt(getPrintoutAsStr(dict, key), 0);
        }

        private string getPrintoutAsStr(Dictionary<string, object[]> dict, string key)
        {
            if (dict.ContainsKey(key))
            {
                object[] values = dict[key];
                if (values.Length > 0)
                    return values[0].ToString();
            }
            return string.Empty;
        }        

        private void updateHeroes(ref Wiki db)
        {
            var responseString = CML.ComUrl.Post(API, Param.QueryHeroes, proxyip, proxyport);
            QueryHeroes tmp = CML.ComUtility.FromJson<QueryHeroes>(responseString);

            List<Hero> rv = new List<Hero>();
            foreach(string name in tmp.query.results.Keys)
            {
                QueryHeroes.Query.Result.Printout props = tmp.query.results[name].printouts;

                Hero h = new Hero() { Name = name };

                if (props.WeaponType.Length > 0)
                {
                    h.WeaponClassId = db.weaponclasses.And(props.WeaponType[0]).Id;
                }

                if (props.MoveType.Length > 0)
                {
                    h.MoveTypeId = db.movetypes.And(props.MoveType[0]).Id;
                }

                IEnumerable<dal.Weapon> weapons = db.weapons.Query();
                
                if (props.weapon0.Length > 0)
                {
                    h.Weapon[0] = CML.ComUtility.QueryFirst(from x in weapons where x.Name == props.weapon0[0].fulltext select x.Id, 0);
                }

                if (props.weapon1.Length > 0)
                {
                    h.Weapon[1] = CML.ComUtility.QueryFirst(from x in weapons where x.Name == props.weapon1[0].fulltext select x.Id, 0);
                }

                if (props.weapon2.Length > 0)
                {
                    h.Weapon[2] = CML.ComUtility.QueryFirst(from x in weapons where x.Name == props.weapon2[0].fulltext select x.Id, 0);
                }

                if (props.weapon3.Length > 0)
                {
                    h.Weapon[3] = CML.ComUtility.QueryFirst(from x in weapons where x.Name == props.weapon3[0].fulltext select x.Id, 0);
                }

                if (props.weapon4.Length > 0)
                {
                    h.Weapon[4] = CML.ComUtility.QueryFirst(from x in weapons where x.Name == props.weapon4[0].fulltext select x.Id, 0);
                }

                db.heroes.And(h);
            }
        }

        private void updateHeroesIcon(Wiki db, string dirpath)
        {
            for (int i = 0; i < db.heroes.Count; i++)
            {
                string name = db.heroes[i].Name;
                string path = string.Format(@"{0}Wiki\{1}.png", dirpath, db.heroes[i].Id);

                if (!File.Exists(path))
                {
                    var responseString = CML.ComUrl.Get(API, Param.QueryHeroIcon(name), proxyip, proxyport);
                    QueryHeroIcon tmp = CML.ComUtility.FromJson<QueryHeroIcon>(responseString);

                    foreach (string key in tmp.query.pages.Keys)
                    {
                        Stream stream = CML.ComUrl.Download(tmp.query.pages[key].imageinfo[0].url, proxyip, proxyport);
                        using (var fs = new FileStream(path, FileMode.CreateNew))
                        {
                            stream.CopyTo(fs);
                        }
                        break;
                    }
                }
            }
        }

        private void updateHeroesStats(ref Wiki db)
        {
            var responseString = CML.ComUrl.Post(API, Param.QueryHeroesStats, proxyip, proxyport);
            QueryHeroesStats tmp =  CML.ComUtility.FromJson<QueryHeroesStats>(responseString);

            int[] Lvs = new int[] { 1, 40 };
            int[] Raritys = new int[] { 3, 4, 5 };
            string[] Props = new string[] { "HP", "ATK", "SPD", "DEF", "RES" };
            string[] Vars = new string[] { "Bane", "Neut", "Boon" };
            
            foreach (string name in tmp.query.results.Keys)
            {
                dal.Hero h = CML.ComUtility.QueryFirst(from x in db.heroes.Query() where x.Name == name select x, null);
                if (h !=null)
                {
                    foreach (int Lv in Lvs)
                    {
                        foreach (int Rarity in Raritys)
                        {
                            for (int i = 0; i < Vars.Length; i++)
                            {
                                dal.HeroEntity entity = new dal.HeroEntity()
                                {
                                    Level = Lv,
                                    Rarity = Rarity,
                                    HeroId = h.Id,
                                    Variation = i - 1,
                                };

                                List<int> PropValue = new List<int>();

                                Dictionary<string, string[]> prints = tmp.query.results[name].printouts;

                                foreach (string Prop in Props)
                                {
                                    string key = string.Format("Lv{0}R{1}{2}{3}", Lv, Rarity, Prop, Vars[i]);
                                    if (prints.Keys.Contains(key) && prints[key].Length > 0)
                                    {
                                        if (prints[key].Length > 1)
                                        {
                                            Console.WriteLine("TooMany!");
                                        }
                                        PropValue.Add(CML.ComUtility.XInt(prints[key][0], 0));
                                    }
                                    else
                                        PropValue.Add(0);
                                }

                                entity.HP = PropValue[0];
                                entity.ATK = PropValue[1];
                                entity.SPD = PropValue[2];
                                entity.DEF = PropValue[3];
                                entity.RES = PropValue[4];

                                dal.Weapon w = CML.ComUtility.QueryFirst(from x in db.weapons.Query()
                                                                         where x.Id == h.Weapon[Rarity - 1]
                                                                         select x, null);

                                if (w != null)
                                {
                                    entity.W_ATK = w.Damage;
                                    if (w.Effect == 15)
                                    {
                                        //Console.WriteLine(w.Name);
                                        entity.W_SPD = -5;
                                    }
                                }

                                if (entity.HP > 0 || entity.ATK > 0 || entity.SPD > 0 || entity.DEF > 0 || entity.RES > 0)
                                {
                                    if (entity.HP > 0 && entity.ATK > 0 && entity.SPD > 0 && entity.DEF > 0 && entity.RES > 0)
                                    {
                                        db.stats.And(entity);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Has Zero!");

                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            fehteam.dal.Wiki wikidb = new dal.Wiki();
            string wikicontent = CML.ComFile.Read(lbWikiPath.Text + "Wiki.data");
            wikidb.FromJson(wikicontent);

            db.SqlDataContext DB = new tools.db.SqlDataContext();

            DB.ExecuteCommand("TRUNCATE TABLE dbo.Hero");
            DB.ExecuteCommand("TRUNCATE TABLE dbo.HeroEntity");
            DB.ExecuteCommand("TRUNCATE TABLE dbo.Weapon");
            DB.ExecuteCommand("TRUNCATE TABLE dbo.KV");
            //DB.ExecuteCommand("TRUNCATE TABLE dbo.KVType", null);
            DB.ExecuteCommand("TRUNCATE TABLE dbo.HeroDefWeapon");

            foreach (dal.Hero it in wikidb.heroes)
            {
                DB.Heros.InsertOnSubmit(new db.Hero()
                {
                    Id = it.Id,
                    Move = it.MoveTypeId,
                    WeaponClass = it.WeaponClassId,
                    Name = it.Name,
                });

                for (int i = 0; i < it.Weapon.Length; i++)
                {
                    DB.HeroDefWeapons.InsertOnSubmit(new db.HeroDefWeapon()
                    {
                        Hero = it.Id,
                        Rarity = i + 1,
                        Weapon = it.Weapon[i],
                    });
                }
            }

            foreach (dal.Weapon it in wikidb.weapons)
            {
                DB.Weapons.InsertOnSubmit(new db.Weapon()
                {
                    Id = it.Id,
                    Damage = it.Damage,
                    Cost = it.Cost,
                    Effect = it.Effect,
                    IsExclusive = it.IsExclusive,
                    Name = it.Name,
                    Range = it.Range,
                });
            }

            foreach (dal.HeroEntity it in wikidb.stats)
            {
                DB.HeroEntities.InsertOnSubmit(new db.HeroEntity()
                {
                    Id = it.Id,
                    Hero = it.HeroId,
                    Rarity = it.Rarity,
                    Level = it.Level,
                    Variation = it.Variation,
                    ATK = it.ATK,
                    HP = it.HP,
                    SPD = it.SPD,
                    DEF = it.DEF,
                    RES = it.RES,
                    W_ATK = it.W_ATK,
                    W_SPD = it.W_SPD,
                });
            }

            foreach (dal.KeyValue it in wikidb.weaponclasses)
            {
                DB.KVs.InsertOnSubmit(new db.KV()
                {
                    Type = 1,
                    Name = it.Name,
                    Id = it.Id,
                });
            }

            foreach (dal.KeyValue it in wikidb.movetypes)
            {
                DB.KVs.InsertOnSubmit(new db.KV()
                {
                    Type = 2,
                    Name = it.Name,
                    Id = it.Id,
                });
            }

            foreach (dal.KeyValue it in wikidb.weaponeffects)
            {
                DB.KVs.InsertOnSubmit(new db.KV()
                {
                    Type = 3,
                    Name = it.Name,
                    Id = it.Id,
                });
            }

            DB.SubmitChanges();

            //CML.ComFile.Renew(lbWikiPath.Text + "Wiki.js",
            //    "var mdb =" + CML.ComUtility.ToJson(new
            //    {
            //        heroes = wikidb.heroes,
            //        stats = wikidb.stats,
            //        weapons = wikidb.weapons,
            //        weapontype = wikidb.weaponclasses.ToDict(),
            //        move = wikidb.movetypes.ToDict(),
            //    }));

            MessageBox.Show("Backup Success!");
        }

        private void ckProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (ckProxy.Checked)
            {
                proxyip= "10.177.226.111";
                proxyport = 8090;
            }
            else
            {
                proxyip = null;
                proxyport = 0;
            }
        }
    }
}
