﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Extensions;
using TweakUtility.Helpers;

namespace TweakUtility.Forms
{
    internal partial class SplashForm : Form
    {
        internal SplashForm() => this.InitializeComponent();

        /// <summary>
        /// This prevents the application showing the main form if an error occurred while launching.
        /// </summary>
        private bool formInitiatedClose = false;

        private void SplashForm_Shown(object sender, EventArgs e)
        {
        }

        public new void Close()
        {
            formInitiatedClose = true;
            base.Close();
        }

        public void SetStatus(string status)
        {
            statusLabel.Text = status;
            statusLabel.Refresh();
            Application.DoEvents();
        }

        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formInitiatedClose)
            {
                Application.Exit();
            }
        }
#if DEBUG //this might confuse people who keep clicking everywhere.
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            {
                DialogResult d;
                d = MessageBox.Show("Broken Heart, and Graphic High. Ahh the wine. Come home, Jon.", "94th Parappafan94", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.No)
                {
                    throw new Exception("I'm not sorry, Jon. You don't want to come back home. I don't want to be a Raymond.");
                }
                if (d == DialogResult.Yes)
                {
                    d = MessageBox.Show("Good, Thank you.", "Garfield", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
#endif