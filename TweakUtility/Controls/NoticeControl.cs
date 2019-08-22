using System;
using System.Drawing;
using System.Windows.Forms;
using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;
using TweakUtility.Theming;

namespace TweakUtility.Controls
{
    public class NoticeControl : Control
    {
        private readonly Color outlineColor;
        private readonly Color fillColor;
        private readonly Icon icon;

        private new string Text { get; }

        public NoticeControl(NoticeAttribute attribute) : this(GetFillColor(attribute.Type), GetOutlineColor(attribute.Type), GetIcon(attribute.Type), attribute.Text)
        {
        }

        private NoticeControl(Color fill, Color outline, Icon icon, string text)
        {
            this.fillColor = fill;
            this.outlineColor = outline;
            this.icon = icon;
            this.Text = text;

            this.Paint += this.NoticeControl_Paint;
        }

        public override Size MinimumSize => new Size(this.Width, 24);

        /// <summary>
        /// Retrieves the width of the control, generally the width of the parent while respecting margin and padding properties.
        /// </summary>
        public new int Width => this.Parent.Width - this.Parent.Padding.Left - this.Parent.Padding.Right;

        private void NoticeControl_Paint(object sender, PaintEventArgs e)
        {
            var padding = new Padding(4);

            var rectangle = new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - this.Padding.Right - 1, this.Height - this.Padding.Bottom - 1);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (Theme.IsDark)
            {
                e.Graphics.DrawRoundedRectangle(new Pen(this.fillColor), rectangle, 4);
            }
            else
            {
                e.Graphics.FillRoundedRectangle(new SolidBrush(this.fillColor), rectangle, 4);
                e.Graphics.DrawRoundedRectangle(new Pen(this.outlineColor), rectangle, 4);
            }

            const int iconSize = 16;
            var iconRectangle = new Rectangle(padding.Left, (this.Height / 2) - (iconSize / 2), iconSize, iconSize);
            e.Graphics.DrawIcon(icon, iconRectangle);

            const int iconSpacing = 4;

            Brush textBrush;

            if (SystemColors.Window.GetReadableColor())
                textBrush = SystemBrushes.WindowText;
            else
                textBrush = fillColor.GetReadableColor() ? Brushes.White : Brushes.Black;

            var textLeft = padding.Left + iconSize + iconSpacing;
            var textWidth = this.Width - textLeft - Padding.Right;
            var textHeight = this.Height - padding.Top - padding.Bottom;
            var textRectangle = new Rectangle(textLeft, padding.Top, textWidth, textHeight);
            e.Graphics.DrawString(this.Text, this.Font, textBrush, textRectangle);
        }

        private static Color GetFillColor(NoticeType type)
        {
            switch (type)
            {
                case NoticeType.Info:
                    return Color.FromArgb(140, 170, 230);

                case NoticeType.Tip:
                    return Color.FromArgb(255, 255, 155);

                case NoticeType.Warning:
                    return Color.FromArgb(255, 204, 0);

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private static Color GetOutlineColor(NoticeType type)
        {
            switch (type)
            {
                case NoticeType.Info:
                    return Color.FromArgb(100, 135, 220);

                case NoticeType.Tip:
                    return Color.FromArgb(255, 204, 0);

                case NoticeType.Warning:
                    return Color.FromArgb(255, 153, 51);

                default:
                    throw new ArgumentException(nameof(type));
            }
        }

        private static Icon GetIcon(NoticeType type)
        {
            switch (type)
            {
                case NoticeType.Info:
                    return Icons.Information;

                case NoticeType.Tip:
                    return Icons.Bulb;

                case NoticeType.Warning:
                    return Icons.Warning;

                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }
}