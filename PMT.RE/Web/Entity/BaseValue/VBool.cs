using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VBool : IEntity
    {
        public static readonly VBool Default = new VBool(false);
        private bool value;
        public VBool() { value = false; }
        public VBool(bool v) { value = v; }

        public void FromJson(string content)
        {
            value = CML.ComUtility.XBool(content, false);
        }

        public static implicit operator VBool(bool v)
        {
            return new VBool(v);
        }
        public static implicit operator string(VBool v)
        {
            return v.ToString();
        }
    }
}
