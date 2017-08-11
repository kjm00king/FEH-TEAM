using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VPicArray : Entities<VPic>
    {
        public VPicArray() : base() { }
        public VPicArray(string content) : base(content) { }
    }
}
