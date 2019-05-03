using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    public class ProxyInfo : NameIdentifiedInfo
    {
        public string ip;
        public string username;
        public string password;

        public override LvItem GetLvRepresentation()
        {
            return new LvProxyItem(this);
        }
        public override string ToString()
        {
            return name;
        }
    }
}
