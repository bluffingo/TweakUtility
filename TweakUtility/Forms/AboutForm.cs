using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    internal partial class AboutForm : Form
    {
        private readonly List<Keys> input = new List<Keys>();

        internal AboutForm()
        {
            this.InitializeComponent();
            this.Localize();
        }

        public void Localize()
        {
            this.Text = Properties.Strings.About;
            this.githubLabel.Text = Properties.Strings.About_License.Replace("{0}", "GitHub");
            this.githubLabel.LinkArea = new LinkArea(Properties.Strings.About_License.IndexOf("{0}"), 6);
            this.descriptionLabel.Text = Properties.Strings.About_Description;
            this.copyrightLabel.Text = Properties.Strings.About_Copyright;
            this.feedbackButton.Text = Properties.Strings.Button_Feedback;
            this.creditsButton.Text = Properties.Strings.Button_Credits;
        }

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
        {   //BELOW IS THE KONAMI CODE EASTER EGG CODE
            input.Add(e.KeyCode);

            if (input.ToArray() == new[] { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A })
            {
                if (MessageBox.Show("Wanna crash?", Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    throw new Exception("User triggered exception");
                }
            }
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            using (var credits = new CreditsForm())
            {
                credits.ShowDialog(this);
            }
        }
    }
}