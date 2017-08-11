using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VString : IEntity
    {
        public static readonly VString Default = new VString(string.Empty);
        private string value;
        public VString() { value = string.Empty; }
        public VString(string v) { value = v; }

        public void FromJson(string content)
        {
            value = (content == null ? String.Empty : content);
        }

        public static implicit operator VString(string v)
        {
            return new VString(v);
        }
        public static implicit operator string(VString v)
        {
            return v.ToString();
        }
    }
}
