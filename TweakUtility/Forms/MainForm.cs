using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Theming;
using TweakUtility.TweakPages;
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
            var pages = Program.GetAllTweakPages().ToList();
            pages.Add(new DeadMemeCreeperPage());
            return pages;
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
                        return true;
                }

                return false;
            }

            foreach (TreeNode node in treeView.Nodes)
            {
                if (Find(node, tweakPage))
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }

                this.Focus();
            }

            base.WndProc(ref m);
        }

        private void AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }

        private void BackupsButton_Click(object sender, EventArgs e)
        {
            using (var form = new BackupsForm())
            {
                form.ShowDialog();
            }
        }

        private void ExtensionsButton_Click(object sender, EventArgs e)
        {
            using (var form = new ExtensionsForm())
            {
                form.ShowDialog();
            }
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
            //Adds tooltips for the bottom buttons
            toolTip.SetToolTip(preferencesButton, Properties.Strings.Preferences);
            toolTip.SetToolTip(backupsButton, Properties.Strings.Backups);
            toolTip.SetToolTip(extensionsButton, Properties.Strings.Extensions);

            //Loads icons for the bottom buttons
            preferencesButton.Image = Icons.Options.ToBitmap();
            backupsButton.Image = Icons.RecentDocuments.ToBitmap();
            extensionsButton.Image = Icons.SystemFile.ToBitmap();

            imageList.Images.Add("default", Icons.Folder);
            foreach (TweakPage page in Program.Pages)
                this.AddPage(page);

            this.LoadWindowRectangle();
            Theme.Apply(this);

            //Fixes design bug where the layout seems a bit off in Windows XP
            //for not having different bottom panel background colors.
            if (!IsSupported(OperatingSystemVersion.WindowsVista))
                splitContainer.Height += 10;

            this.LayoutSidebar();

            this.OpenPageFromArguments();
        }

        private void MainForm_Shown(object sender, EventArgs e) => this.SetupTaskbar();

        private void MainForm_SizeChanged(object sender, EventArgs e) => this.LayoutSidebar();

        private void OpenPageFromArguments()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (!args[i].Equals("--open", StringComparison.OrdinalIgnoreCase))
                    continue;

                i++; //move +1 to get page type name
                string targetType = args[i];

                TweakPage targetPage = this.GetAllTweakPages().FirstOrDefault(t => t.GetType().FullName.Equals(targetType, StringComparison.InvariantCultureIgnoreCase));

                if (targetPage != null)
                    this.Select(targetPage);
            }
        }

        private void SaveWindowRectangle()
        {
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowLeft", this.Left);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowTop", this.Top);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowWidth", this.Width);
            RegistryHelper.SetValue(@"HKCU\Software\Craftplacer\TweakUtility\WindowHeight", this.Height);
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (TriggerCreeperPage(searchTextBox.Text))
            {
                foreach (TweakPage tweakPage in this.GetAllTweakPages())
                {
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
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void SetupTaskbar()
        {
            if (!TaskbarManager.IsPlatformSupported)
                return;

            TaskbarManager.Instance.ApplicationId = Constants.Application_Id;

            var jumpList = JumpList.CreateJumpList();
            var pages = this.GetAllTweakPages();
            var tasks = new JumpListTask[pages.Count];

            jumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;

            for (int i = 0; i < pages.Count; i++)
            {
                var page = pages[i];
                var id = page.GetType().FullName;

                //damnit, my screen just froze and now firefox is laggy,
                //firefox is too lazy to load discord, i'm guessing my driver is glitchy or something
                //brb gonna load chrome, oh, it loaded, great
                //the day is saved, firefox decided to load discord.
                //           -PF94, August 15th 2019.

                tasks[i] = new JumpListLink(Assembly.GetExecutingAssembly().Location, page.Name)
                {
                    ShowCommand = WindowShowCommand.Restore,
                    IconReference = new IconReference(page.ExportIcon(), 0),
                    Arguments = "--open " + id
                };
            }

            jumpList.AddUserTasks(tasks);
            jumpList.Refresh();
        }

        private void SetView(TweakPage tweakPage)
        {
            Control control = tweakPage.CustomView ?? new TweakPageView(tweakPage);

            control.Dock = DockStyle.Fill;

            splitContainer.Panel2.Controls.Clear();
            splitContainer.Panel2.Controls.Add(control);
        }

        //i did this shit (live share) on a live share session of botrappa a while back :P -PF94
        private void SplitContainer_BorderPaint(object sender, PaintEventArgs e)
        {
            if (sender == splitContainer.Panel1)
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, treeView.Left - 1, treeView.Top - 1, treeView.Width + 2, treeView.Height + 2);
            else
                e.Graphics.FillRectangle(SystemBrushes.ControlDark, new Rectangle(Point.Empty, ((SplitterPanel)sender).Size));
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e) => this.LayoutSidebar();

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is TweakPage tweakPage)
                this.SetView(tweakPage);
        }

        private void PreferencesButton_Click(object sender, EventArgs e)
        {
            this.treeView.SelectedNode = null;
            this.SetView(new PreferencesPage());
        }
    }
}