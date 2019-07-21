using System;
using System.Reflection;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    partial class AboutForm : Form
    {
        public AboutForm() => this.InitializeComponent();

        private void FeedbackButton_Click(object sender, EventArgs e) => Program.OpenURL("https://github.com/Craftplacer/TweakUtility/issues/new/choose");

        private void AboutForm_Load(object sender, EventArgs e)
        {
            versionLabel.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version}";
#if DEBUG
            debugLabel.Visible = true;
#endif
        }

        private void GithubLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Program.OpenURL("https://github.com/Craftplacer/TweakUtility");
    }
}