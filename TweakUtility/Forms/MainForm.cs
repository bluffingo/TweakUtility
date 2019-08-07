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
                tweakPages.AddRange(this.GetSubTweakPages(page));
            }
            tweakPages.Add(new CreeperPage());

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
                tweakPages.AddRange(this.GetSubTweakPages(subPage));
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
            //Adds tool tips for the bottom buttons
            toolTip.SetToolTip(backupsButton, Properties.Strings.Backups);
            toolTip.SetToolTip(extensionsButton, Properties.Strings.Extensions);

            //Loads icons for the bottom buttons
            backupsButton.Image = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -21).ToBitmap();
            extensionsButton.Image = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -154).ToBitmap();

            imageList.Images.Add("default", NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -4));
            foreach (TweakPage page in Program.Pages)
            {
                this.AddPage(page);
            }

            this.LoadWindowRectangle();

            Theme.Apply(this);

            //Fixes design bug where the layout seems a bit off in Windows XP
            //for not having different bottom panel background colors.
            if (!IsSupported(OperatingSystemVersion.WindowsVista))
            {
                splitContainer.Height += 10;
            }

            this.LayoutSidebar();
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
                this.SetView(tweakPage);
            }
        }

        private void SetView(TweakPage tweakPage)
        {
            Control control = tweakPage.CustomView ?? new TweakPageView(tweakPage);

            control.Dock = DockStyle.Fill;

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(control);
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (TweakPage tweakPage in this.GetAllTweakPages())
                {
                    if (searchTextBox.Text.Equals("Creeper, aw man", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SetView(new CreeperPage());
                    }

                    //Searches for a page matching the query (case-insensitive)
                    if (tweakPage.Name.Equals(searchTextBox.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        this.Select(tweakPage);
                        break;
                    }

                    //Searches for an entry inside the page matching the query (case-insensitive)
                    foreach (TweakEntry entry in tweakPage.Entries)
                    {
                        if (entry.Name.Equals(searchTextBox.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            this.Select(tweakPage);
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

        private void BackupsButton_Click(object sender, EventArgs e)
        {
            using (var form = new BackupsForm())
            {
                form.ShowDialog();
            }
        }

        private class CreeperPage : TweakPage
        {
            public CreeperPage() : base("Creeper, aw man")
            {
            }

            [DisplayName(".")]
            public string t1 => "So we back in the mine, got our pickaxe swingin' from side to side, side, side to side";

            [DisplayName(".")]
            public string t2 => "This task a grueling one, hope to find some diamonds tonight, night, night, diamonds tonight";

            [DisplayName(".")]
            public string t3 => "Heads up";

            [DisplayName(".")]
            public string t4 => "You hear a sound, turn around and look up";

            [DisplayName(".")]
            public string t5 => "Total shock fills your body";

            [DisplayName(".")]
            public string t6 => "Oh no it's you again, I can never forget those eyes, eyes, eyes, eyes, eyes, eyes";

            [DisplayName(".")]
            public string t7 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string t8 => "Cause baby tonight, you grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string t9 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string t10 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again, stuff again, gain";

            [DisplayName(".")]
            public string t11 => "Just when you think you're safe, overhear some hissing from right behind, right, right behind";

            [DisplayName(".")]
            public string t12 => "That's a nice life you have, shame it's gotta end at this time, time, time, time, time, time, time";

            [DisplayName(".")]
            public string t13 => "Blows up, then your health bar drops and you could use a 1-up";

            [DisplayName(".")]
            public string t14 => "Get inside don't be tardy";

            [DisplayName(".")]
            public string t15 => "So now you're stuck in there, half a heart is left but don't die, die, die, die, die, die";

            [DisplayName(".")]
            public string t16 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string t17 => "Cause baby tonight, grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string t18 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string t19 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string t20 => "Creepers, you're mine ha ha";

            [DisplayName(".")]
            public string t21 => "Dig up diamonds, craft those diamonds, make some armor";

            [DisplayName(".")]
            public string t22 => "Get it baby, go and forge that like you so, TweakUtility pro";

            [DisplayName(".")]
            public string t23 => "The sword's made of diamonds, so come at me bro";

            [DisplayName(".")]
            public string t24 => "Ha, training in your room under the torch-light";

            [DisplayName(".")]
            public string t25 => "Hone that form to get you ready for the big fight";

            [DisplayName(".")]
            public string t26 => "Every single day in the whole night";

            [DisplayName(".")]
            public string t27 => "Creeper's out prowlin', (Whoo), alright";

            [DisplayName(".")]
            public string t28 => "Look at me, look at you";

            [DisplayName(".")]
            public string t29 => "Take my revenge that's what I'm gonna do";

            [DisplayName(".")]
            public string t30 => "I'm a warrior baby, what else is new";

            [DisplayName(".")]
            public string t31 => "And my blade's gonna tear through you";

            [DisplayName(".")]
            public string t32 => "Bring it";

            [DisplayName(".")]
            public string t33 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again (Get your stuff)";

            [DisplayName(".")]
            public string t34 => "Yea, let's take back the world";

            [DisplayName(".")]
            public string t35 => "Yea baby tonight, grab your sword armor and go (It's on)";

            [DisplayName(".")]
            public string t36 => "Take your revenge (Whoo)";

            [DisplayName(".")]
            public string t37 => "Oh so fight, fight, like it's the last, last night of your life, life show them your bite (Whoo)";

            [DisplayName(".")]
            public string t38 => "Cause baby tonight, the creeper's tryin' to steal our stuff again";

            [DisplayName(".")]
            public string t39 => "Cause baby tonight, grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string t40 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string t41 => "Cause baby tonight, come on, the creeper's, come on, tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string t42 => "(Whoo)";
        }
    }
}
