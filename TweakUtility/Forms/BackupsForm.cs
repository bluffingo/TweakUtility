using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    internal partial class BackupsForm : Form
    {
        internal BackupsForm()
        {
            InitializeComponent();
        }

        private void BackupsForm_Load(object sender, EventArgs e)
        {
        }

        private void OkButton_Click(object sender, EventArgs e) => this.Close();

        private void OpenFolderButton_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("explorer.exe", Path.GetFullPath("backups")) { UseShellExecute = true });

        private void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}