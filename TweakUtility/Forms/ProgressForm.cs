using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm() => this.InitializeComponent();

        public void SetProgress(int value, int maximum, string status)
        {
            this.progressBar.Maximum = maximum;
            this.progressBar.Value = value;

            if (!string.IsNullOrWhiteSpace(status))
            {
                this.progressLabel.Text = status;
            }

            this.Refresh();
            Application.DoEvents();
        }
    }
}