using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBot
{
    class LvProfileItem : LvItem
    {
        public LvProfileItem(NameIdentifiedInfo nii)
        {
            ProfileInfo pi = (ProfileInfo)nii;
            this.ProfileName = nii.name;
        }
        public string ProfileName { get; set; }
    }
}
