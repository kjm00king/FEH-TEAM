using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class ContainerField : BaseField, PMT.RE.IEntity
    {
        public ContainerField()
        {
            Init();
        }

        public ContainerField(int _sort)
        {
            Init();
            sort = _sort;
        }

        public ContainerField(string content)
        {
            Init();
            FromJson(content);
        }

        public void FromJson(string content)
        {
            PMT.RE.DictTmp tmp = new PMT.RE.DictTmp(content);
            sort = tmp.GetAsInt("0");
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(
                new PMT.RE.DictTmp(
                    "0", sort));
        }

        public override void Init()
        {
            base.Init();
            type = UnitType.Container;
            fieldtype = FieldType.Null;
        }
    }
}
