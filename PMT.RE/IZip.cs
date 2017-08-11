using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public interface IZip
    {
        byte[] Zip();
        void FromZip(byte[] buff);
    }
}
