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
        public List<TweakPage> Pages = new List<TweakPage>()
        {
            new CustomizationPage(),
            new InternetExplorerPage(),
            new AdvancedPage(),
            new UncategorizedPage()
        };

        public PropertyGrid CurrentPropertyGrid => (PropertyGrid)splitContainer.Panel2.Controls.Find("propertyGrid", false)[0];

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //propertyGrid1.

            foreach (TweakPage page in Pages)
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
                splitContainer.Panel2.Controls.Clear();

                Control control;

                if (tweakPage.CustomView == null)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(tweakPage))
                    {
                        var supportedAttribute = (OperatingSystemSupportedAttribute)descriptor.Attributes[typeof(OperatingSystemSupportedAttribute)];

                        if (supportedAttribute == null)
                        {
                            continue;
                        }

                        bool supported = Program.IsSupported(supportedAttribute.Mininum, supportedAttribute.Maximum);

                        var browsableAttribute = (BrowsableAttribute)descriptor.Attributes[typeof(BrowsableAttribute)];
                        FieldInfo isBrowsable = browsableAttribute.GetType().GetField("browsable", BindingFlags.NonPublic | BindingFlags.Instance);
                        isBrowsable.SetValue(browsableAttribute, supported);
                    }

                    control = new PropertyGrid()
                    {
                        SelectedObject = tweakPage,
                        Name = "propertyGrid"
                    };
                }
                else
                {
                    control = (Control)tweakPage.CustomView;
                }

                splitContainer.Panel2.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        private void AboutLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(new ProcessStartInfo("https://github.com/Craftplacer/TweakUtility") { UseShellExecute = true });

        private void RevertButton_Click(object sender, EventArgs e)
        {
            var descriptor = CurrentPropertyGrid.SelectedGridItem.PropertyDescriptor;
            var attribute = (DefaultValueAttribute)descriptor.Attributes[typeof(DefaultValueAttribute)];

            if (attribute == null)
            {
                return;
            }

            descriptor.SetValue(CurrentPropertyGrid.SelectedObject, attribute.Value);

            CurrentPropertyGrid.SelectedGridItem.Select();
        }
    }
}