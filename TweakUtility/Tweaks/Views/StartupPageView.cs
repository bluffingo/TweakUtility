using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using TweakUtility.Tweaks.Model;

namespace TweakUtility.Tweaks.Views
{
    public partial class StartupPageView : UserControl
    {
        public StartupPageView()
        {
            this.InitializeComponent();
        }

        private void StartupPageView_Load(object sender, EventArgs e)
        {
            string getTypeName(StartupItem item)
            {
                if (item is StartupFolderItem folderItem)
                {
                    return folderItem.Public ? "Public Startup Folder" : "Startup Folder";
                }

                return "N/A";
            }

            var items =
                GetStartupFolderItems();

            listView.Items.Clear();

            foreach (StartupItem item in items)
            {
                var listViewItem = new ListViewItem(item.Name);

                listViewItem.SubItems.AddRange(new[]
                {
                    item.Path,
                    getTypeName(item)
                });

                listView.Items.Add(listViewItem);
            }
        }

        public List<StartupFolderItem> GetStartupFolderItems()
        {
            var items = new List<StartupFolderItem>();

            string commonStartupPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
            string startupPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            var paths = Directory.GetFiles(commonStartupPath).Concat(Directory.GetFiles(startupPath));

            foreach (var path in paths)
            {
                if (new FileInfo(path).Attributes.HasFlag(FileAttributes.System))
                {
                    continue;
                }

                items.Add(new StartupFolderItem(path));
            }

            return items;
        }
    }
}