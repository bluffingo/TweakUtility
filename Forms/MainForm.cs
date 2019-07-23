using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TweakUtility.Attributes;
using TweakUtility.Theming;
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
                var attribute = page.GetAttribute<OperatingSystemSupportedAttribute>();
                if (attribute != null && !attribute.IsSupported())
                {
                    continue;
                }

                AddPage(page);
            }

            Theme.Apply(this);
        }

        public void AddPage(TweakPage page, TreeNode parent = null)
        {
            var tn = new TreeNode()
            {
                Text = page.Name,
                Tag = page
            };

            if (page.Icon != null)
            {
                string id = page.GetType().Name;
                imageList.Images.Add(id, page.Icon);
                tn.ImageKey = id;
                tn.StateImageKey = id;
                tn.SelectedImageKey = id;
            }

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
                Control control = tweakPage.CustomView ?? new TweakPageView(tweakPage);

                control.Dock = DockStyle.Fill;

                splitContainer.Panel2.Controls.Clear();
                splitContainer.Panel2.Controls.Add(control);
            }
        }

        private void AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

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

        //i did this shit (live share) on a live share session of botrappa a while back :P -PF94
        private void SplitContainer_BorderPaint(object sender, PaintEventArgs e) => e.Graphics.FillRectangle(SystemBrushes.ControlDark, new Rectangle(Point.Empty, ((SplitterPanel)sender).Size));
    }
}