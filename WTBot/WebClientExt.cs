using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    class WebClientExt : WebClient
    {

    
    // Timeout in milliseconds, default = 600,000 msec
    public int Timeout { get; set; }

        public WebClientExt()
        {
            this.Timeout = 600000;
        }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            var wr = base.GetWebRequest(uri);
            wr.Timeout = this.Timeout;
            return wr;
        }
    }
}

