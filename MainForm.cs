using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            new AdvancedPage(),
            new UncategorizedPage()
        };

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
                splitContainer1.Panel2.Controls.Clear();

                Control control = tweakPage.CustomView == null ? new PropertyGrid() { SelectedObject = tweakPage } : (Control)tweakPage.CustomView;

                splitContainer1.Panel2.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }
    }
}