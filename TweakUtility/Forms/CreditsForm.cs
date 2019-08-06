using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class CreditsForm : Form
    {
        private float creditsEnd = -1;
        private float position = 0;

        private static StringFormat format = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };

        public CreditsForm()
        {
            this.InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            this.timer.Interval = 1000 / 60;
        }

        private void CreditsForm_Load(object sender, EventArgs e)
        {
        }

        private void CreditsForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            position += 0.5f;

            if (creditsEnd == -1)
            {
                creditsEnd = e.Graphics.MeasureString(Properties.Strings.Credits, this.Font, this.Width).Height;
            }

            if (creditsEnd < position)
            {
                timer.Stop();
            }

            var rect = new RectangleF(0, this.Height - position, this.Width, creditsEnd);
            e.Graphics.DrawString(Properties.Strings.Credits, this.Font, SystemBrushes.ControlText, rect, format);
        }

        private void Timer_Tick(object sender, EventArgs e) => this.Refresh();
    }
}