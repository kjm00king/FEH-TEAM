using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VFloat : PMT.RE.IEntity
    {
        public static readonly VFloat Default = new VFloat(0);
        private float value;
        public VFloat() { value = 0; }
        public VFloat(float v) { value = v; }

        public void FromJson(string content)
        {
            value = CML.ComUtility.XFloat(content, 0);
        }

        public static implicit operator VFloat(float v)
        {
            return new VFloat(v);
        }
        public static implicit operator string(VFloat v)
        {
            return v.ToString();
        }
    }
}
