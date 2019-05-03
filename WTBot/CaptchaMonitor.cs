using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WTBot
{
    public class CaptchaMonitor
    {
        Label output;
        ConcurrentQueue<ReCaptchaResponseInfo> queue;
        public CaptchaMonitor(Label output)
        {
            this.output = output;
            queue = new ConcurrentQueue<ReCaptchaResponseInfo>();
        }
        public void startMonitoring()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        output.Background = Brushes.Green;
                    }));
                    if (queue.First<ReCaptchaResponseInfo>().timer.ElapsedMilliseconds > ReCaptchaResponseInfo.RECAPTCHA_VALIDITY_MS_PERIOD)
                    {
                        queue.TryDequeue(out _);   // I dont care about it if it's no longer valid captcha.
                    }
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        output.Background = Brushes.Red;
                    }));
                }
                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    output.Content = queue.Count.ToString();
                }));

                Thread.Sleep(900); // ~900ms sleep per one circulation + the code time
            }
        }
        public ReCaptchaResponseInfo GetSpareReCaptcha()
        {
            ReCaptchaResponseInfo rcri = new ReCaptchaResponseInfo();
            bool success = queue.TryDequeue(out rcri);
            if (!success)
            {
                return null;
            }
            return rcri;
        }
        public void AddReCaptcha(ReCaptchaResponseInfo rcri)
        {
            queue.Enqueue(rcri);
        }
    }
}
