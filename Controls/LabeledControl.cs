using System;
using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public partial class LabeledControl : Control
    {
        private SolidBrush textBrush;

        private static readonly StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        private int stringWidth = 0;

        private Control _child;

        public Control Child
        {
            get => _child;
            set
            {
                if (_child != null && !_child.IsDisposed)
                {
                    this.Controls.Remove(_child);
                    _child.Dispose();
                }

                _child = value;

                this.Controls.Add(_child);
            }
        }

        public LabeledControl()
        {
            InitializeComponent();
            textBrush = new SolidBrush(this.ForeColor);

            //making sure the control is displayed properly
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Width = this.Parent.Width;
            Child.Width = this.Width - stringWidth - 10;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            var size = Child.GetPreferredSize(proposedSize);
            var modifiedSize = new Size(this.Parent.Width - stringWidth, size.Height);

            return modifiedSize;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Child == null)
            {
                return;
            }

            using (Graphics graphics = this.CreateGraphics())
            {
                this.Child.Left = stringWidth = (int)graphics.MeasureString(this.Text, this.Font).Width + 10;
            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            OnTextChanged(EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var point = GetStringPoint(e.ClipRectangle.Location);
            var size = GetStringSize(e.ClipRectangle.Size);
            var area = new Rectangle(point.X, point.Y, size.Width + 50, this.Height);
            e.Graphics.DrawString(Text, this.Font, textBrush, area, stringFormat);
        }

        private Point GetStringPoint(Point point) => new Point(point.X + this.Padding.Left, point.Y + this.Padding.Top);

        private Size GetStringSize(Size size) => new Size(size.Width - this.Padding.Right, size.Height - this.Padding.Bottom);

        private Rectangle GetStringArea(Rectangle rectangle) => new Rectangle(GetStringPoint(rectangle.Location), GetStringSize(rectangle.Size));
    }
}