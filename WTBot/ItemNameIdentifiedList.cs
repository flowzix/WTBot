using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WTBot
{
    [Serializable]
    // a list, that contains named items (Proxy names, Task names, Profile names, maybe more)
    public class ItemNameIdentifiedList : List<NameIdentifiedInfo>
    {
        public ItemNameIdentifiedList() { }
        public NameIdentifiedInfo GetItemByName(string name)
        {
            foreach(var item in this)
            {
                if (item.name.Equals(name))
                {
                    return item;
                }
            }
            return default(NameIdentifiedInfo);
        }
        
        public void RemoveItemByName(string name)
        {
            for(int i = this.Count-1; i >= 0; i--)
            {
                if (this[i].name.Equals(name))
                {
                    this.RemoveAt(i);
                    return;
                }
            }
        }

        public void AddItem(NameIdentifiedInfo item)
        {
            NameIdentifiedInfo nii = GetItemByName(item.name);
            if(nii == null)
            {
                this.Add(item);
                return;
            }
            RemoveItemByName(item.name);
            AddItem(item);
            
        }

        public void updateListView(ListView lv)
        {
            lv.Items.Clear();
            int i = 1;
            foreach (var item in this)
            {
                lv.Items.Add(item.GetLvRepresentation());
                i++;
            }
        }

    }
}
