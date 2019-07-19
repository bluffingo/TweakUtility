using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using TweakUtility.Attributes;
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
            //TODO: If TweakPageView hits completion, deprecate PropertyGrid and make it optional with a property.

            if (e.Node.Tag is TweakPage tweakPage)
            {
                Control control;

                if (tweakPage.CustomView == null)
                {
                    control = new TweakPageView(tweakPage);
                }
                else
                {
                    control = tweakPage.CustomView;
                }

                control.Dock = DockStyle.Fill;

                splitContainer.Panel2.Controls.Clear();
                splitContainer.Panel2.Controls.Add(control);
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