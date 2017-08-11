using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class ImgField : BaseField, PMT.RE.IEntity
    {
        public ImgField()
        {
            Init();
        }

        public ImgField(int _sort)
        {
            Init();
            sort = _sort;
        }

        public ImgField(string content)
        {
            Init();
            FromJson(content);
        }

        public void FromJson(string content)
        {
            PMT.RE.DictTmp tmp = new PMT.RE.DictTmp(content);
            sort = tmp.GetAsInt("0");
            multi = tmp.GetAsBool("1");
            multilingual = tmp.GetAsBool("2");
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(
                new PMT.RE.DictTmp(
                    "0", sort,
                    "1", multi,
                    "2", multilingual));
        }

        public override void Init()
        {
            base.Init();
            type = UnitType.Field;
            fieldtype = FieldType.Img;
        }
    }
}
