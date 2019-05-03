using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    [Serializable]
    public class TaskItemInfo
    {
        private static int ID = 1;
        public TaskItemInfo()
        {
            keywords = new List<string>();
            id = ID;
            ID++;
        }
        public int id;
        public string size;
        public string category;
        public string color;
        public List<string> keywords;
        public string GetKeywords()
        {
            string skeywords = "";
            foreach (var k in keywords)
            {
                skeywords += k + ",";
            }
            return skeywords.Substring(0, skeywords.Length - 1);
        }
    }
}
