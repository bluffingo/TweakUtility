using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;

using TweakUtility.Enums;
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
			this.treeView.Width = this.splitContainer.Panel1.Width - 2;
			this.treeView.Left = 1;
		}

		private void LoadPages()
		{
			imageList.Images.Add("default", Icons.Folder);
			foreach (TweakPage page in Program.Pages)
				this.AddPage(page);
		}

		private void LoadWindowRectangle()
		{
			if (!Properties.Settings.Default.SaveWindowInfo)
				return;

			this.Location = Properties.Settings.Default.WindowPosition;
			this.Size = Properties.Settings.Default.WindowSize;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveWindowRectangle();
			Properties.Settings.Default.Save();
		}

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

			this.LoadPages();
			this.LoadWindowRectangle();
			Theme.Apply(this);

			//Fixes design bug where the layout seems a bit off in Windows XP because
			//the bottom panel doesn't have different background colors (Aero).
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

				TweakPage targetPage = Program.GetAllTweakPages().FirstOrDefault(t => t.GetType().FullName.Equals(targetType, StringComparison.OrdinalIgnoreCase));

				if (targetPage != null)
					this.Select(targetPage);
			}
		}

		private void PreferencesButton_Click(object sender, EventArgs e)
		{
			this.treeView.SelectedNode = null;
			this.SetView(new PreferencesPage());
		}

		private void SaveWindowRectangle()
		{
			if (!Properties.Settings.Default.SaveWindowInfo)
				return;

			Properties.Settings.Default.WindowPosition = this.Location;
			Properties.Settings.Default.WindowSize = this.Size;
		}

		private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;

			foreach (TweakPage tweakPage in Program.GetAllTweakPages())
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

			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		private void SetupTaskbar()
		{
			if (!TaskbarManager.IsPlatformSupported)
				return;

			TaskbarManager.Instance.ApplicationId = Constants.Application_Id;

			var jumpList = JumpList.CreateJumpList();
			var pages = Program.GetAllTweakPages();
			var tasks = new JumpListTask[pages.Count()];

			jumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;

			for (int i = 0; i < pages.Count(); i++)
			{
				var page = pages.ElementAt(i);
				var id = page.GetType().FullName;

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
	}
}