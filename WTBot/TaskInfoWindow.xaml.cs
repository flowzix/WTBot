using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WTBot
{
    /// <summary>
    /// Interaction logic for TaskInfoWindow.xaml
    /// </summary>
    public partial class TaskInfoWindow : Window
    {
        private Bot bot;
        private bool editMode;
        private string editedName;
        private int id;
        List<TaskItemInfo> items = new List<TaskItemInfo>();
        public TaskInfoWindow(Bot bot, bool editMode, string editedName)
        {
            id = 1;
            InitializeComponent();
            this.bot = bot;
            this.editMode = editMode;
            this.editedName = editedName;

            ItemNameIdentifiedList cds = bot.infoManager.GetProfilesList();
            foreach(var cd in cds)
            {
                cbProfile.Items.Add(cd);
            }
            cbProxy.Items.Add("-");
            ItemNameIdentifiedList proxies = bot.infoManager.GetProxiesList();
            foreach(var proxy in proxies)
            {
                cbProxy.Items.Add(proxy);
            }
            cbProxy.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            cbSize.SelectedIndex = 0;
            cbProfile.SelectedIndex = 0;

            colId.DisplayMemberBinding = new Binding("Id");
            colKeywords.DisplayMemberBinding = new Binding("Keywords");
            colSize.DisplayMemberBinding = new Binding("Size");
            colCategory.DisplayMemberBinding = new Binding("Category");
            colColor.DisplayMemberBinding = new Binding("Color");

        }
        public void fillWithTaskInfo(TaskInfo ti)
        {
            items = ti.items;
            tbCheckoutDelay.Text = ti.checkoutDelay;
            tbTaskName.Text = ti.name;

            foreach(var item in cbProfile.Items)
            {
                ProfileInfo cd = (ProfileInfo)item;
                if (cd.name.Equals(ti.profileName))
                {
                    cbProfile.SelectedItem = item;
                    break;
                }
            }

            foreach(var tii in ti.items)
            {
                lvItems.Items.Add(new LvItemInfo { Id = tii.id.ToString(), Keywords = tii.GetKeywords(), Size = tii.size, Color = tii.color, Category = tii.category });
            }


            
        }
        private void butTaskInfoSave_Click(object sender, RoutedEventArgs e)
        {
            if(items.Count == 0)
            {
                MessageBox.Show(Properties.Resources.errorCantSaveWithoutItems);
                return;
            }

            if (((ProfileInfo)cbProfile.SelectedItem).name.Equals(""))
            {
                System.Windows.MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.taskInfoProfile);
                return;
            }
            else if (tbCheckoutDelay.Text.Equals(""))
            {
                System.Windows.MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.taskInfoCheckoutDelay);
                return;
            }

            TaskInfo taskInfo = new TaskInfo();
            taskInfo.items = items;
            taskInfo.checkoutDelay = tbCheckoutDelay.Text;
            taskInfo.profileName = ((ProfileInfo)cbProfile.SelectedItem).name;
            taskInfo.proxyName = cbProxy.SelectedValue.ToString();
            Console.WriteLine(taskInfo.proxyName);
            taskInfo.name = tbTaskName.Text;

            


            TaskInfo currEdited = (TaskInfo)bot.infoManager.GetTaskByName(editedName);
            if (currEdited != null)
            {
                bot.infoManager.RemoveTaskByName(currEdited.name);
            }
            bot.infoManager.addTask(taskInfo);
            bot.infoManager.updateTasksList();
            bot.infoManager.saveTasksList();

            this.Close();
        }

        private double calculateSize(SizeChangedEventArgs e) {
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;
            return (width * 9 > height * 9) ? height * 9 : width * 9;
        }

        private void taskInfoGridSizeChanged(object sender, SizeChangedEventArgs e) {
            double size = calculateSize(e);

            List<Control> controls = new List<Control>();

            controls.Add(labelCategory);
            controls.Add(labelCheckoutDelay);
            controls.Add(labelColor);
            controls.Add(labelKeywords);
            controls.Add(labelSize);
            controls.Add(labelProfile);
            controls.Add(labelTaskName);
            controls.Add(labelProxy);

            controls.Add(tbCheckoutDelay);
            controls.Add(tbColor);
            controls.Add(tbKeywords);
            

            controls.Add(cbCategory);
            controls.Add(cbProfile);
            controls.Add(cbSize);
            controls.Add(cbProxy);

            controls.Add(lvItems);
            controls.Add(butAddItem);
            controls.Add(butRemoveItem);


            controls.Add(butTaskInfoSave);

            foreach (var control in controls) {
                control.FontSize = size / (15 * 16);
            }
        }

        private void lvItems_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int idWeight = 1;
            int categoryWeight = 3;
            int colorWeight = 2;
            int sizeWeight = 2;
            int keywordsWeight = 4;

            double weightSum = idWeight + categoryWeight + sizeWeight + keywordsWeight + colorWeight;
            double chunk = lvItems.ActualWidth / weightSum;

            colId.Width = chunk * idWeight;
            colCategory.Width = chunk * categoryWeight;
            colKeywords.Width = chunk * keywordsWeight;
            colSize.Width = chunk * sizeWeight;
            colColor.Width = chunk * colorWeight;
            
        }

        private void butAddItem_Click(object sender, RoutedEventArgs e)
        {

            TaskItemInfo tii = new TaskItemInfo();

            tii.category = ((ComboBoxItem)cbCategory.SelectedItem).Content.ToString();
            tii.color = tbColor.Text.ToLower();
            tii.size = ((ComboBoxItem)cbSize.SelectedItem).Content.ToString();

            if(tii.category.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.taskInfoCategory);
                return;
            }
            else if (tii.color.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.taskInfoColor);
                return;
            }
            else if (tii.size.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.taskInfoSize);
                return;
            }

            string keywords = tbKeywords.Text.ToLower();
            string[] akeywords = keywords.Split(',');
            foreach (var keyword in akeywords)
            {
                tii.keywords.Add(keyword);
            }
            items.Add(tii);

            lvItems.Items.Add(new LvItemInfo { Id = (id++).ToString(), Keywords = tii.GetKeywords(), Size = tii.size, Color = tii.color, Category = tii.category });
        }

        private void butRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            LvItemInfo lvi = ((LvItemInfo)lvItems.SelectedItem);
            if (lvi != null)    // item selected
            {
                string id = lvi.Id;

                for (int i = items.Count - 1; i >= 0; i--)
                {
                    if (items[i].id.ToString().Equals(id))
                    {
                        items.RemoveAt(i);
                        break;
                    }
                }
                lvItems.Items.Remove(lvi);
            }
            else // item not selected
            {
                MessageBox.Show(Properties.Resources.errorSelectTaskFirst);
                return;
            }
        }


    }
}
