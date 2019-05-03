using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WTBot
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]

    public class InterchangeHelper
    {
        ReCaptchaWindow mExternalWPF;
        Bot bot;
        public InterchangeHelper(ReCaptchaWindow w, Bot bot)
        {
            this.mExternalWPF = w;
            this.bot = bot;
        }
        public void reCaptchaPassed(string reCaptcha)
        {
            ReCaptchaResponseInfo rcri = new ReCaptchaResponseInfo(reCaptcha);
            bot.captchaMonitor.AddReCaptcha(rcri);
            mExternalWPF.showReCaptcha();
        }

    }
}
