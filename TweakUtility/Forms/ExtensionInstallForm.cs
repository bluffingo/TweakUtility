using System;
using System.IO;
using System.Windows.Forms;

using TweakUtility.Extensions;

namespace TweakUtility.Forms
{
    public partial class ExtensionInstallForm : Form
    {
        private readonly string extensionPath;

        public ExtensionInstallForm(Extension extension, string path)
        {
            this.InitializeComponent();

            this.extensionPath = path;

            if (Properties.Settings.Default.AutoInstallExtensions)
            {
                this.InstallButton_Click(this, EventArgs.Empty);
            }

            this.nameLabel.Text = extension.Name;
            this.authorLabel.Text = extension.Author;
            this.descriptionLabel.Text = extension.Description;

            this.cancelButton.Text = Properties.Strings.Button_Cancel;
            this.dontAskCheckBox.Text = Properties.Strings.CheckBox_DontAskAgain;
            this.installButton.Text = Properties.Strings.Button_Install;
            this.label1.Text = Properties.Strings.Extension_Install_Confirmation;
            this.Text = Properties.Strings.Extension_Install;
        }

        private void CancelButton_Click(object sender, EventArgs e) => this.Close();

        private void InstallButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            string fileName = Path.GetFileName(extensionPath);
            string targetPath = Path.Combine(Program.ApplicationDirectory, "extensions", fileName);

            File.Move(extensionPath, targetPath);

            this.Hide();

            MessageBox.Show(Properties.Strings.Extension_Install_Success, Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void DontAskCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoInstallExtensions = dontAskCheckBox.Checked;
            Properties.Settings.Default.Save();
        }
    }
}