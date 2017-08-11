using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class TextField : BaseField, PMT.RE.IEntity
    {
        public string @default { get; set; }

        public TextField()
        {
            Init();
        }

        public TextField(int _sort)
        {
            Init();
            sort = _sort;
        }

        public TextField(string content)
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
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(
                new PMT.RE.DictTmp(
                    "0", sort,
                    "1", @default,
                    "2", multilingual));
        }

        public override void Init()
        {
            base.Init();
            type = UnitType.Field;
            fieldtype = FieldType.Text;
            @default = string.Empty;
        }
    }
}
