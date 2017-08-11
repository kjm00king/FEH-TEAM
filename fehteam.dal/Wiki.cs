using PMT.RE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.dal
{
    public class Wiki : IEntity
    {
        public Heroes heroes = new Heroes();
        public KeyValueArray weaponclasses = new KeyValueArray();
        public Weapons weapons = new Weapons();
        public KeyValueArray movetypes = new KeyValueArray();
        public HeroEntities stats = new HeroEntities();
        public KeyValueArray weaponeffects = new KeyValueArray();

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            heroes = new Heroes();
            stats = new HeroEntities();
            weaponclasses = new KeyValueArray();
            movetypes = new KeyValueArray();
            weapons = new Weapons();
            heroes.FromJson(tmp.Get(0));
            stats.FromJson(tmp.Get(1));
            weaponclasses.FromJson(tmp.Get(2));
            movetypes.FromJson(tmp.Get(3));
            weapons.FromJson(tmp.Get(4));
        }

        public override string ToString()
        {
            return new ListTmp(heroes, stats, weaponclasses, movetypes, weapons).ToString();
        }
        
        //public class MyStats
        //{
        //    public int minR = 0;
        //    public bool hasVar = false;
        //    public Dictionary<int, Dictionary<int, List<int>>> value = new Dictionary<int, Dictionary<int, List<int>>>();
        //}

        //public Dictionary<int,MyStats> GetStatsDic()
        //{
        //    Dictionary<int, MyStats> rv = new Dictionary<int, MyStats>();

        //    foreach(Hero h in heroes)
        //    {
        //        IEnumerable<HeroEntity> L1R3Bane = (from x in stats where x.HeroId == h.Id select x);

        //    }

        //    return rv;
        //}
    }
}
