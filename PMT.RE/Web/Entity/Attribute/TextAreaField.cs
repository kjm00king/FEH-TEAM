using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class TextAreaField : BaseField, PMT.RE.IEntity
    {
        public string @default { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public float width { get; set; }
        public float height { get; set; }

        public TextAreaField()
        {
            Init();
        }

        public TextAreaField(int _sort)
        {
            Init();
            sort = _sort;
        }

        public TextAreaField(string content)
        {
            Init();
            FromJson(content);
        }

        public void FromJson(string content)
        {
            PMT.RE.DictTmp tmp = new PMT.RE.DictTmp(content);
            sort = tmp.GetAsInt("0");
            @default = tmp.Get("1");
            multilingual = tmp.GetAsBool("2");
            row = tmp.GetAsInt("3");
            col = tmp.GetAsInt("4");
            width = tmp.GetAsFloat("5");
            height = tmp.GetAsFloat("6");
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(
                new PMT.RE.DictTmp(
                    "0", sort,
                    "1", @default,
                    "2", multilingual,
                    "3", row,
                    "4", col,
                    "5", width,
                    "6", height));
        }

        public override void Init()
        {
            base.Init();
            type = UnitType.Field;
            fieldtype = FieldType.TextArea;
            @default = string.Empty;
            row = 0;
            col = 0;
            width = 0;
            height = 0;
        }
    }
}
