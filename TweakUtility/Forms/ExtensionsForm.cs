using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    internal partial class ExtensionsForm : Form
    {
        internal ExtensionsForm() => this.InitializeComponent();

        private void ExtensionsForm_Load(object sender, EventArgs e)
        {
        }

        private void OkButton_Click(object sender, EventArgs e) => this.Close();

        private void OpenFolderButton_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("explorer.exe", Path.GetFullPath("extensions")) { UseShellExecute = true });

        private void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}