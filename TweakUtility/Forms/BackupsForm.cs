using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using TweakUtility.Theming;

namespace TweakUtility.Forms
{
    internal partial class BackupsForm : Form
    {
        internal BackupsForm()
        {
            this.InitializeComponent();

            this.applyButton.Text = Properties.Strings.Button_Apply;
            this.cancelButton.Text = Properties.Strings.Button_Cancel;
            this.createButton.Text = Properties.Strings.Button_Create;
            this.deleteButton.Text = Properties.Strings.Button_Delete;
            this.openFolderButton.Text = Properties.Strings.Backups_OpenFolder;
            this.nameColumnHeader.Text = Properties.Strings.Backups_Name;
            this.dateColumnHeader.Text = Properties.Strings.Backups_Date;
            this.sizeColumnHeader.Text = Properties.Strings.Backups_Size;
        }

        private void BackupsForm_Load(object sender, EventArgs e)
        {
            listView.Items.Clear();

            foreach (Backup backup in Program.Backups)
            {
                var item = new ListViewItem(backup.Name)
                {
                    Tag = backup
                };

                item.SubItems.Add(backup.Date.ToShortDateString());
                item.SubItems.Add($"{Math.Round(value: backup.Size / 1000, 2, MidpointRounding.ToEven)} KB");

                listView.Items.Add(item);
            }

            Theme.Apply(this);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            var backup = listView.SelectedItems[0].Tag as Backup;

            this.Enabled = false;

            backup.Apply();

            this.Close();
        }

        private void OpenFolderButton_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("explorer.exe", Path.GetFullPath("backups")) { UseShellExecute = true });

        private void CreateButton_Click(object sender, EventArgs e)
        {
            using (var form = new BackupCreateForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Program.LoadBackups();
                    this.BackupsForm_Load(this, EventArgs.Empty);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var backup = listView.SelectedItems[0].Tag as Backup;

            this.Enabled = false;

            backup.Delete();
            this.BackupsForm_Load(this, EventArgs.Empty);

            this.Enabled = true;
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => applyButton.Enabled = deleteButton.Enabled = listView.SelectedItems.Count != 0;
    }
}