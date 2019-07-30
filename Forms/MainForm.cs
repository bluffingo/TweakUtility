using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Theming;
using TweakUtility.TweakPages;

using static TweakUtility.OperatingSystemVersions;

namespace TweakUtility.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        public Control CurrentPageView => splitContainer.Panel2.Controls.Find("content", false)[0];

        public TweakPage CurrentTweakPage => treeView.SelectedNode.Tag is TweakPage tweakPage ? tweakPage : null;

        public void AddPage(TweakPage page, TreeNode parent = null)
        {
            var treeNode = new TreeNode()
            {
                Text = page.Name,
                Tag = page
            };

            if (page.Icon != null)
            {
                string id = page.GetType().Name;

                if (page.Icon is Icon icon)
                {
                    imageList.Images.Add(id, icon);
                }
                else if (page.Icon is Image image)
                {
                    imageList.Images.Add(id, image);
                }

                treeNode.ImageKey = id;
                treeNode.StateImageKey = id;
                treeNode.SelectedImageKey = id;
            }

            foreach (TweakPage subPage in page.SubPages)
            {
                this.AddPage(subPage, treeNode);
            }

            if (parent == null)
            {
                treeView.Nodes.Add(treeNode);
            }
            else
            {
                parent.Nodes.Add(treeNode);
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

        private void AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowLeft", this.Left);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowTop", this.Top);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowWidth", this.Width);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowHeight", this.Height);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int optimalLeft = 0;
            int optimalTop = 0;

            this.Width = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowWidth", this.Width);
            this.Height = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowHeight", this.Height);

            this.Left = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowLeft", this.Left);

            if (this.Left < 0)
            {
                this.Left = 0;
            }
            else if ((optimalLeft = Screen.PrimaryScreen.Bounds.Width - this.Width) < this.Left)
            {
                this.Left = optimalLeft;
            }

            this.Top = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowTop", this.Top);

            if (this.Top < 0)
            {
                this.Top = 0;
            }
            else if ((optimalTop = Screen.PrimaryScreen.Bounds.Height - this.Width) < this.Top)
            {
                this.Top = optimalTop;
            }

            imageList.Images.Add("default", Program.FolderIcon);

            foreach (TweakPage page in Program.Pages)
            {
                this.AddPage(page);
            }

            Theme.Apply(this);

            if (!IsSupported(OperatingSystemVersion.WindowsVista))
            {
                splitContainer.Height += 10;
            }
        }

        private void RevertButton_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageView is PropertyGrid propertyGrid)
            {
                PropertyDescriptor descriptor = propertyGrid.SelectedGridItem.PropertyDescriptor;

                if (descriptor.CanResetValue(this.CurrentTweakPage))
                {
                    descriptor.ResetValue(this.CurrentTweakPage);
                    propertyGrid.SelectedGridItem.Select();
                }
            }
        }

        //i did this shit (live share) on a live share session of botrappa a while back :P -PF94
        private void SplitContainer_BorderPaint(object sender, PaintEventArgs e) => e.Graphics.FillRectangle(SystemBrushes.ControlDark, new Rectangle(Point.Empty, ((SplitterPanel)sender).Size));

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
    }
}