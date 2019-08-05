using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Theming;

using static TweakUtility.Helpers.OperatingSystemVersions;

namespace TweakUtility.Forms
{
    internal partial class MainForm : Form
    {
        internal MainForm()
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

            searchTextBox.AutoCompleteCustomSource.Add(page.Name);

            foreach (TweakEntry entry in page.Entries)
            {
                searchTextBox.AutoCompleteCustomSource.Add(entry.Name);
            }
        }

        public List<TweakPage> GetAllTweakPages()
        {
            var tweakPages = new List<TweakPage>();

            foreach (TweakPage page in Program.Pages)
            {
                tweakPages.AddRange(GetSubTweakPages(page));
            }

            return tweakPages;
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

        private List<TweakPage> GetSubTweakPages(TweakPage page)
        {
            var tweakPages = new List<TweakPage>() { page };

            foreach (TweakPage subPage in page.SubPages)
            {
                tweakPages.AddRange(GetSubTweakPages(subPage));
            }

            return tweakPages;
        }

        private void LayoutSidebar()
        {
            //TreeView is 6 pixels under the searching TextBox
            this.treeView.Top = this.searchTextBox.Height + 6;

            //TreeView fills the free space while leaving 1 pixel for the border
            this.treeView.Height = this.splitContainer.Panel1.Height - this.treeView.Top - 1;

            this.searchTextBox.Width = this.splitContainer.Panel1.Width;
            this.treeView.Width = (this.splitContainer.Panel1.Width - 2);
            this.treeView.Left = 1;
        }

        private void LoadWindowRectangle()
        {
            this.Width = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowWidth", this.Width);
            this.Height = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowHeight", this.Height);

            this.Left = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowLeft", this.Left);

            int optimalLeft;
            if (this.Left < 0)
            {
                this.Left = 0;
            }
            else if ((optimalLeft = Screen.PrimaryScreen.Bounds.Width - this.Width) < this.Left)
            {
                this.Left = optimalLeft;
            }

            this.Top = RegistryHelper.GetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowTop", this.Top);

            int optimalTop;
            if (this.Top < 0)
            {
                this.Top = 0;
            }
            else if ((optimalTop = Screen.PrimaryScreen.Bounds.Height - this.Width) < this.Top)
            {
                this.Top = optimalTop;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => this.SaveWindowRectangle();

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadWindowRectangle();

            imageList.Images.Add("default", Program.FolderIcon);

            foreach (TweakPage page in Program.Pages)
            {
                this.AddPage(page);
            }

            Theme.Apply(this);

            //Fixes design bug where the layout seems a bit off in Windows XP
            //for not having different bottom panel background colors.
            if (!IsSupported(OperatingSystemVersion.WindowsVista))
            {
                splitContainer.Height += 10;
            }

            LayoutSidebar();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e) => this.LayoutSidebar();

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

        private void SaveWindowRectangle()
        {
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowLeft", this.Left);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowTop", this.Top);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowWidth", this.Width);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowHeight", this.Height);
        }

        //i did this shit (live share) on a live share session of botrappa a while back :P -PF94
        private void SplitContainer_BorderPaint(object sender, PaintEventArgs e)
        {
            if (sender == splitContainer.Panel1)
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, treeView.Left - 1, treeView.Top - 1, treeView.Width + 2, treeView.Height + 2);
            }
            else
            {
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, new Rectangle(Point.Empty, ((SplitterPanel)sender).Size));
            }
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e) => this.LayoutSidebar();

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

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (TweakPage tweakPage in GetAllTweakPages())
                {
                    //Searches for a page matching the query (case-insensitive)
                    if (tweakPage.Name.Equals(searchTextBox.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Select(tweakPage);
                        break;
                    }

                    //Searches for an entry inside the page matching the query (case-insensitive)
                    foreach (TweakEntry entry in tweakPage.Entries)
                    {
                        if (entry.Name.Equals(searchTextBox.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Select(tweakPage);
                            break;
                        }
                    }
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ExtensionsButton_Click(object sender, EventArgs e)
        {
            using (var form = new ExtensionsForm())
            {
                form.ShowDialog();
            }
        }
    }
}