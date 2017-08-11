using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class VPic : PMT.RE.IEntity
    {
        public static readonly VPic Default = new VPic();

        public string src;
        public float width;
        public float height;

        public VPic()
        {
            FromJson(string.Empty);
        }

        public VPic(string content)
        {
            FromJson(content);
        }

        public void FromJson(string content)
        {
            PMT.RE.ListTmp tmp = new PMT.RE.ListTmp();
            src = tmp.Get(0);
            width = tmp.GetAsFloat(1);
            height = tmp.GetAsFloat(2);
        }

        public override string ToString()
        {
            return new PMT.RE.ListTmp(src, width, height).ToString();
        }

        public static implicit operator VPic(string v)
        {
            return new VPic() { src = v };
        }

        public static implicit operator string(VPic v)
        {
            return v.src;
        }
    }
}
