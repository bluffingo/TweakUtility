using System;
using System.Drawing;
using System.Windows.Forms;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.Controls
{
    public class CommandControl : Control
    {
        private const int Spacing = 4;
        private int currentState = 0;
        private static Bitmap bitmap;
        private static Font underlinedFont;

        public CommandControl()
        {
            this.Cursor = Cursors.Hand;
            this.MouseLeave += (s, e) => this.SetState(0);
            this.MouseHover += (s, e) => this.SetState(1);
            this.MouseUp += (s, e) => this.SetState(1);
            this.MouseDown += (s, e) => this.SetState(2);

            //bitmap = NativeHelpers.ExtractImage(@"%SystemRoot%\Resources\Themes\aero\aero.msstyles", 516);
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows10))
            {
                bitmap = Properties.Resources.go10;
            }
            else if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
            {
                bitmap = Properties.Resources.go7;
            }
            else
            {
                bitmap = Icons.Go.ToBitmap();
            }

            if (underlinedFont == null && !OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
            {
                underlinedFont = new Font(this.Font, FontStyle.Underline);
            }
        }

        private void SetState(int state)
        {
            currentState = state;
            this.Refresh();
        }

        public override Size MinimumSize
        {
            get
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    Image icon = this.GetIcon(0);
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
            base.OnPaint(e);

            int frame = this.Enabled ? currentState : 3;

            Image icon = this.GetIcon(frame);
            e.Graphics.DrawImageUnscaled(icon, this.Padding.Left, this.Padding.Top + (this.Height / 2) - (icon.Height / 2));

            Font font = this.Font;

            if (currentState != 0 && !OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
            {
                font = underlinedFont;
            }

            var rect = new RectangleF(
                this.Padding.Left + icon.Width + Spacing,
                this.Padding.Top + 4,
                this.Width - this.Padding.Right,
                this.Height - this.Padding.Bottom - 4);

            e.Graphics.DrawString(this.Text, font, SystemBrushes.ControlText, rect, Constants.NearCenterStringFormat);
        }

        private Image GetIcon(int frame)
        {
            Bitmap icon = bitmap;

            //If there's only one frame, return it, ignoring the user input.
            if (icon.Width == icon.Height)
            {
                return icon;
            }

            var rect = new Rectangle(0, frame * 20, 20, 20);
            return icon.Clone(rect, icon.PixelFormat);
        }
    }
}