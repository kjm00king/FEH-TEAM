using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT.RE;

namespace fehteam.dal
{
    public class KeyValue : IEntity
    {
        public string Name = string.Empty;
        public int Id = 0;

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Id = tmp.GetAsInt(0);
            Name = tmp.Get(1);
        }

        public override string ToString()
        {
            return new ListTmp(Id, Name).ToString();
        }
    }

    public class KeyValueArray : List<KeyValue>, IEntity
    {
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Clear();
            for (int i = 0; i < tmp.Count; i++)
            {
                KeyValue it = new KeyValue();
                it.FromJson(tmp[i]);
                Add(it);
            }
        }

        public override string ToString()
        {
            return new ListTmp(this).ToString();
        }

        public void And(List<KeyValue> they)
        {
            foreach (KeyValue it in they)
                And(it);
        }

        public KeyValue And(KeyValue it)
        {
            KeyValue item = new KeyValue() { Name = it.Name };
            if (Count == 0)
            {
                item.Id = 1;
                Add(item);
                return item;
            }
            else
            {
                KeyValue check = CML.ComUtility.QueryFirst(
                    from x in this
                    where x.Name == it.Name
                    select x, null);

                if (check == null)
                {
                    item.Id = this.Max(o => o.Id) + 1;
                    Add(item);
                    return item;

                }
                else
                    return check;

            }

        }

        public KeyValue And(string name)
        {
            return And(new KeyValue() { Name = name });
        }

        public Dictionary<int,string> ToDict()
        {
            Dictionary<int, string> tmp = new Dictionary<int, string>();
            foreach (KeyValue it in this)
                tmp.Add(it.Id, it.Name);
            return tmp;
        }
    }
}
