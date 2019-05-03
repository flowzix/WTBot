using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WTBot
{
    static class WebTools
    {
        static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
        // Sends out a HEAD request to see if a page exists
        public static bool WebUriExists(string uri)
        {
            HttpWebResponse resp = null;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "HEAD";
            try
            {
                resp = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (resp != null)
                {
                    resp.Close();
                }
            }
            return true;
        }
        // Gets page HTML as String
        // gets it no matter what.
        public static string GetPageHTML(string uri, CookieContainer cookies, ProxyInfo proxyInfo)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = userAgent;
            req.Method = "GET";
            req.CookieContainer = cookies;
           
            setProxy(proxyInfo, req);
            string pageHTML = "";
            var res = SendRequestAtAllCosts(req);
            var responseReader = new StreamReader(res.GetResponseStream());
            pageHTML = responseReader.ReadToEnd();
            responseReader.Close();
            return pageHTML;
        }

        // Sometimes, when the page is down, it returns some exceptions, especially during high traffic
        // for example 503, 404, we will be keep on trying to make the requests
        // even if those statuses occur.
        // It's also very advised to set DNS to google one,
        // because sometimes ISP's (like my ISP) redirect
        // to custom 404 pages, which are not regarded as 404
        public static HttpWebResponse SendRequestAtAllCosts(HttpWebRequest req)
        {
            HttpWebResponse res = null;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch(WebException ex)
            {
                FileLogger.log("WebExpcetion occured: " + ex.Message);
                Console.WriteLine(ex.Message);
                Thread.Sleep(200); // Don't spam too much with those requests
                return SendRequestAtAllCosts(req); // call stack should have enough space with that sleep lol
            }
            Console.WriteLine("Hmm");
            return res;
        }



        public static void setProxy(ProxyInfo proxyInfo, HttpWebRequest req)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            req.UserAgent = userAgent;
            if (proxyInfo != null)
            {
                WebProxy proxy = new WebProxy();
                proxy.Address = new Uri(proxyInfo.ip);
                proxy.Credentials = new NetworkCredential(proxyInfo.username, proxyInfo.password);
                req.Proxy = proxy;
            }
            else
            {
                req.Proxy = null;
            }
        }

    }
}
