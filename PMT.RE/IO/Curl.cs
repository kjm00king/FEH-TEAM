using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PMT.RE.IO
{
    public class Curl:IEntity
    {
        public class Proxy : IEntity
        {
            public bool use = false;
            public string address = string.Empty;
            public int port = 0;
            public string domain = string.Empty;
            public string user = string.Empty;
            public string pwd = string.Empty;

            public void FromJson(string content)
            {
                ListTmp tmp = new ListTmp(content);
                use = tmp.GetAsBool(0);
                address = tmp.Get(1);
                port = tmp.GetAsInt(1);
                domain = tmp.Get(1);
                user = tmp.Get(1);
                pwd = tmp.Get(1);
            }

            public override string ToString()
            {
                return new ListTmp(use
                    , address
                    , port
                    , domain
                    , user
                    , pwd).ToString();
            }
        }

        public Proxy proxy = new Proxy();
        
        public Curl() { }

        public NetworkCredential getProxyCredential()
        {
            return new NetworkCredential(proxy.user, proxy.pwd, proxy.domain);
        }

        public string getResult(string url)
        {
            url = HttpUtility.HtmlDecode(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            if (proxy.use)
            {
                request.Proxy = new WebProxy(proxy.address, proxy.port);
                request.Proxy.Credentials = getProxyCredential();
            }

            string result = string.Empty;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.UTF8);
            result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }
        
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            proxy.FromJson(tmp.Get(0));
        }

        public override string ToString()
        {
            return new ListTmp(proxy).ToString();
        }
    }
}
