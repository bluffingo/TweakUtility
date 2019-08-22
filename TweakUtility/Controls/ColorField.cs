using System;
using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public class ColorField : Control
    {
        private const int PreviewSize = 16;
        private const int TextSpacing = PreviewSize / 4;

        public ColorField()
        {
            this.Padding = new Padding(4, 0, 0, 0);
            this.Cursor = Cursors.Hand;
            this.AutoSize = true;

            this.Click += (s, e) =>
            {
                using (var dialog = new ColorDialog()
                {
                    Color = this.Color
                })
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        this.Color = dialog.Color;
                        ColorChanged?.Invoke(this, EventArgs.Empty);
                        this.Refresh();
                    }
                }
            };
        }

        public EventHandler ColorChanged;

        public Color Color { get; set; } = Color.Red;

        public override Size MinimumSize
        {
            get
            {
                using (Graphics graphics = this.CreateGraphics())
                {
                    SizeF requiredSize = graphics.MeasureString(this.Text, this.Font);

                    var modifiedSize = new Size(
                        (int)requiredSize.Width + PreviewSize + this.Padding.Left + this.Padding.Right + TextSpacing,
                        Math.Max((int)requiredSize.Height, PreviewSize) + this.Padding.Top + this.Padding.Bottom
                        );

                    return modifiedSize;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            this.OnPaintPreview(e);
            this.OnPaintText(e);
        }

        public void OnPaintPreview(PaintEventArgs e)
        {
            var previewRectangle = new Rectangle(
                this.Padding.Left,
                this.Padding.Top + (this.Height / 2) - (PreviewSize / 2),
                PreviewSize,
                PreviewSize);

            e.Graphics.FillRectangle(new SolidBrush(this.Color), previewRectangle);
            e.Graphics.DrawRectangle(SystemPens.ControlText, previewRectangle);
        }

        public void OnPaintText(PaintEventArgs e)
        {
            var textRectangle = new Rectangle(
                this.Padding.Left + PreviewSize + TextSpacing,
                this.Padding.Top,
                this.Width - this.Padding.Left - PreviewSize - TextSpacing - this.Padding.Right + 5, //adding 5 to prevent the renderer to think that the space is too small
                this.Height - this.Padding.Top - this.Padding.Bottom);

            //HACK: Optimization can be done to reduce initializations of SolidBrush.
            //      (only create new instance of SolidBrush on color change)
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRectangle, Constants.NearCenterStringFormat);
        }
    }
}