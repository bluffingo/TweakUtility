using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class ProgressForm : Form
    {
        public ProgressForm() => this.InitializeComponent();

        public void SetProgress(int value, int maximum, string status)
        {
			if (value < 0)
			{
				this.progressBar.Maximum = this.progressBar.Value = 0;
				this.progressBar.Style = ProgressBarStyle.Marquee;
			}
			else
			{
				this.progressBar.Maximum = maximum;
				this.progressBar.Value = value;
				this.progressBar.Style = ProgressBarStyle.Continuous;
			}

            if (!string.IsNullOrWhiteSpace(status))
                this.progressLabel.Text = status;

            this.Refresh();
            Application.DoEvents();
        }
    }
}