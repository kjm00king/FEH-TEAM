using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CML
{
    public class ComUrl
    {
        public static string Post(string url, string param)
        {
            return Post(url, param, null, 0);
        }

        public static string Post(string url, string param, string proxyendpoint, int proxyport)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(param);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            if (proxyendpoint != null && proxyport != 0)
            {
                WebProxy proxy = new WebProxy(proxyendpoint, proxyport);
                proxy.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy = proxy;
            }
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();

        }

        public static string Get(string url, string param)
        {
            return Get(url, param, null, 0);
        }

        public static string Get(string url, string param, string proxyendpoint, int proxyport)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + "?" + param);
            if (proxyendpoint != null && proxyport != 0)
            {
                WebProxy proxy = new WebProxy(proxyendpoint, proxyport);
                proxy.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy = proxy;
            }
            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();

        }

        public static Stream Download(string url)
        {
            return Download(url, null, 0);
        }

        public static Stream Download(string url, string proxyendpoint, int proxyport)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (proxyendpoint != null && proxyport != 0)
            {
                WebProxy proxy = new WebProxy(proxyendpoint, proxyport);
                proxy.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy = proxy;
            }
            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }
    }
}
