using System;
using System.Drawing;
using System.Windows.Forms;
using TweakUtility.Attributes;

namespace TweakUtility.Controls
{
    public class CommandControl : Control
    {
        private const int Spacing = 4;
        private int state = 0;

        public CommandControl()
        {
            this.Cursor = Cursors.Hand;
            this.MouseLeave += (s, e) => SetState(0);
            this.MouseHover += (s, e) => SetState(1);
            this.MouseUp += (s, e) => SetState(1);
            this.MouseDown += (s, e) => SetState(2);
        }

        private void SetState(int state)
        {
            this.state = state;
            this.Refresh();
        }

        public override Size MinimumSize
        {
            get
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    Image icon = GetIcon(0);
                    var size = graphics.MeasureString(this.Text, this.Font).ToSize();

                    size.Width += Spacing;
                    size.Width += icon.Width;

                    size.Height = Math.Max(size.Height, icon.Height);

                    return size;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int frame = this.Enabled ? state : 3;

            Image icon = GetIcon(frame);
            e.Graphics.DrawImageUnscaled(icon, Padding.Left, Padding.Top + (this.Height / 2) - (icon.Height / 2));

            Font font = this.Font;

            if (state != 0 && !OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                font = new Font(this.Font, FontStyle.Underline);
            }

            var rect = new RectangleF(
                Padding.Left + icon.Width + Spacing,
                Padding.Top,
                this.Width - Padding.Right,
                this.Height - Padding.Bottom);

            e.Graphics.DrawString(this.Text, font, SystemBrushes.ControlText, rect, Program.stringFormat);
            base.OnPaint(e);
        }

        private Image GetIcon(int frame)
        {
            var icon = (Bitmap)GetIcon();

            //If there's only one frame, return it, ignoring the user input.
            if (icon.Width == icon.Height)
            {
                return icon;
            }

            var rect = new Rectangle(0, frame * 20, 20, 20);
            return icon.Clone(rect, icon.PixelFormat);
        }

        private Image GetIcon()
        {
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows10Beta10074))
            {
                return Properties.Resources.go10;
            }
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                return Properties.Resources.go7;
            }
            return Properties.Resources.goXP;
        }
    }
}