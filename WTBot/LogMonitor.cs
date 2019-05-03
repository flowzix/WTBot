using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WTBot
{
    public class LogMonitor
    {
        private TextBlock textBlock;
        private ScrollViewer sv;
        public LogMonitor(TextBlock textBlock, ScrollViewer sv)
        {
            this.textBlock = textBlock;
            this.sv = sv;
        }
        public void addLogMessage(string message, string name)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.ff");
            textBlock.Text += "[#" + name + "][" + time + "]: " + message + "\n";
            sv.ScrollToBottom();
        }
        public void emptyLog()
        {
            textBlock.Text = "";
        }
    }
}
