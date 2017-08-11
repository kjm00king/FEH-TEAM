using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VInt : PMT.RE.IEntity
    {
        public static readonly VInt Default = new VInt(0);
        private int value;
        public VInt() { value = 0; }
        public VInt(int v) { value = v; }

        public void FromJson(string content)
        {
            value = CML.ComUtility.XInt(content, 0);
        }

        public static implicit operator VInt(int v)
        {
            return new VInt(v);
        }
        public static implicit operator string(VInt v)
        {
            return v.ToString();
        }
    }
}
