using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public abstract class Event
    {
        protected Dictionary<string, Delegate> listeners = new Dictionary<string, Delegate>();

        public object Exec(string name, params object[] arg)
        {
            if (listeners.Keys.Contains(name))
            {
                return listeners[name].DynamicInvoke(arg);
            }

            return null;
        }
    }
}
