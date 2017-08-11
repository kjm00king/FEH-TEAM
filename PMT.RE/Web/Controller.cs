using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web
{
    public class Controller : System.Web.Mvc.Controller, IModel
    {
        //public string IocPath = "~/IoC.bin";

        protected ISrvProvider _owner = null;

        //private LifeScope life = new LifeScope();

        public string this[string name]
        {
            get { return string.Empty; }
        }

        public ISrvProvider owner
        {
            set
            {
                _owner = value;
            }
        }

        public void FromJson(string content)
        {
            //ListTmp tmp = new ListTmp(content);
            //life = tmp.GetAsEntity<LifeScope>(0);
        }

        //public override string ToString()
        //{
        //    return new ListTmp(life).ToString();
        //}        
        
        protected void LoadSrvState()
        {
            if (_owner != null)
            {
                _owner.Load();
                //string _web_srv_path = Server.MapPath(IocPath);
                //using (FileStream fs = new FileStream(_web_srv_path, FileMode.OpenOrCreate))
                //{
                //    byte[] buff = new byte[fs.Length];
                //    fs.Read(buff, 0, buff.Length);
                //    _owner.Load(buff);
                //}
            }
        }
        protected void SaveSrvState()
        {
            if (_owner != null)
            {
                _owner.Save();
                //string _web_srv_path = Server.MapPath(IocPath);
                //string tmppath = Server.MapPath("~/" + Guid.NewGuid().ToString().Replace("-", ""));
                //using (FileStream fs = new FileStream(tmppath, FileMode.OpenOrCreate))
                //{
                //    byte[] buff = _owner.Save();
                //    fs.Write(buff, 0, buff.Length);
                //}

                //System.IO.File.Copy(tmppath, _web_srv_path, true);
                //System.IO.File.Delete(tmppath);
            }
        }
    }
}
