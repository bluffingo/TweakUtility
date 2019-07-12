using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    public partial class MainForm : Form
    {
        public PropertyGrid CurrentPropertyGrid => (PropertyGrid)splitContainer.Panel2.Controls.Find("content", false)[0];

        public MainForm() => this.InitializeComponent();

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (TweakPage page in Program.Pages)
            {
                AddPage(page);
            }
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
                ///#region Compatibility Check
                ///
                ///foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(tweakPage))
                ///{
                ///    OperatingSystemSupportedAttribute supportedAttribute = (OperatingSystemSupportedAttribute)descriptor.Attributes[typeof(OperatingSystemSupportedAttribute)];
                ///
                ///    if (supportedAttribute != null)
                ///    {
                ///        bool supported = Program.IsSupported(supportedAttribute.Mininum, supportedAttribute.Maximum);
                ///
                ///        BrowsableAttribute browsableAttribute = (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
                ///        var field = browsableAttribute.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
                ///        field.SetValue(browsableAttribute, supported);
                ///    }
                ///}
                ///
                ///#endregion Compatibility Check

                Control control;

                if (tweakPage.CustomView == null)
                {
                    var propertyGrid = new PropertyGrid()
                    {
                        SelectedObject = tweakPage,
                        Name = "content"
                    };

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
                                    "This option requires you to logoff.\nWould you like to do that now?",
                                    "Tweak Utility",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.LogOff, NativeMethods.ShutdownReason.MinorReconfig);
                            }
                        }
                    };

                    control = propertyGrid;
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
            PropertyDescriptor descriptor = CurrentPropertyGrid.SelectedGridItem.PropertyDescriptor;
            descriptor.ResetValue(descriptor);
        }
    }
}