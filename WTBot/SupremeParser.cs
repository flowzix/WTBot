using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WTBot
{
    public class SupremeParser
    {
        private Random generator;
        public SupremeParser() { generator = new Random(); }

        public static string GetCSRFToken(string html)
        {
            return new Regex("<meta name=\"csrf-token\" content=\"([^\"]*)\"").Match(html).Groups[1].Value;
        }
        public static string GetAuthenticityToken(string html)
        {
            return new Regex("<input type=\"hidden\" name=\"authenticity_token\" value=\"(.+?)\" />").Match(html).Groups[1].Value;
        }
        public List<ShopItemInfo> GetItemsFromCategory(string category,CookieContainer cookies, ProxyInfo proxyInfo)
        {
            string categoryURL = "http://www.supremenewyork.com/shop/all/" + category;
            string pageHTML = WebTools.GetPageHTML(categoryURL, cookies, proxyInfo).ToLower();

            //<a class="name-link" href="/shop/sweatshirts/w1lxfbkgv/inh4q21pg">Split Crewneck Sweatshirt</a></h1>
            //<p><a class="name-link" href="/shop/sweatshirts/w1lxfbkgv/inh4q21pg">Brown</a>
            string RegExString = "<a class=\"name-link\" href=\"(/[- / a-z 0-9 _]*)\">(.*?)</a></h1>";
            RegExString += "<p><a class=\"name-link\" href=\"[- / a-z 0-9 _]*\">(.*?)</a>";

            Regex regExpPattern = new Regex(RegExString);
            MatchCollection allMatches = regExpPattern.Matches(pageHTML);
            List<ShopItemInfo> items = new List<ShopItemInfo>();

            foreach (var match in allMatches)
            {
                Match m = (Match)match;
                //Console.WriteLine(m.Groups[1].ToString() + " " +  m.Groups[2].ToString() + " " + m.Groups[3].ToString());
                items.Add(new ShopItemInfo(m.Groups[1].ToString(), m.Groups[2].ToString(), m.Groups[3].ToString()));
            }
            return items;
        }
            

        // Gets Supreme item's ID from item's HTML page depending on size chosen
        public String GetItemSizeIdFromPage(string pageHTML, TaskItemInfo tii)
        {
            Regex sizePattern;
            Match sizeMatch;
            string chosenSizeString = tii.size;
            string size = "";

            if (tii.size.Equals("Universal"))    // universal size items - those that are available in only one size.
            {
                sizePattern = new Regex("id=\"size\" value=\"([0-9]*)\"");
                sizeMatch = sizePattern.Match(pageHTML);
                if(sizeMatch.Groups.Count > 1)
                {
                    size = sizeMatch.Groups[1].Value;
                }
                else
                {
                    return null;
                }
            }
            else if (tii.size.Equals("Any"))    // choose size randomly - either universal or from size list.
            {
                sizePattern = new Regex("<option value=\"([0-9]*)\">");
                MatchCollection matches = sizePattern.Matches(pageHTML);    // first check if item is universal or multi-size
                int matchesFound = matches.Count;                           // if it's universal we won't find options to choose

                if (matchesFound >= 1)    // Items are in multiple or one choices ( Not universal )
                {
                    int idChosen = (int)(generator.NextDouble() * (matchesFound));
                    return matches[idChosen].Groups[1].Value;
                }

                // Check if item has only one choice
                sizePattern = new Regex("id=\"size\" value=\"([0-9]*)\"");
                matches = sizePattern.Matches(pageHTML);
                matchesFound = matches.Count;
                if(matchesFound >= 1)
                {
                    return matches[0].Groups[1].Value;
                }
                return null;

            }
            else   // normal sizing items - Medium, Large - chosen by user
            {
                sizePattern = new Regex("<option value=\"([0-9]*)\">" + chosenSizeString + "</option>");
                sizeMatch = sizePattern.Match(pageHTML);
                if (sizeMatch.Groups.Count > 1)
                {
                    size = sizeMatch.Groups[1].Value;
                }
                else  // no sizes available
                {
                    return null;
                }
            }
        return size;
        }


        public String GetItemStyleId(string pageHTML, TaskItemInfo tii)
        {
            Regex stylePattern = new Regex("id=\"style\" value=\"([0-9]*)\"");
            Match styleMatch = stylePattern.Match(pageHTML);
            if(styleMatch.Groups.Count > 1)
            {
                return styleMatch.Groups[1].Value;
            }
            return null;
        }


        public string GetItemCartAddUrl(string pageHTML)
        {
            // get url for adding to cart ->            vvvvvvvvvvvvvvvv
            // <form class="add" id="cart-addf" action="/shop/303399/add" accept-charset="UTF-8" data-remote="true" method="post">
            Regex urlPattern = new Regex("<form class=\"add\" id=.*action=\"(/shop/[0-9]*/add)\".*method=\"post\">");
            Match urlMatch = urlPattern.Match(pageHTML);

            if (urlMatch.Groups.Count < 2)   // no size match found - finish
            {
                //forwardMessageToLogMonitor(Properties.Resources.errorNoSizeAvailable, Int32.Parse(ti.number));
                return "";
            }
            return urlMatch.Groups[1].Value;
        }
        


    }
}
