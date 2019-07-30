using System;
using System.Reflection;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    internal partial class AboutForm : Form
    {
        private string input = "";

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

        private void AboutForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) input += 'u';
            else if (e.KeyCode == Keys.Down) input += 'd';
            else if (e.KeyCode == Keys.Left) input += 'l';
            else if (e.KeyCode == Keys.Right) input += 'r';
            else if (e.KeyCode == Keys.B) input += 'b';
            else if (e.KeyCode == Keys.A) input += 'a';

            if (input == "uuddlrlrba")
            {
                if (MessageBox.Show("Wanna crash?", Properties.Resources.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    throw new Exception("User triggered exception");
                }
            }
        }
    }
}