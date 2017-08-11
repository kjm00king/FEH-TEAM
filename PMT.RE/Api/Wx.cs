using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Api
{
    public class Wx
    {
        public bool debug = false;

        public string appid = string.Empty;
        public string secret = string.Empty;

        public string token = string.Empty;

        public string type = string.Empty;
        
        public string getTokenUrl()
        {
            return "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
        }

        public string getTicketUrl()
        {
            return "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";
        }
    }
}
