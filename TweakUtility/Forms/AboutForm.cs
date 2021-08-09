﻿using System;
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
            this.titleLabel.Text = Properties.Strings.Application_Name;
            this.Text = Properties.Strings.About;
            this.githubLabel.Text = Properties.Strings.About_License.Replace("{0}", "GitHub");
            this.githubLabel.LinkArea = new LinkArea(Properties.Strings.About_License.IndexOf("{0}"), 6);
            this.descriptionLabel.Text = Properties.Strings.About_Description;
            this.copyrightLabel.Text = Properties.Strings.About_Copyright;
            this.feedbackButton.Text = Properties.Strings.Button_Feedback;
            this.creditsButton.Text = Properties.Strings.Button_Credits;
            this.versionLabel.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString(3)}";
#if DEBUG
            this.debugLabel.Visible = true;
            this.debugLabel.Text = Properties.Strings.Debug;
#endif
        }

        private void FeedbackButton_Click(object sender, EventArgs e) => Program.OpenURL("https://github.com/PF94/TweakUtility_PF94/issues/new/choose");

        private void GithubLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Program.OpenURL("https://github.com/PF94/TweakUtility_PF94");

        private void AboutForm_KeyUp(object sender, KeyEventArgs e)
        {
            input.Add(e.KeyCode);

            if (input.ToArray() == new[] { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A })
            {
                if (MessageBox.Show("Wanna crash?", Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    throw new Exception("User triggered exception");
                }
            }
            else if (input.Count >= 10)
            {
                //resetting
                input.Clear();
            }
        }

        private void CreditsButton_Click(object sender, EventArgs e)
        {
            using (var credits = new CreditsForm())
            {
                credits.ShowDialog(this);
            }
        }

        private void DebugSivityigans_Click(object sender, EventArgs e)
        {
            debugLabel.Text = Properties.Strings.SivityigansPossible_EasterEgg;
        }

        private void CanberraEasterEgg_Click(object sender, EventArgs e) //<-- probably should make it only happen on may 14th.
        {
            versionLabel.Text = Properties.Strings.Canberra_EasterEgg_1;
            iconPictureBox.Image = Properties.Resources.nuhuh;
            titleLabel.Text = Properties.Strings.Canberra_EasterEgg_2;
        }
    }
}