using System;
using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public class ColorButton : Button
    {
        private const int PreviewSize = 16;
        private const int LeftPadding = 4;

        public override ContentAlignment TextAlign => ContentAlignment.MiddleRight;

        public override bool AutoSize => true;

        public ColorButton()
        {
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

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size size = base.GetPreferredSize(proposedSize);
            size.Width += PreviewSize + LeftPadding;
            return size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaintBackground(e);

                var rectangle = new Rectangle(
                e.ClipRectangle.X + PreviewSize + LeftPadding,
                e.ClipRectangle.Y,
                e.ClipRectangle.Width - PreviewSize - LeftPadding,
                e.ClipRectangle.Height);

                base.OnPaint(new PaintEventArgs(e.Graphics, rectangle));

                var previewRectangle = new Rectangle(
                    e.ClipRectangle.X + LeftPadding,
                    (e.ClipRectangle.Height / 2) - (PreviewSize / 2) + e.ClipRectangle.Y,
                    PreviewSize,
                    PreviewSize);

                e.Graphics.FillRectangle(new SolidBrush(Color), previewRectangle);
                e.Graphics.DrawRectangle(SystemPens.ControlText, previewRectangle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}