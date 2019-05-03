using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    [Serializable]
    public abstract class NameIdentifiedInfo
    {
        public string name;
        public abstract LvItem GetLvRepresentation();
    }
}
