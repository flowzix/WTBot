using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   
using System.IO;
using System.Windows.Controls;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;
using System.Windows.Threading;
using System.Windows.Media;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace WTBot
{
    public class Bot
    { 
        public LogMonitor logMonitor;
        public InfoManager infoManager { get; private set; }
        public Thread captchaMonitorThread;
        private Dictionary<string, CookieContainer> cookies; // (Task name) -> Cookies
        public  CaptchaMonitor captchaMonitor { get; private set; }
        public BotSettings settings { get; private set; }

        public Bot(InfoManager infoManager)
        {
            ItemNameIdentifiedList profileInfoList = new ItemNameIdentifiedList();
            ItemNameIdentifiedList taskInfoList = new ItemNameIdentifiedList();
            ItemNameIdentifiedList proxyInfoList = new ItemNameIdentifiedList();

            this.infoManager = infoManager;
            this.infoManager.provideLists(taskInfoList, profileInfoList, proxyInfoList);

            cookies = new Dictionary<string, CookieContainer>();
            settings = new BotSettings();
        }

        public void monitorCaptchas(Label lCaptchas)   // called by window
        {
            string readme = "I, as a creator of this software, know that this code can be easily reflected, however, I prefer to spend the time" +
                " making bot better, rather than obfuscating the code, making it harder to read and recreate." +
                "If you are reading this, that's cool, you know more about this than 98% of people!" +
                "Contact me at oliwierflow@gmail.com if you have some questions!";
                
                
            captchaMonitor = new CaptchaMonitor(lCaptchas);
            captchaMonitorThread = new Thread(() => captchaMonitor.startMonitoring());
            captchaMonitorThread.Start();
        }

        private void forwardMessageToLogMonitor(string message,string name)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                logMonitor.addLogMessage(message, name);
            }));
        }

        private void initCookiesByTaskName(string name)
        {
            cookies[name] = new CookieContainer();
            cookies[name].Add(new Cookie("hasShownCookieNotice", "1", "/", "www.supremenewyork.com"));
        }
        public void abortTaskByName(string name)
        {
            forwardMessageToLogMonitor(Properties.Resources.logStoppingThreadWithIt + name, "BOT");
            TaskInfo ti = (TaskInfo)infoManager.GetTaskByName(name);
            ti.taskThread.Abort();
            ti.running = false;
        }

        public void startTaskWithName(string name)
        {
            TaskInfo task = (TaskInfo)infoManager.GetTaskByName(name);
            initCookiesByTaskName(task.name);
            task.taskThread = new Thread(() => taskExecute(task));
            forwardMessageToLogMonitor(Properties.Resources.logStartedTaskWithId + " " + task.name, task.name);
            task.taskThread.Start();
        }

        private void taskExecute(TaskInfo info)
        {

            ShopItemInfo itemFound = null;


            foreach (var userItem in info.items)
            {
                forwardMessageToLogMonitor(Properties.Resources.logTryingToFindItem, info.name);
                itemFound = findMatchingItem(info, userItem);

                if (itemFound != null) // item found
                {
                    forwardMessageToLogMonitor(Properties.Resources.logFoundMatchingItem + " " + itemFound.name, info.name);
                    bool addedToCart = addToCart(itemFound, info, userItem);

                    if (!addedToCart)
                    {
                        return;
                    }
                }
                else    // item not found on website - wait for the page to change content
                {
                    forwardMessageToLogMonitor(Properties.Resources.errorNotFoundRefIn + " " + settings.refreshDelayMs.ToString() +  "ms", info.name);
                    Thread.Sleep(settings.refreshDelayMs);
                    taskExecute(info);
                    return;
                }
            }
            ProfileInfo chosenProfile = (ProfileInfo)infoManager.GetProfileByName(info.profileName);
            checkout(chosenProfile, info);

        }

        private bool everyKeywordMatches(List<string> keywords, string itemName)
        {
            foreach(var keyword in keywords)
            {
                if (!itemName.Contains(keyword))
                {
                    return false;
                }
            }
            return true;
        }


        //  Gets Website's HTML in specific category, finds all items
        //  and returns item matching by color and keywords specified in TaskItemInfo
        //  null is returned if finding fails.
        private ShopItemInfo findMatchingItem(TaskInfo info, TaskItemInfo userItem)
        {
            SupremeParser parser = new SupremeParser();
            List<ShopItemInfo> items = parser.GetItemsFromCategory(userItem.category, cookies[info.name], (ProxyInfo)infoManager.GetProxyByName(info.proxyName));
            if (items == null)
            {
                return null;
            }
            forwardMessageToLogMonitor(Properties.Resources.logFoundTotalOf + " " + items.Count.ToString() +
                " " + Properties.Resources.logItemsIn + " " + userItem.category, info.name);
            
            foreach (var shopItem in items)  // find item by name and color
            {
                if (everyKeywordMatches(userItem.keywords,shopItem.name) && shopItem.color.Contains(userItem.color)) // get first matching item
                {
                    return shopItem;
                }
            }
            return null;
        }


        // Adds item to cart, stores cookies in TaskInfo,
        // those cookies have informations about items
        // that are put in cart
        // returns false if failed
        private bool addToCart(ShopItemInfo itemInfo, TaskInfo ti, TaskItemInfo tii)
        {
            forwardMessageToLogMonitor(Properties.Resources.logAdding + " " + itemInfo.name + " " + Properties.Resources.logToCart, ti.name);
            SupremeParser parser = new SupremeParser();

            // get url of the chosen item
            var itemUrl = "http://www.supremenewyork.com" + itemInfo.url;
            ti.lastItemUri = itemUrl; // referer for checkout
            var responseItemHTML = WebTools.GetPageHTML(itemUrl, cookies[ti.name], (ProxyInfo)infoManager.GetProxyByName(ti.proxyName));

            string itemAddUrl = "https://www.supremenewyork.com" + parser.GetItemCartAddUrl(responseItemHTML);

            String style = parser.GetItemStyleId(responseItemHTML, tii);
            String size = parser.GetItemSizeIdFromPage(responseItemHTML, tii);

            String csrf_token = new Regex("<meta name=\"csrf-token\" content=\"([^\"]*)\"").Match(responseItemHTML).Groups[1].Value;


            //FileLogger.log("ItemUri: " + itemUrl + "\n");
            //FileLogger.log("Style: " + style + " Size: " + size + "\n");
            //FileLogger.log("PostUri: " + itemAddUrl + "\n");

            if (style == null || size == null)
            {
                forwardMessageToLogMonitor(Properties.Resources.logItemNotAvailable, ti.name);
                return false;
            }
            string postString = PostStringGenerator.generateAddCartPostString(size, style);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(itemAddUrl);
            req.Referer = itemUrl;
            req.Method = "POST";
            req.Accept = "*/*;q=0.5, text/javascript, application/javascript, application/ecmascript, application/x-ecmascript";
            req.Headers["x-csrf-token"] = csrf_token;
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"; // utf8 del
            req.ContentLength = postString.Length;
            req.CookieContainer = cookies[ti.name];

            WebTools.setProxy((ProxyInfo)infoManager.GetProxyByName(ti.proxyName), req);

            // as postWriter doesn't actually send any data, we only need to make sure that GetResponse is going good.
            StreamWriter postWriter = new StreamWriter(req.GetRequestStream());
            postWriter.Write(postString);
            postWriter.Close();


            //MSDOC
            //The GetResponse method sends a request to an Internet resource and returns a WebResponse instance.
            //If the request has already been initiated by a call to GetRequestStream, the GetResponse method 
            //completes the request and returns any response. \|/
            var res = WebTools.SendRequestAtAllCosts(req);
            res.Close();
            // it takes ~7ms to read the response, but well, we don't need it here, we just need cookiez

            foreach (var cookie in cookies[ti.name].GetCookies(new Uri("http://www.supremenewyork.com")))
            {
                Console.WriteLine(cookie.ToString());
            }


            //cookies are now in CookieContainer which is a reference for cookies[ti.number] ( no need to save them )
            forwardMessageToLogMonitor(Properties.Resources.logSuccessfullyAdded + " " + itemInfo.name + " " + Properties.Resources.logToCart, ti.name);
            return true;
        }


        // Goes to checkout page, gets autheticity token,
        // checks out with cookies provided in TaskInfo
        // that contain item that was put in cart
        // and profile provided in ProfileInfo
        private void checkout(ProfileInfo info,TaskInfo ti)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.supremenewyork.com/checkout");
            req.Method = "GET";
            req.CookieContainer = cookies[ti.name];
            WebTools.setProxy((ProxyInfo)infoManager.GetProxyByName(ti.proxyName), req);
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            req.Referer = ti.lastItemUri;
            req.Headers["upgrade-insecure-requests"] = "1";
            
            // Go to checkout page to read Authenticity Token
            HttpWebResponse res = WebTools.SendRequestAtAllCosts(req);
            StreamReader responseReader = new StreamReader(res.GetResponseStream());
            String finalRes = responseReader.ReadToEnd();
            responseReader.Close();

            Console.WriteLine(finalRes);

            String authenticity_token = new Regex("<input type=\"hidden\" name=\"authenticity_token\" value=\"(.+?)\" />").Match(finalRes).Groups[1].Value;
            Console.WriteLine("Auth=" + authenticity_token);

            StringBuilder sb = new StringBuilder();
            sb.Append("https://www.supremenewyork.com/checkout.js?utf8=%E2%9C%93&");
            sb.Append("authenticity_token=");
            sb.Append(Uri.EscapeDataString(authenticity_token));
            sb.Append("&order%5Bbilling_name%5D=");
            sb.Append(info.FullName.Replace(" ", "+"));
            sb.Append("&order%5Bemail%5D=");
            sb.Append(info.Email.Replace("@", "%40"));
            sb.Append("&order%5Btel%5D=");
            sb.Append(info.TelNr.Replace("+", "%2B"));
            sb.Append("&order%5Bbilling_address%5D=");
            sb.Append(info.Address1.Replace(" ", "+"));
            sb.Append("&order%5Bbilling_address_2%5D=");
            sb.Append(info.Address2.Replace(" ", "+"));
            sb.Append("&order%5Bbilling_address_3%5D=");
            sb.Append("");    // address3 will be null - this line has no effect at all
            sb.Append("&order%5Bbilling_city%5D=");
            sb.Append(info.City.Replace(" ", "+"));
            sb.Append("&order%5Bbilling_zip%5D=");
            sb.Append(info.Postcode);
            sb.Append("&order%5Bbilling_country%5D=");
            sb.Append(CountryConverter.GetCountryCode(info.Country));
            sb.Append("&same_as_billing_address=1&store_credit_id=");
            sb.Append("&credit_card%5Btype%5D=visa&credit_card%5Bcnb%5D=&credit_card%5Bmonth%5D=");
            sb.Append("09&credit_card%5Byear%5D=2018&credit_card%5Bvval%5D=&order%5Bterms%5D=0&g-recaptcha-response=&hpcvv=&cnt=2");
            string checkoutJsUri = sb.ToString();

            string csrfToken = SupremeParser.GetCSRFToken(finalRes);

            //// When selecting the country manually, supreme sends a GET request
            //// and gets a new supreme sessid. I don't know if it changes anything,
            //// but I wanna do everything like human does - from beginning till end.
            req = (HttpWebRequest)WebRequest.Create(checkoutJsUri);
            req.Method = "GET";
            req.Accept = "text/html, */*; q=0.01";
            req.Headers["accept-encoding"] = "gzip, deflate, br";
            req.Headers["x-csrf-token"] = csrfToken;
            req.Headers["x-requested-with"] = "XMLHttpRequest";
            req.CookieContainer = cookies[ti.name];
            WebTools.setProxy((ProxyInfo)infoManager.GetProxyByName(ti.proxyName), req);
            res = WebTools.SendRequestAtAllCosts(req);
            res.Close();
            // just cookies interest me

            //Console.WriteLine("After checkout js:");
            //foreach (var cookie in cookies[ti.name].GetCookies(new Uri("http://www.supremenewyork.com")))
            //{
            //    Console.WriteLine(cookie);
            //}


            Thread.Sleep(20);
            // Before checkout the email is being verified and we get some new supreme sessid.. so many requests

            // This site needs a custom request - it returns 404 error, yet it sets the cookies?
            // It's strange, but yeah, even with 404 exception, the cookies are being set.
            //req = (HttpWebRequest)WebRequest.Create("https://www.supremenewyork.com/store_credits/verify?email=" + info.Email.Replace("@", "%40"));
            //req.Method = "GET";
            //req.CookieContainer = cookies[ti.name];
            //req.Headers["x-csrf-token"] = csrfToken;
            //req.Headers["x-requested-with"] = "XMLHttpRequest";
            //req.Accept = "*/*";
            //req.Headers["accept-encoding"] = "gzip, deflate, br";

            //req.Referer = "https://www.supremenewyork.com/checkout";

            //WebTools.setProxy((ProxyInfo)infoManager.GetProxyByName(ti.proxyName), req);
            //try
            //{
            //    Console.WriteLine("In");
            //    req.AllowAutoRedirect = true;
            //    res = (HttpWebResponse)req.GetResponse();
            //    res.Close();
            //    Console.WriteLine("Out");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("ex");
            //    Console.WriteLine(ex.Message);
            //}


            //Console.WriteLine("After mail check:");
            //foreach (var cookie in cookies[ti.name].GetCookies(new Uri("http://www.supremenewyork.com")))
            //{
            //    Console.WriteLine(cookie);
            //}

            //Console.WriteLine(finalRes);



            ReCaptchaResponseInfo rcri = captchaMonitor.GetSpareReCaptcha();
            if (rcri == null)
            {
                forwardMessageToLogMonitor(Properties.Resources.errorNoCaptchasAvailable, ti.name);
                return;
            }
            String postString = PostStringGenerator.generateCheckoutPostString(info, authenticity_token, rcri.response);

            Console.WriteLine(postString);

            // Fill Post request info
            req = (HttpWebRequest)WebRequest.Create("https://www.supremenewyork.com/checkout");
            req.Referer = "https://www.supremenewyork.com/checkout";
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postString.Length;
            req.Host = "www.supremenewyork.com";
            req.Referer = "https://www.supremenewyork.com/checkout";
            req.Headers["x-csrf-token"] = csrfToken;
            req.CookieContainer = cookies[ti.name];
            WebTools.setProxy((ProxyInfo)infoManager.GetProxyByName(ti.proxyName), req);

            // Sleep for the Checkout delay
            Thread.Sleep(Int32.Parse(ti.checkoutDelay));

            // Send request
            StreamWriter postWriter = new StreamWriter(req.GetRequestStream());
            postWriter.Write(postString);
            postWriter.Close();

            res = WebTools.SendRequestAtAllCosts(req);
            responseReader = new StreamReader(res.GetResponseStream());
            finalRes = responseReader.ReadToEnd();
            responseReader.Close();

            Console.WriteLine(finalRes);

            // checkout OK
            if (finalRes.Contains("submitted"))
            {
                forwardMessageToLogMonitor(Properties.Resources.logCheckedOutSuc, "BOT");
            }
            else if (finalRes.Contains("Card Payment"))
            {
                forwardMessageToLogMonitor(Properties.Resources.cardError, "BOT");
            }
            else if (finalRes.Contains("Unfortunately"))
            {
                forwardMessageToLogMonitor(Properties.Resources.checkoutError, "BOT");
            }
        }

    }
}
