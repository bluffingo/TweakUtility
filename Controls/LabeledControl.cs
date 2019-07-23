using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility.Controls
{
    public partial class LabeledControl : Control
    {
        /// <summary>
        /// Stores information about which is the largest spacing on each parent to keep layout consistency over a parent.
        /// </summary>
        private static readonly Dictionary<int, int> spacings = new Dictionary<int, int>();

        private const int spacing = 4;

        private static readonly StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        private Control _child;
        private SolidBrush textBrush;

        public LabeledControl()
        {
            InitializeComponent();
            textBrush = new SolidBrush(this.ForeColor);
        }

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

        public override Size MinimumSize => new Size(Width, Child.GetPreferredSize(Size.Empty).Height);

        /// <summary>
        /// Retrieves the width of the control, generally the width of the parent while respecting margin and padding properties.
        /// </summary>
        public new int Width => this.Parent.Width - this.Parent.Margin.Horizontal - this.Parent.Padding.Horizontal;

        protected override void InitLayout()
        {
            base.InitLayout();
            OnTextChanged(EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var point = new Point(e.ClipRectangle.X + this.Padding.Left, e.ClipRectangle.Y + this.Padding.Top);
            var size = new Size(e.ClipRectangle.Width - this.Padding.Right, e.ClipRectangle.Height - this.Padding.Bottom);

            var area = new Rectangle(point.X, point.Y, size.Width + 50, this.Height);
            e.Graphics.DrawString(Text, this.Font, textBrush, area, stringFormat);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            OnTextChanged(EventArgs.Empty);
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
                int getSize() => (int)graphics.MeasureString(this.Text, this.Font).Width + spacing;

                int parent = this.Parent.GetHashCode();

                if (!spacings.ContainsKey(parent) || spacings[parent] < getSize())
                {
                    spacings[parent] = getSize();
                }

                this.Child.Left = spacings[parent];
                this.Child.Width = Width - Child.Left - Padding.Right;
            }
        }
    }
}