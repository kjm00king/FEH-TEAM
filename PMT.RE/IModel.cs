using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public interface IModel : IEntity
    {
        //string this[string name] { get; }

        ISrvProvider owner { set; }

        //object Exec(string name, params object[] arg);
    }
}
