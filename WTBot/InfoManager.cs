using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WTBot
{
    // communication between bot and window
    public class InfoManager
    {
        const string tasksFileName = "tasks.bot";
        const string profilesFileName = "profiles.bot";
        const string proxiesFileName = "proxies.bot";
          
        private ItemNameIdentifiedList tasksList;
        private ItemNameIdentifiedList profilesList;
        private ItemNameIdentifiedList proxiesList;
        private ListView lvTasks;
        private ListView lvProfiles;
        private ListView lvProxies;
        public InfoManager()
        {

        }
        public ItemNameIdentifiedList GetProxiesList()
        {
            return proxiesList;
        }
        public void provideLists(ItemNameIdentifiedList t, ItemNameIdentifiedList prof, ItemNameIdentifiedList prox)
        {
            tasksList = t;
            profilesList = prof;
            proxiesList = prox;
        }
        public void provideLVS(ListView lvt, ListView lvprof, ListView lvprox)
        {
            lvTasks = lvt;
            lvProfiles = lvprof;
            lvProxies = lvprox;
        }
        public NameIdentifiedInfo GetTaskByName(string name)
        {
            return tasksList.GetItemByName(name);
        }
        public NameIdentifiedInfo GetProfileByName(string name)
        {
            return profilesList.GetItemByName(name);
        }
        public NameIdentifiedInfo GetProxyByName(string name)
        {
            return proxiesList.GetItemByName(name);
        }

        public void RemoveTaskByName(string name)
        {
            tasksList.RemoveItemByName(name);
        }
        public void RemoveProfileByName(string name)
        {
            profilesList.RemoveItemByName(name);
        }
        public void RemoveProxyByName(string name)
        {
            proxiesList.RemoveItemByName(name);
        }

        public void updateTasksList()
        {
            tasksList.updateListView(lvTasks);
        }
        public void updateProfilesList()
        {
            profilesList.updateListView(lvProfiles);
        }
        public void updateProxiesList()
        {
            proxiesList.updateListView(lvProxies);
        }

        public void addProfile(NameIdentifiedInfo info)
        {
            profilesList.AddItem(info);
        }
        public void addTask(NameIdentifiedInfo info)
        {
            tasksList.AddItem(info);
        }
        public void addProxy(NameIdentifiedInfo info)
        {
            proxiesList.AddItem(info);
        }

        public void saveTasksList()
        {
            List<TaskInfo> l = new List<TaskInfo>();
            foreach(var item in tasksList)
            {
                l.Add((TaskInfo)item);
            }
            new Thread(() =>
            {
                ObjectSerializer.SerializeRawData<List<TaskInfo>>(l, tasksFileName);
            }).Start();
        }
        public void loadTasksList()
        {
            if (File.Exists(tasksFileName))
            {
                new Thread(() => {
                    List<TaskInfo> l = new List<TaskInfo>();
                    l = ObjectSerializer.DeSerializeRawData<List<TaskInfo>>(tasksFileName);
                    foreach(var item in l)
                    {
                        addTask(item);
                    }
                    System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        updateTasksList();
                    }));
                }).Start();
            }
        }

        public void saveProfilesList()  
        {
            new Thread(() => {
                List<ProfileInfo> l = new List<ProfileInfo>();
                foreach(var item in profilesList)
                {
                    l.Add((ProfileInfo)item);
                }
                ObjectSerializer.SerializeAndCipher<List<ProfileInfo>>(l, profilesFileName);
            }).Start();
        }

        public void loadProfilesList()
        {
            if (File.Exists(profilesFileName))
            {
                List<ProfileInfo> l = new List<ProfileInfo>();
                l = ObjectSerializer.DeserializeCiphered<List<ProfileInfo>>(profilesFileName);
                foreach(var item in l)
                {
                    addProfile(item);
                }

                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    updateProfilesList();
                }));
            }
        }

        public void saveProxiesList()
        {
            List<ProxyInfo> l = new List<ProxyInfo>();
            foreach (var item in proxiesList)
            {
                l.Add((ProxyInfo)item);
            }
            new Thread(() =>
            {
                ObjectSerializer.SerializeRawData<List<ProxyInfo>>(l, proxiesFileName);
            }).Start();
        }
        public void loadProxiesList()
        {
            if (File.Exists(proxiesFileName))
            {
                new Thread(() => {
                    List<ProxyInfo> l = new List<ProxyInfo>();
                    l = ObjectSerializer.DeSerializeRawData<List<ProxyInfo>>(proxiesFileName);
                    foreach (var item in l)
                    {
                        addProxy(item);
                    }
                    System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        updateProxiesList();
                    }));
                }).Start();
            }
        }
        public ItemNameIdentifiedList GetProfilesList()
        {
            return profilesList;
        }

        public bool anyProfileExists()
        {
            return profilesList.Count > 0 ? true : false;
        }
    }
}
