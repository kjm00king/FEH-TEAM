using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class Units : Entities<Unit>
    {
        public Units() : base() { }
        public Units(string content) : base(content) { }
    }
}
