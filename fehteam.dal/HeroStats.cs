using PMT.RE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fehteam.dal
{
    public class HeroEntity : IEntity
    {
        public int Id = 0;
        public int HeroId = 0;
        public int Level = 0;
        public int Rarity = 0;
        public int Variation = 0;
        public int HP = 0;
        public int ATK = 0;
        public int SPD = 0;
        public int DEF = 0;
        public int RES = 0;
        public int W_ATK = 0;
        public int W_SPD = 0;

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Id = tmp.GetAsInt(0);
            HeroId = tmp.GetAsInt(1);
            Level = tmp.GetAsInt(2);
            Rarity = tmp.GetAsInt(3);
            Variation = tmp.GetAsInt(4);
            HP = tmp.GetAsInt(5);
            ATK = tmp.GetAsInt(6);
            SPD = tmp.GetAsInt(7);
            DEF = tmp.GetAsInt(8);
            RES = tmp.GetAsInt(9);
            W_ATK = tmp.GetAsInt(10);
            W_SPD = tmp.GetAsInt(11);
        }

        public override string ToString()
        {
            return new ListTmp(
                Id, HeroId, Level, Rarity, Variation, HP, ATK, SPD, DEF, RES, W_ATK, W_SPD)
                .ToString();
        }
    }
    
    public class HeroEntities : List<HeroEntity>, IEntity
    {
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Clear();
            for (int i = 0; i < tmp.Count; i++)
            {
                HeroEntity it = new HeroEntity();
                it.FromJson(tmp[i]);
                Add(it);
            }
        }

        public override string ToString()
        {
            return new ListTmp(this).ToString();
        }
        
        public void And(List<HeroEntity> they)
        {
            foreach (HeroEntity it in they)
                And(it);
        }

        public void And(HeroEntity it)
        {
            HeroEntity add = new HeroEntity();

            if (Count == 0)
            {
                add.FromJson(it.ToString());
                add.Id = 1;
                Add(add);
            }
            else
            {
                HeroEntity now = CML.ComUtility.QueryFirst<HeroEntity>(
                    from x in this
                    where x.HeroId == it.HeroId
                    where x.Level == it.Level
                    where x.Rarity == it.Rarity
                    where x.Variation == it.Variation
                    select x, null);

                if (now == null)
                {
                    add.FromJson(it.ToString());
                    add.Id = this.Max(o => o.Id) + 1;
                    Add(add);
                }
                else
                {
                    now.HP = it.HP;
                    now.ATK = it.ATK;
                    now.SPD = it.SPD;
                    now.DEF = it.DEF;
                    now.RES = it.RES;
                    now.W_ATK = it.W_ATK;
                    now.W_SPD = it.W_SPD;
                }
            }
        }
        
    }
}
