using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT.RE;

namespace fehteam.dal
{
    public class Hero : IEntity
    {
        public string Name = string.Empty;
        public int WeaponClassId = 0;
        public int MoveTypeId = 0;
        public int Id = 0;
        public int[] Weapon = new int[] { 0, 0, 0, 0, 0 };

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Id = tmp.GetAsInt(0);
            Name = tmp.Get(1);
            WeaponClassId = tmp.GetAsInt(2);
            MoveTypeId = tmp.GetAsInt(3);
            Weapon[0] = tmp.GetAsInt(4);
            Weapon[1] = tmp.GetAsInt(5);
            Weapon[2] = tmp.GetAsInt(6);
            Weapon[3] = tmp.GetAsInt(7);
            Weapon[4] = tmp.GetAsInt(8);
        }

        public override string ToString()
        {
            return new ListTmp(Id, Name, WeaponClassId, MoveTypeId
                , Weapon[0]
                , Weapon[1]
                , Weapon[2]
                , Weapon[3]
                , Weapon[4])
                .ToString();
        }
    }

    public class Heroes : List<Hero>, IEntity
    {
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Clear();
            for (int i = 0; i < tmp.Count; i++)
            {
                Hero it = new Hero();
                it.FromJson(tmp[i]);
                Add(it);
            }
        }

        public override string ToString()
        {
            return new ListTmp(this).ToString();
        }

        public void And(List<Hero> they)
        {
            foreach (Hero it in they)
                And(it);
        }

        public void And(Hero it)
        {
            if (Count == 0)
            {
                Add(new Hero()
                {
                    Name = it.Name,
                    Id = 1,
                    WeaponClassId = it.WeaponClassId,
                    MoveTypeId = it.MoveTypeId,
                    Weapon = new int[] { it.Weapon[0], it.Weapon[1], it.Weapon[2], it.Weapon[3], it.Weapon[4] },
                });
            }
            else
            {
                Hero now = CML.ComUtility.QueryFirst(from x in this where x.Name == it.Name select x, null);

                if (now == null)
                {
                    Add(new Hero()
                    {
                        Name = it.Name,
                        Id = this.Max(o => o.Id) + 1,
                        WeaponClassId = it.WeaponClassId,
                        MoveTypeId = it.MoveTypeId,
                        Weapon = new int[] { it.Weapon[0], it.Weapon[1], it.Weapon[2], it.Weapon[3], it.Weapon[4] },
                    });
                }
                else
                {

                    now.WeaponClassId = it.WeaponClassId;
                    now.MoveTypeId = it.MoveTypeId;
                    now.Weapon = new int[] { it.Weapon[0], it.Weapon[1], it.Weapon[2], it.Weapon[3], it.Weapon[4] };
                }
            }
        }

        public IEnumerable<Hero> Query()
        {
            return this.AsEnumerable<Hero>();
        }
    }
}
