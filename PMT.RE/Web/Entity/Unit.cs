using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class Unit : PMT.RE.IEntity
    {
        public UnitType type { get; set; }
        public FieldType fieldtype { get; set; }
        public bool isMulti { get; set; }
        public bool isArrayContainer { get; set; }
        public bool isArrayElement { get; set; }
        public bool isMultilingual { get; set; }

        public string key { get; set; }
        public string parent { get; set; }
        public string name { get; set; }
        public int sort { get; set; }
        public int no { get; set; }

        public int lang { get; set; }
        public string content { get; set; }
        public string attribute { get; set; }

        //public bool tree_open { get; set; }
        //public bool tree_selected { get; set; }

        public Unit()
        {
            Init();
        }

        private void Init()
        {
            type = UnitType.Null;
            fieldtype = FieldType.Null;
            isMulti = false;
            isArrayContainer = false;
            isArrayElement = false;
            isMultilingual = false;
            key = string.Empty;
            parent = string.Empty;
            name = string.Empty;
            sort = 0;
            no = 0;
            lang = 0;
            content = string.Empty;
            attribute = string.Empty;

            //tree_open = false;
            //tree_selected = false;
        }

        public Unit(string content)
        {
            Init();
            FromJson(content);
        }

        public Unit(BaseField attr)
        {
            Init();
            FromAttr(attr);
        }

        public void FromJson(string content)
        {
            PMT.RE.ListTmp tmp = new PMT.RE.ListTmp(content);

            type = (UnitType)tmp.GetAsInt(0);
            fieldtype = (FieldType)tmp.GetAsInt(1);
            isMulti = tmp.GetAsBool(2);
            isMultilingual = tmp.GetAsBool(3);
            isArrayContainer = tmp.GetAsBool(4);
            isArrayElement = tmp.GetAsBool(5);

            key = tmp.Get(6);
            parent = tmp.Get(7);
            name = tmp.Get(8);
            sort = tmp.GetAsInt(9);
            no = tmp.GetAsInt(10);

            lang = tmp.GetAsInt(11);
            content = tmp.Get(12);
            attribute = tmp.Get(13);

        }

        public void FromAttr(BaseField attr)
        {
            fieldtype = attr.fieldtype;
            isMulti = attr.multi;
            isMultilingual = attr.multilingual;

            sort = attr.sort;


            PMT.RE.IEntity entity = (attr as PMT.RE.IEntity);
            if (entity != null)
                attribute = entity.ToString();

        }

        public override string ToString()
        {
            return new PMT.RE.ListTmp(
                    (int)type,
                    (int)fieldtype,
                    isMulti,
                    isMultilingual,
                    isArrayContainer,
                    isArrayElement,

                    key,
                    parent,
                    name,
                    sort,
                    no,

                    (int)lang,
                    content,
                    attribute).ToString();
        }        
    }
}
