using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public class ColorButton : Button
    {
        private const int PreviewSize = 8;
        private const int TextPadding = 4;

        public Color Color { get; set; } = Color.Red;

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var rectangle = new Rectangle(
                e.ClipRectangle.X + PreviewSize + TextPadding,
                e.ClipRectangle.Y,
                e.ClipRectangle.Width - PreviewSize - TextPadding,
                e.ClipRectangle.Height);

                base.OnPaint(new PaintEventArgs(e.Graphics, rectangle));

                e.Graphics.FillRectangle(new SolidBrush(Color), e.ClipRectangle.X, e.ClipRectangle.Y, PreviewSize, PreviewSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}