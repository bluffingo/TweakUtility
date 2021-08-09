using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TweakUtility.Helpers;

namespace TweakUtility.Forms
{
    public partial class BackupCreateForm : Form
    {
        public BackupCreateForm()
        {
            this.InitializeComponent();
            this.instructionLabel.Text = Properties.Strings.Backups_Create_Instruction;
            this.validLabel.Text = Properties.Strings.Backups_Create_Requirement;
        }

        private void BackupCreateForm_Load(object sender, EventArgs e)
        {
            foreach (TweakPage page in Program.Pages)
            {
                this.Add(page);
            }
        }

        public string BackupName => string.IsNullOrWhiteSpace(textBox1.Text) ? Path.GetRandomFileName() : textBox1.Text;

        public bool Valid => treeView.Nodes.Cast<TreeNode>().Any(t => t.Checked);

        private void Add(TweakPage page, TreeNode parent = null)
        {
            var treeNode = new TreeNode(page.Name)
            {
                Tag = page
            };

            //add subpages
            foreach (TweakPage subPage in page.SubPages)
            {
                this.Add(subPage, treeNode);
            }

            //add options
            foreach (TweakEntry entry in page.Entries)
            {
                if (entry is TweakOption option)
                {
                    if (!option.Visible)
                    {
                        continue;
                    }

                    treeNode.Nodes.Add(new TreeNode(option.Name)
                    {
                        Tag = option
                    });
                }
            }

            //add to treeview if no parent is specified
            if (parent == null)
            {
                this.treeView.Nodes.Add(treeNode);
            }
            else
            {
                parent.Nodes.Add(treeNode);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) => this.Close();

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            var options = treeView.Nodes.Cast<TreeNode>().Flatten(n => n.Nodes.Cast<TreeNode>()).Where(t => t.Checked && t.Tag is TweakOption).Select(t => t.Tag as TweakOption);
            var backup = new Backup(options.ToArray());
            string path = Path.Combine(Program.ApplicationDirectory, "backups", this.BackupName + ".tubk");

            backup.Export(path);

            this.Close();
        }

        private void TreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes != null)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }

            validLabel.Visible = !this.Valid;
            okButton.Enabled = this.Valid;
        }
    }
}