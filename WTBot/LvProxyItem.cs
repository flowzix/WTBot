using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    class LvProxyItem : LvItem
    {
        public string profileName { get; set; }
        public string ip { get; set; }
        public string username { get; set; }
        public LvProxyItem(NameIdentifiedInfo nii)
        {
            ProxyInfo pi = (ProxyInfo)(nii);
            this.profileName = pi.name;
            this.ip = pi.ip;
            this.username = pi.username;
        }
    }
}
