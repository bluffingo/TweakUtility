using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Forms;

namespace TweakUtility.Tweaks.Views
{
    public partial class HostsPageView : UserControl
    {
        public static string path = Path.Combine(Environment.ExpandEnvironmentVariables("%windir%"), @"system32\drivers\etc\hosts");
        private List<string> hosts;

        public HostsPageView()
        {
            InitializeComponent();
            this.Padding = new Padding(SystemInformation.VerticalScrollBarWidth);
            this.openButton.Image = Icons.Notepad.ToBitmap();

            this.addButton.Text = Properties.Strings.Entry_Add;
            this.deleteButton.Text = Properties.Strings.Entry_Remove;
            this.editButton.Text = Properties.Strings.Entry_Edit;
            this.openButton.Text = Properties.Strings.OpenInNotepad;
        }

        private void HostsPageView_Load(object sender, EventArgs e)
        {
            this.hosts = File.ReadAllLines(path).ToList();

            listView.Items.Clear();
            for (int i = 0; i < hosts.Count; i++)
            {
                string line = hosts[i];

                if (line.Length == 0 || line[0] == '#')
                    continue;

                var split = line.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 0)
                    continue;

                var item = new ListViewItem(split[0])
                {
                    Tag = i,
                    ImageKey = isLocalHost(split[0]) ? "loopback" : "direct"
                };

                item.SubItems.Add(split[1]);

                listView.Items.Add(item);
            }
        }

        private bool isLocalHost(string ip) => ip == "127.0.0.1" || ip == "::1";

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => editButton.Enabled = deleteButton.Enabled = listView.SelectedItems.Count != 0;

        #region Button Events

        private void AddButton_Click(object sender, EventArgs e)
        {
            var newValues = HostsItemForm.ShowDialog(this.ParentForm);

            if (newValues == null)
                return;

            hosts.Add(string.Join("\t", newValues));

            File.WriteAllLines(path, hosts);

            HostsPageView_Load(null, null);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var item = listView.SelectedItems[0];
            if (item?.Tag is int i)
            {
                var newValues = HostsItemForm.ShowDialog(this.ParentForm, item.SubItems[0].Text, item.SubItems[1].Text);

                if (newValues == null)
                    return;

                hosts[i] = string.Join("\t", newValues);

                File.WriteAllLines(path, hosts);

                HostsPageView_Load(null, null);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var item = listView.SelectedItems[0];
            if (item?.Tag is int i)
            {
                hosts.RemoveAt(i);

                File.WriteAllLines(path, hosts);

                HostsPageView_Load(null, null);
                ListView_ItemSelectionChanged(null, null);
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("notepad")
            {
                Verb = "runas",
                UseShellExecute = true,
                Arguments = path
            });
        }

        #endregion Button Events

        #region Input Events

        private void ListView_ItemActivate(object sender, EventArgs e) => EditButton_Click(null, null);

        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteButton_Click(null, null);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                DeleteButton_Click(null, null);
        }

        #endregion Input Events
    }
}