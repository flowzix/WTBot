using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WTBot
{
    [Serializable]
    public class TaskInfo : NameIdentifiedInfo
    {
        [XmlIgnore]
        public bool running;
        public TaskInfo()
        {
            items = new List<TaskItemInfo>();
            taskThread = null;
            running = false;
        }
        public string proxyName;
        public string color;
        public string checkoutDelay;
        public List<TaskItemInfo> items;
        public string status;
        public string profileName;
        public string category;
        [XmlIgnore]
        public string lastItemUri;
        public string size;
        [XmlIgnore]
        public Thread taskThread;


        public override LvItem GetLvRepresentation()
        {
            return new LvTaskItem(this);
        }
    }
}
