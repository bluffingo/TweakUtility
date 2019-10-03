using Microsoft.VisualBasic;
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Pages;

namespace TweakUtility.Tweaks.Views
{
    internal partial class DiskCleanupPageView : UserControl
    {
        private List<DiskCleanupHandler> Handlers { get; } = new List<DiskCleanupHandler>();

        internal DiskCleanupPageView()
        {
            this.InitializeComponent();
            this.Padding = new Padding(SystemInformation.VerticalScrollBarWidth);
        }

        private void DiskCleanupPageView_Load(object sender, EventArgs e)
        {
            propertyGrid.CommandsVisibleIfAvailable = false;

            this.Handlers.Clear();

            using (RegistryKey key = RegistryHelper.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VolumeCaches\"))
            {
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    RegistryKey subKey = key.OpenSubKey(subKeyName, true);

                    if ((string)subKey.GetValue(null, "") != "{C0E13E61-0CC6-11d1-BBB6-0060978B2AE6}")
                    {
                        subKey.Close();
                        continue;
                    }

                    this.Handlers.Add(new DiskCleanupHandler(subKey));
                }
            }

            listView.Items.Clear();
            imageList.Images.Clear();

            foreach (DiskCleanupHandler handler in this.Handlers)
            {
                this.AddHandler(handler);
            }
        }

        private void AddHandler(DiskCleanupHandler handler)
        {
            var item = new ListViewItem(handler.DisplayName)
            {
                Tag = handler
            };

            Icon icon = handler.Icon ?? Icons.File;

            imageList.Images.Add(handler.KeyName, icon);
            item.ImageKey = handler.KeyName;

            listView.Items.Add(item);
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            removeButton.Enabled = e.Item != null;
            propertyGrid.SelectedObject = e.Item?.Tag;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox("Enter the name of the new item:");

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            using (RegistryKey key = RegistryHelper.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VolumeCaches\"))
            {
                RegistryKey subKey = key.CreateSubKey(name);
                subKey.SetValue(null, "{C0E13E61-0CC6-11d1-BBB6-0060978B2AE6}");

                var handler = new DiskCleanupHandler(subKey);
                this.Handlers.Add(handler);
                this.AddHandler(handler);
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem item = listView.SelectedItems[0];

            if (item.Tag is DiskCleanupHandler handler)
            {
                using (RegistryKey key = RegistryHelper.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VolumeCaches\"))
                {
                    key.DeleteSubKeyTree(Path.GetFileName(handler.KeyName));
                }

                this.Handlers.Remove(handler);
                listView.Items.Remove(item);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e) => this.DiskCleanupPageView_Load(sender, e);

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
        }
    }
}