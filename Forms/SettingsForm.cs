using System;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm() => this.InitializeComponent();

        private void SettingsForm_Load(object sender, EventArgs e) => this.Controls.Add(new TweakPageView(Program.Config)
        {
            Dock = DockStyle.Fill
        });

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e) => Program.SaveConfig();
    }
}