using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WTBot
{
    public partial class MainWindow : Window
    {
        private LogMonitor logMonitor;
        private InfoManager infoManager;
        private Bot bot;
        public MainWindow()
        {
            InitializeComponent();
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            
            logMonitor = new LogMonitor(tbLog,tbLogScroll);

            infoManager = new InfoManager();
            infoManager.provideLVS(lvTasks, lvData, lvProxy);

            bot = new Bot(infoManager);

            colTaskName.DisplayMemberBinding = new Binding("TaskName");
            colProfile.DisplayMemberBinding = new Binding("ProfileName");
            colCheckoutDelay.DisplayMemberBinding = new Binding("CheckoutDelay");

            dataColProfileName.DisplayMemberBinding = new Binding("ProfileName");

            colProxyName.DisplayMemberBinding = new Binding("profileName");
            colIP.DisplayMemberBinding = new Binding("ip");
            colUsername.DisplayMemberBinding = new Binding("username");
            
            bot.logMonitor = logMonitor;

            logMonitor.addLogMessage(Properties.Resources.logLoadingData, "BOT");

            infoManager.loadProfilesList();
            infoManager.loadTasksList();
            infoManager.loadProxiesList();

            logMonitor.addLogMessage(Properties.Resources.logLoadingDataFinished, "BOT");

            cbCountry.SelectedIndex = 0;
            cbCardType.SelectedIndex = 0;
            cbCardMonth.SelectedIndex = 0;
            cbCardYear.SelectedIndex = 0;

            bot.monitorCaptchas(lCapchasSolved); 
            
        }

        private void fillDataView()
        {
            int nameWeight = 1;
            int weightSum = nameWeight;
            double lvWidth = lvData.ActualWidth;
            double chunk = lvWidth / weightSum;
            dataColProfileName.Width = nameWeight * chunk;
        }

        private void lvTasks_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            fillListView();
        }

        private void fillListView()
        {
            int taskNameWeight = 3;
            int profileWeight = 3;
            int checkoutDelayWeight = 2;
            int runWeight = 2;
            int weightSum =taskNameWeight + checkoutDelayWeight + runWeight + profileWeight;

            double lvWidth = lvTasks.ActualWidth;
            double chunk = lvWidth / weightSum;

            colTaskName.Width = chunk * taskNameWeight;
            colRun.Width = chunk * runWeight;
            colCheckoutDelay.Width = chunk * checkoutDelayWeight;
            colProfile.Width = chunk * profileWeight;
        }

        private void butAddTask_Click(object sender, RoutedEventArgs e)
        {
            if (infoManager.anyProfileExists())
            { 
                TaskInfoWindow tiw = new TaskInfoWindow(bot, false, "");
                tiw.Show();
            }
            else
            {
                MessageBox.Show(Properties.Resources.errorAddProfileFirst);
            }
        }

        private void butEditTask_Click(object sender, RoutedEventArgs e)
        {
            LvTaskItem gti = ((LvTaskItem)lvTasks.SelectedItem);
            if (gti != null)    // something is selected
            {
                TaskInfo ti = (TaskInfo)infoManager.GetTaskByName(gti.TaskName);

                TaskInfoWindow tiw = new TaskInfoWindow(bot, true, ti.name);
                tiw.fillWithTaskInfo(ti);
                tiw.Show();
            }
            else
            {
                MessageBox.Show(Properties.Resources.errorSelectTaskFirst);
            }

        }

        private void butRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            LvTaskItem gti = ((LvTaskItem)lvTasks.SelectedItem);
            if (gti != null)    // something is selected
            {
                infoManager.RemoveTaskByName(gti.TaskName);
                infoManager.updateTasksList();
                infoManager.saveTasksList();
                logMonitor.addLogMessage(Properties.Resources.logTaskRemoved, "BOT");
            }
            else
            {
                MessageBox.Show(Properties.Resources.errorSelectTaskFirst);
            }
        }

        private void butStartTask_Click(object sender, RoutedEventArgs e)
        {
            LvTaskItem obj = ((FrameworkElement)sender).DataContext as LvTaskItem;
            TaskInfo ti = (TaskInfo)infoManager.GetTaskByName(obj.TaskName);

            ProfileInfo pi = (ProfileInfo)infoManager.GetProfileByName(ti.profileName);
            if(pi == null)
            {
                MessageBox.Show(Properties.Resources.errorProfileDoesntExist);
                return;
            }

            if (ti.running) // stop task if running
            {
                bot.abortTaskByName(ti.name);
                
                ((Button)sender).Content = "Start";
                ti.running = false;
            }   
            else    // task not running - start it
            {
                ((Button)sender).Content = "Stop";
                ti.running = true;
                bot.startTaskWithName(ti.name);
            }

        }

        private void butSave_Click(object sender, RoutedEventArgs e)
        {
            ProfileInfo cd = new ProfileInfo();

            cd.name = tbProfileName.Text;
            cd.FullName = tbFullName.Text;
            cd.Email = tbEmail.Text;
            cd.TelNr = tbTel.Text;
            cd.Address1 = tbAddress1.Text;
            cd.Address2 = tbAddress2.Text;
            cd.City = tbCity.Text;
            cd.Postcode = tbPostcode.Text;
            cd.Country = ((ComboBoxItem)cbCountry.SelectedItem).Content.ToString();

            cd.CardType = ((ComboBoxItem)cbCardType.SelectedItem).Tag.ToString();
            cd.CardNr = tbCardno.Text;
            cd.ExpMonth = ((ComboBoxItem)cbCardMonth.SelectedItem).Content.ToString();
            cd.ExpYear = ((ComboBoxItem)cbCardYear.SelectedItem).Content.ToString();
            cd.CVV = tbCVV.Text;

            if (cd.FullName.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataFullName);
                return;
            }
            else if (cd.Email.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataEmail);
                return;
            }
            else if (cd.TelNr.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataTel);
                return;
            }
            else if (cd.Address1.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataAddress1);
                return;
            }
            else if (cd.Postcode.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataPostcode);
                return;
            }
            else if (cd.Country.Equals("-"))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataCountry);
                return;
            }
            else if (cd.CardNr.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataCardNumber);
                return;
            }
            else if (cd.CVV.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataCVV);
                return;
            }
            else if (cd.name.Equals(""))
            {
                MessageBox.Show(Properties.Resources.errorEnter + " " + Properties.Resources.dataRef);
                return;
            }

            infoManager.addProfile(cd);
            infoManager.updateProfilesList();
            infoManager.saveProfilesList();
        }

        private void butClear_Click(object sender, RoutedEventArgs e)
        {
            tbFullName.Text = "";
            tbEmail.Text = "";
            tbAddress1.Text = "";   
            tbAddress2.Text = "";
            tbCity.Text = "";
            tbPostcode.Text = "";
            tbCardno.Text = "";
            tbCVV.Text = "";
            tbProfileName.Text = "";
            tbTel.Text = "";
            cbCountry.SelectedIndex = 0;
            cbCardType.SelectedIndex = 0;
            cbCardMonth.SelectedIndex = 0;
            cbCardYear.SelectedIndex = 0;
        }
        
        private void lvData_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            fillDataView(); 
        }

        private double calculateSize(SizeChangedEventArgs e) {
            double width = e.NewSize.Width;
            double height = e.NewSize.Height;
            return (width * 9 > height * 16) ? height * 16 : width * 9;
        }

        private void settingsGridSizeChanged(object sender, SizeChangedEventArgs e) {
            double size = calculateSize(e);

            List<Control> controls1 = new List<Control>();
            List<Control> controls2 = new List<Control>();

            controls1.Add(labelFullName);
            controls1.Add(labelEmail);
            controls1.Add(labelTel);
            controls1.Add(labelAddress1);
            controls1.Add(labelAddress2);
            controls1.Add(labelCity);
            controls1.Add(labelPostcode);
            controls1.Add(labelCountry);
            controls1.Add(labelCardCvv);
            controls1.Add(labelCardExpire);
            controls1.Add(labelCardNumber);
            controls1.Add(labelCardRef);
            controls1.Add(labelCardType);

            controls1.Add(tbAddress1);
            controls1.Add(tbAddress2);
            controls1.Add(tbCardno);
            controls1.Add(tbCity);
            controls1.Add(tbCVV);
            controls1.Add(tbEmail);
            controls1.Add(tbFullName);
            controls1.Add(tbPostcode);
            controls1.Add(tbTel);
            controls1.Add(tbProfileName);

            controls1.Add(cbCardMonth);
            controls1.Add(cbCardType);
            controls1.Add(cbCardYear);
            controls1.Add(cbCountry);
            controls1.Add(butClear);
            
            controls1.Add(butSave);
            controls1.Add(butEdit);
            controls1.Add(butRemove);
            controls1.Add(lvData);

            controls2.Add(labelPersonalData);
            controls2.Add(labelCardDetails);


            foreach (var control in controls1) {
                control.FontSize = size / (25 * 16);
            }
            foreach (var control in controls2) {
                control.FontSize = size / (20 * 16);
            }
        }
        private void onTabControlSizeChanged(object sender, SizeChangedEventArgs e) {
            double size = calculateSize(e);

            List<TextBlock> textBlocks = new List<TextBlock>();

            textBlocks.Add(tabItemProxyHeader);
            textBlocks.Add(tabItemSettingsHeader);
            textBlocks.Add(tabItemTasksHeader);

            foreach (var textBlock in textBlocks) {
                textBlock.FontSize = size / (25 * 16);
            }
        }

        private void tasksGridSizeChanged(object sender, SizeChangedEventArgs e) {
            double size = calculateSize(e);

            List<Control> controls = new List<Control>();
            controls.Add(butAddTask);
            controls.Add(butEditTask);
            controls.Add(butRemoveTask);
            controls.Add(butSolveCaptchas);
            controls.Add(lvTasks);
            controls.Add(lCapchasSolved);
            controls.Add(tbLogScroll);

            foreach (var control in controls) {
                control.FontSize = size / (25 * 16);
            }
        }


        private void butEdit_Click(object sender, RoutedEventArgs e)
        {
            LvProfileItem lvi = ((LvProfileItem)lvData.SelectedItem);
            if (lvi != null)
            {
                string profileName = lvi.ProfileName;
                ProfileInfo profile = (ProfileInfo)infoManager.GetProfileByName(profileName);


                tbFullName.Text = profile.FullName;
                tbEmail.Text = profile.Email;
                tbAddress1.Text = profile.Address1;
                tbAddress2.Text = profile.Address2;
                tbCity.Text = profile.City;
                tbPostcode.Text = profile.Postcode;
                tbCardno.Text = profile.CardNr;
                tbCVV.Text = profile.CVV;
                tbProfileName.Text = profile.name;
                tbTel.Text = profile.TelNr;
                cbCountry.SelectedValue = profile.Country;
                cbCardType.SelectedValue = profile.CardType;
                cbCardMonth.SelectedValue = profile.ExpMonth;
                cbCardYear.SelectedValue = profile.ExpYear;

                //bot.addClientData(profile);
            }
        }

        private void butRemove_Click(object sender, RoutedEventArgs e)
        {
            LvProfileItem lvi = ((LvProfileItem)lvData.SelectedItem);
            if (lvi != null)
            {
                infoManager.RemoveProfileByName(lvi.ProfileName);
                infoManager.updateProfilesList();
                infoManager.saveProfilesList();
            }
        }

        private void butSolveCaptchas_Click(object sender, RoutedEventArgs e)
        {
            ReCaptchaWindow rcw = new ReCaptchaWindow(bot);
            rcw.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // bot.captchaMonitorThread.Abort();
            Environment.Exit(1);
        }

        private void butProxyAdd_Click(object sender, RoutedEventArgs e)
        {
            ProxyInfo pi = new ProxyInfo();
            pi.name = tbProxyName.Text;
            pi.ip = tbProxyIpAddress.Text;
            pi.username = tbProxyUsername.Text;
            pi.password = tbProxyPassword.Text;

            if(pi.ip != "")
            {
                if (!pi.ip.Contains("http"))
                {
                    pi.ip = "http://" + pi.ip;
                }
            }

            infoManager.addProxy(pi);
            infoManager.updateProxiesList();
            infoManager.saveProxiesList();
        }

        private void butProxyClear_Click(object sender, RoutedEventArgs e)
        {
            tbProxyName.Text = "";
            tbProxyIpAddress.Text = "";
            tbProxyPassword.Text = "";
            tbProxyUsername.Text = "";
        }

        private void butProxyEdit_Click(object sender, RoutedEventArgs e)
        {
            LvProxyItem lvi = ((LvProxyItem)lvProxy.SelectedItem);
            if (lvi != null)
            {
                ProxyInfo pi = (ProxyInfo)infoManager.GetProxyByName(lvi.profileName);
                tbProxyName.Text = pi.name;
                tbProxyIpAddress.Text = pi.ip;
                tbProxyPassword.Text = pi.password;
                tbProxyUsername.Text = pi.username;
            }
        }

        private void butProxyRemove_Click(object sender, RoutedEventArgs e)
        {
            LvProxyItem lvi = ((LvProxyItem)lvProxy.SelectedItem);
            if (lvi != null)
            {
                infoManager.RemoveProxyByName(lvi.profileName);
                infoManager.updateProxiesList();
                infoManager.saveProxiesList();
            }
        }

        private void lvProxy_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int proxyNameWeight = 1;
            int proxyIPWeight = 1;
            int proxyUsernameWeight = 1;
            int weightSum = proxyNameWeight + proxyIPWeight + proxyUsernameWeight;

            double lvWidth = lvProxy.ActualWidth;
            double chunk = lvWidth / weightSum;

            colIP.Width = proxyIPWeight * chunk;
            colUsername.Width = proxyUsernameWeight * chunk;
            colProxyName.Width = proxyNameWeight * chunk;
        }

        private void proxyTabGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double size = calculateSize(e);

            List<Control> controls = new List<Control>();
            controls.Add(lProxy);
            controls.Add(lProxyAddress);
            controls.Add(lProxyName);
            controls.Add(lProxyUsername);
            controls.Add(lProxyPassword);
            controls.Add(lSettings);
            controls.Add(tbProxyName);
            controls.Add(tbProxyPassword);
            controls.Add(tbProxyUsername);
            controls.Add(tbProxyIpAddress);
            controls.Add(lRefreshFreq);
            controls.Add(tbRefresMS);
            controls.Add(butProxyAdd);
            controls.Add(butProxyClear);
            controls.Add(butProxyEdit);
            controls.Add(butProxyRemove);
            controls.Add(lvProxy);
            controls.Add(butSettingsSave);
            foreach (var control in controls)
            {
                control.FontSize = size / (25 * 16);
            }
        }

        private void butSettingsSave_Click(object sender, RoutedEventArgs e)
        {
            string refms = tbRefresMS.Text;
            int res;
            bool success = Int32.TryParse(refms, out res);
            if (success)
            {
                bot.settings.refreshDelayMs = res;  
            }
        }
    }
}
