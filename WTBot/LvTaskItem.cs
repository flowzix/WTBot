using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WTBot
{
    public class LvTaskItem : LvItem
    {
        public LvTaskItem(NameIdentifiedInfo iil)
        {
            TaskInfo ti = (TaskInfo)iil;
            this.ProfileName = ti.profileName;
            this.CheckoutDelay = ti.checkoutDelay;
            this.TaskName = ti.name;

        }
        public string TaskName { get; set; }
        public string ProfileName { get; set; }
        public string CheckoutDelay { get; set; }
    }
}
