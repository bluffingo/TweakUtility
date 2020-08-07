using System;
using System.Windows.Forms;

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

        private void GarfieldPF94_Click(object sender, EventArgs e)
        {
            {
                DialogResult d; //causes IDE0059, but trying to fix it causes a error.
                d = MessageBox.Show("Broken Heart, and Graphic High. Ahh the wine. Come home, Jon.", "Proud 'n 94th Parappa Fan", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (d == DialogResult.No)
                {
                    throw new Exception("I'm not sorry, Jon. You don't want to come back home. I don't want to be a Raymond.");
                }
                if (d == DialogResult.Yes)
                {
                    MessageBox.Show("Good, Thank you.", "Garfield", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}