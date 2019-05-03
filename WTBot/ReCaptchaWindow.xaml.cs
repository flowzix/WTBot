using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WTBot
{
    /// <summary>
    /// Interaction logic for ReCaptchaWindow.xaml
    /// </summary>
    public partial class ReCaptchaWindow : Window
    {
        //public List<ShopItemInfo> list;
        private string captcha = "<div class='g-recaptcha' data-sitekey='6LeWwRkUAAAAAOBsau7KpuC9AV-6J8mhw4AjC3Xz' data-callback='reCaptchaCallback'></div>";
        private string page = "http://www.supremenewyork.com";
        private InterchangeHelper helper;
        private Bot bot;
        public ReCaptchaWindow(Bot bot)
        {
            this.bot = bot;
            InitializeComponent();
            HideScriptErrors(reCaptchaBrowser, true);
            reCaptchaBrowser.LoadCompleted += ReCaptchaBrowser_LoadCompleted;
            helper = new InterchangeHelper(this, bot);
            this.reCaptchaBrowser.ObjectForScripting = helper;
            navigateToSupreme();
        }

        private void ReCaptchaBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            showReCaptcha();
        }

        public void showReCaptcha()
        {
            var doc = reCaptchaBrowser.Document as HTMLDocument;
            if (doc != null)
            {
                var script = (IHTMLScriptElement)doc.createElement("SCRIPT");
                script.type = "text/javascript";
                script.text = "function reCaptchaCallback(){window.external.reCaptchaPassed(grecaptcha.getResponse());};";

                var script2 = (IHTMLScriptElement)doc.createElement("SCRIPT");
                script2.type = "text/javascript";
                script2.src = "https://www.google.com/recaptcha/api.js?hl=en";

                IHTMLElementCollection nodes = doc.getElementsByTagName("head");


                foreach (IHTMLElement elem in nodes)
                {
                    var head = (HTMLHeadElement)elem;
                    head.innerHTML = "";
                    head.appendChild((IHTMLDOMNode)script);
                    head.appendChild((IHTMLDOMNode)script2);
                }

                // set body to reCaptcha with shop data-sitekey, pretending to be on their site
                var el = ((HTMLDocument)reCaptchaBrowser.Document).body;
                el.innerHTML = captcha;
            }
        }
        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); //In case we are too early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
        private void navigateToSupreme()
        {
            reCaptchaBrowser.Navigate(page);
        }
    }
}
