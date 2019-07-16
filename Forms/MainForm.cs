using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using TweakUtility.TweakPages;

namespace TweakUtility.Forms
{
    public partial class MainForm : Form
    {
        public Control CurrentPageView => splitContainer.Panel2.Controls.Find("content", false)[0];

        public TweakPage CurrentTweakPage => treeView.SelectedNode.Tag is TweakPage tweakPage ? tweakPage : null;

        public MainForm() => this.InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (TweakPage page in Program.Pages)
            {
                AddPage(page);
            }

            Program.Config.CurrentTheme.Apply(this);
        }

        public void AddPage(TweakPage page, TreeNode parent = null)
        {
            var tn = new TreeNode()
            {
                Text = page.Name,
                Tag = page
            };

            foreach (TweakPage subPage in page.SubPages)
            {
                AddPage(subPage, tn);
            }

            if (parent == null)
            {
                treeView.Nodes.Add(tn);
            }
            else
            {
                parent.Nodes.Add(tn);
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TweakPage tweakPage)
            {
                Control control;

                if (tweakPage.CustomView == null)
                {
                    var propertyGrid = new PropertyGrid()
                    {
                        SelectedObject = tweakPage,
                        Name = "content",
                        ToolbarVisible = false,
                    };

                    control = propertyGrid;

                    propertyGrid.SelectedGridItemChanged += (s, e2) =>
                    {
                        if (e2.NewSelection.GridItemType == GridItemType.Property)
                        {
                            PropertyDescriptor descriptor = e2.NewSelection.PropertyDescriptor;
                            revertButton.Enabled = descriptor.CanResetValue(null);
                        }
                        else
                        {
                            revertButton.Enabled = false;
                        }
                    };

                    propertyGrid.PropertyValueChanged += (s, e2) =>
                    {
                        RefreshRequiredAttribute attribute = e2.ChangedItem.PropertyDescriptor.GetAttribute<RefreshRequiredAttribute>();

                        if (attribute == null)
                        {
                            return;
                        }

                        if (attribute.Type == RestartType.ExplorerRestart)
                        {
                            DialogResult result = MessageBox.Show(
                                    "This option requires you to restart Windows Explorer.\nWould you like to do that now?",
                                    "Tweak Utility",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Program.RestartExplorer();
                            }
                        }
                        else if (attribute.Type == RestartType.SystemRestart)
                        {
                            DialogResult result = MessageBox.Show(
                                    "This option requires you to restart your system.\nWould you like to do that now?",
                                    "Tweak Utility",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, NativeMethods.ShutdownReason.MinorReconfig);
                            }
                        }
                        else if (attribute.Type == RestartType.Logoff)
                        {
                            DialogResult result = MessageBox.Show(
                                    "This option requires you to log off.\nWould you like to do that now?",
                                    "Tweak Utility",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.LogOff, NativeMethods.ShutdownReason.MinorReconfig);
                            }
                        }
                    };
                }
                else
                {
                    control = tweakPage.CustomView;
                }

                splitContainer.Panel2.Controls.Clear();
                splitContainer.Panel2.Controls.Add(control);

                control.Dock = DockStyle.Fill;

                revertButton.Enabled = false;
            }
        }

        private void AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(new ProcessStartInfo("https://github.com/Craftplacer/TweakUtility") { UseShellExecute = true });

        private void RevertButton_Click(object sender, EventArgs e)
        {
            if (CurrentPageView is PropertyGrid propertyGrid)
            {
                PropertyDescriptor descriptor = propertyGrid.SelectedGridItem.PropertyDescriptor;

                if (descriptor.CanResetValue(CurrentTweakPage))
                {
                    descriptor.ResetValue(CurrentTweakPage);
                    propertyGrid.SelectedGridItem.Select();
                }
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        /// <summary>
        /// magical function
        /// </summary>
        /// <param name="tweakPage"></param>
        public void Select(TweakPage tweakPage)
        {
            bool Find(TreeNode node, TweakPage page)
            {
                if (node.Tag is TweakPage tagPage && tagPage == page)
                {
                    treeView.SelectedNode = node;
                    return true;
                }

                foreach (TreeNode subNode in node.Nodes)
                {
                    if (Find(subNode, page))
                    {
                        return true;
                    }
                }

                return false;
            }

            foreach (TreeNode node in treeView.Nodes)
            {
                if (Find(node, tweakPage))
                {
                    break;
                }
            }
        }
    }
}