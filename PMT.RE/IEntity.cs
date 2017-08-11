using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public interface IEntity
    {
        void FromJson(string content);
    }
}
