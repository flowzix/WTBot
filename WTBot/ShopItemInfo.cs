using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    public class ShopItemInfo
    {
        public ShopItemInfo(string url, string name, string color)
        {
            this.url = url;
            this.name = name;
            this.color = color;
        }
        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }
    }
}
