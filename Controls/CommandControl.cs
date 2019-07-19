using System;
using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public class CommandControl : Control
    {
        private const int Spacing = 4;

        public CommandControl()
        {
            this.Cursor = Cursors.Hand;
        }

        public override Size MinimumSize
        {
            get
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    var size = graphics.MeasureString(this.Text, this.Font).ToSize();

                    size.Width += Spacing;
                    size.Width += Properties.Resources.go.Width;

                    size.Height = Math.Max(size.Height, Properties.Resources.go.Height);

                    return size;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(Properties.Resources.go, Padding.Left, Padding.Top + (this.Height / 2) - (Properties.Resources.go.Height / 2));
            e.Graphics.DrawString(this.Text, this.Font, SystemBrushes.ControlText, Padding.Left + Properties.Resources.go.Width + Spacing, Padding.Top);
            base.OnPaint(e);
        }
    }
}