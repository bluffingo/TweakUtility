﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using TweakUtility.Extensions;
using TweakUtility.Theming;

namespace TweakUtility.Forms
{
    internal partial class ExtensionsForm : Form
    {
        internal ExtensionsForm()
        {
            this.InitializeComponent();

            this.openFolderButton.Text = Properties.Strings.Extensions_OpenFolder;
            this.nameColumnHeader.Text = Properties.Strings.Extensions_Name;
            this.descriptionColumnHeader.Text = Properties.Strings.Extensions_Description;
            this.authorColumnHeader.Text = Properties.Strings.Extensions_Author;
        }

        private void ExtensionsForm_Load(object sender, EventArgs e)
        {
            foreach (Extension extension in Program.Loader.Extensions)
            {
                var item = new ListViewItem(extension.Name);

                item.SubItems.AddRange(new[] { extension.Description, extension.Author });

                listView.Items.Add(item);
            }

            Theme.Apply(this);
        }

        private void OkButton_Click(object sender, EventArgs e) => this.Close();

        private void OpenFolderButton_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("explorer.exe", Path.GetFullPath("extensions")) { UseShellExecute = true });
    }
}