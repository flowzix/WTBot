using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    public class BotSettings
    {
        public int refreshDelayMs { get; set; }    // miliseconds between every refresh of page in search of item
        public BotSettings()
        {
            refreshDelayMs = 3000;
        }
    }
}
