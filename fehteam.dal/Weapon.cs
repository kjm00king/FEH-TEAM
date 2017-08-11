using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT.RE;

namespace fehteam.dal
{
    public class Weapon : IEntity
    {
        public int Id = 0;
        public string Name = string.Empty;
        public int Damage = 0;
        public int Effect = 0;
        public int Range = 0;
        public int Cost = 0;
        public bool IsExclusive = true;

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Id = tmp.GetAsInt(0);
            Name = tmp.Get(1);
            Damage = tmp.GetAsInt(2);
            Effect = tmp.GetAsInt(3);
            Range = tmp.GetAsInt(4);
            Cost = tmp.GetAsInt(5);
            IsExclusive = tmp.GetAsBool(6);
        }

        public override string ToString()
        {
            return new ListTmp(Id, Name, Damage, Effect, Range, Cost, IsExclusive).ToString();
        }
    }

    public class Weapons : List<Weapon>, IEntity
    {
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Clear();
            for (int i = 0; i < tmp.Count; i++)
            {
                Weapon it = new Weapon();
                it.FromJson(tmp[i]);
                Add(it);
            }
        }

        public override string ToString()
        {
            return new ListTmp(this).ToString();
        }

        public void And(List<Weapon> they)
        {
            foreach (Weapon it in they)
                And(it);
        }

        public void And(Weapon it)
        {
            if (Count == 0)
            {
                Add(new Weapon()
                {
                    Name = it.Name,
                    Id = 1,
                    Damage = it.Damage,
                    Cost = it.Cost,
                    Effect = it.Effect,
                    IsExclusive = it.IsExclusive,
                    Range = it.Range,
                });
            }
            else
            {
                Weapon now = CML.ComUtility.QueryFirst(from x in this where x.Name == it.Name select x, null);

                if (now == null)
                {
                    Add(new Weapon()
                    {
                        Name = it.Name,
                        Id = this.Max(o => o.Id) + 1,
                        Damage = it.Damage,
                        Cost = it.Cost,
                        Effect = it.Effect,
                        IsExclusive = it.IsExclusive,
                        Range = it.Range,
                    });
                }
                else
                {
                    now.Damage = it.Damage;
                    now.Cost = it.Cost;
                    now.Effect = it.Effect;
                    now.IsExclusive = it.IsExclusive;
                    now.Range = it.Range;

                }
            }
        }

        public IEnumerable<Weapon> Query()
        {
            return this.AsEnumerable<Weapon>();
        }
    }
}
