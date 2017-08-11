using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public class LifeScope : IEntity
    {
        public Guid uuid = Guid.NewGuid();
        public DateTime create = DateTime.Now;
        public Entities<DateTime> use = new Entities<DateTime>();

        public LifeScope()
        {

        }

        public LifeScope(string content)
        {
            FromJson(content);
            use.Add(DateTime.Now);
        }

        public override string ToString()
        {
            return new ListTmp(uuid, create, use).ToString();
        }

        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            uuid = tmp.GetAsGuid(0, uuid);
            create = tmp.GetAsDateTime(1, create);
            use = tmp.GetAsEntity<Entities<DateTime>>(2);
        }
    }
}
