using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public class Entities<T> : List<T>, PMT.RE.IEntity
    {
        public Entities() { }

        public Entities(string content)
        {
            FromJson(content);
        }

        public virtual void FromJson(string content)
        {
            Clear();
            PMT.RE.ListTmp tmp = new PMT.RE.ListTmp(content);
            for (int i = 0; i < tmp.Count; i++)
            {
                PMT.RE.IEntity entity = Activator.CreateInstance(typeof(T)) as PMT.RE.IEntity;
                if (entity != null)
                {
                    entity.FromJson(tmp[i]);
                    Add((T)entity);
                }
            }
        }

        public override string ToString()
        {
            PMT.RE.ListTmp tmp = new PMT.RE.ListTmp();
            for (int i = 0; i < Count; i++)
            {
                tmp.Add(this[i].ToString());
            }
            return CML.ComUtility.ToJson(tmp);
        }
    }
}
