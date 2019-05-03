using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    public class PostReqMaker
    {
        public PostReqMaker()
        {

        }
        public string PostRequest(string uri, string postString, CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Referer = uri;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers["Upgrade-Insecure-Requests"] = "1";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:51.0) Gecko/20100101 Firefox/51.0";
            req.ContentLength = postString.Length;
            req.CookieContainer = cookies;

            StreamWriter postWriter = new StreamWriter(req.GetRequestStream());
            postWriter.Write(postString);
            postWriter.Close();

            var res = (HttpWebResponse)req.GetResponse();
            var responseReader = new StreamReader(res.GetResponseStream());
            var strRes = responseReader.ReadToEnd();
            responseReader.Close();

            return strRes;
        }


    }
}
