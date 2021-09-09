using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TweakUtility.Tweaks.Model;

namespace TweakUtility.Tweaks.Views
{
    public partial class EnvironmentVariablesPageView : UserControl
    {
        public EnvironmentVariablesPageView()
        {
            InitializeComponent();
        }

        private void EnvironmentVariablesPageView_Load(object sender, EventArgs e) => LoadVariables();

        private void LoadVariables()
        {
            listView.Items.Clear();

            foreach (var variable in EnvironmentVariable.GetAllEnvironmentVariables())
            {
                var listViewItem = new ListViewItem()
                {
                    Tag = variable
                };

                SetItem(listViewItem);

                listView.Items.Add(listViewItem);
            }
        }

        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];

                //reset fonts
                foreach (ToolStripMenuItem contextItem in contextMenuStrip.Items)
                {
                    contextItem.Font = contextMenuStrip.Font;
                }

                //reset special default actions
                editAsListToolStripMenuItem.Visible = toggleToolStripMenuItem.Visible = false;

                var defaultItem = getDefaultItem(item);
                defaultItem.Font = new Font(defaultItem.Font, defaultItem.Font.Style | FontStyle.Bold);
                defaultItem.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];
                getDefaultItem(item).PerformClick();
            }
        }

        private ToolStripMenuItem getDefaultItem(ListViewItem item)
        {
            var value = item.SubItems[1].Text;

            if (isList(value))
            {
                return editAsListToolStripMenuItem;
            }

            if (isIntegerBool(value) || isStringBool(value))
            {
                return toggleToolStripMenuItem;
            }

            return editToolStripMenuItem;
        }

        private bool isList(string value) => value.Contains(";");

        private bool isIntegerBool(string value) => value == "0" || value == "1";

        private bool isStringBool(string value) => value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("false", StringComparison.OrdinalIgnoreCase);

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];
                var variable = item.Tag as EnvironmentVariable;
                var value = variable.Value;

                if (isIntegerBool(value))
                {
                    if (value == "1")
                    {
                        variable.Value = "0";
                    }
                    else
                    {
                        variable.Value = "1";
                    }
                }
                else if (isStringBool(value))
                {
                    if (value.Equals("true", StringComparison.OrdinalIgnoreCase))
                    {
                        variable.Value = "false";
                    }
                    else
                    {
                        variable.Value = "true";
                    }
                }

                SetItem(item);
            }
        }

        private void SetItem(ListViewItem item)
        {
            if (item.Tag is EnvironmentVariable variable)
            {
                switch (variable.Target.Value)
                {
                    case EnvironmentVariableTarget.User: item.Group = listView.Groups["userListViewGroup"]; break;
                    case EnvironmentVariableTarget.Machine: item.Group = listView.Groups["systemListViewGroup"]; break;
                }

                item.SubItems.Clear();

                item.Text = variable.Name;
                item.ImageKey = GetImageKey(variable);

                item.SubItems.Add(variable.Value);
            }
            else
            {
                throw new ArgumentException($"Specified ListViewItem does not have a tag of {nameof(EnvironmentVariable)}.", nameof(item));
            }
        }

        private string GetImageKey(EnvironmentVariable variable)
        {
            if (isList(variable.Value))
            {
                return "list";
            }
            else if (variable.Value == "0" || variable.Value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return "false";
            }
            else if (variable.Value == "1" || variable.Value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return "true";
            }
            else if (Directory.Exists(variable.Value))
            {
                return "folder";
            }
            else if (File.Exists(variable.Value))
            {
                if (!imageList.Images.ContainsKey(variable.Value))
                {
                    imageList.Images.Add(variable.Value, Icon.ExtractAssociatedIcon(variable.Value));
                }

                return variable.Value;
            }

            return "default";
        }
    }
}