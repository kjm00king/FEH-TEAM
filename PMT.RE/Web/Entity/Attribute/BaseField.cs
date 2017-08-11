using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class BaseField : Attribute
    {
        public UnitType type { get; set; }
        public FieldType fieldtype { get; set; }
        public int sort { get; set; }
        public bool multilingual { get; set; }
        public bool multi { get; set; }

        public virtual void Init()
        {
            type = UnitType.Null;
            fieldtype = FieldType.Null;
            sort = 0;
            multilingual = false;
            multi = false;
        }

        public BaseField()
        {
            Init();
        }
    }
}
