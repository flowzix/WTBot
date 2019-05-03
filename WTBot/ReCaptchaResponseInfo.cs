using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WTBot
{
    public class ReCaptchaResponseInfo
    {
        public const int RECAPTCHA_VALIDITY_MS_PERIOD = 110000;
        public string response;
        public Stopwatch timer;
        public ReCaptchaResponseInfo()
        {

        }
        public ReCaptchaResponseInfo(string response)
        {
            this.response = response;
            timer = new Stopwatch();    
            timer.Start();
        }
    }
}
