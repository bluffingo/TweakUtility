using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility.Theming
{
    public struct Theme
    {
        //github desktop succ
        public static Theme Dark = new Theme()
        {
            ApplicationBackground = Color.FromArgb(21, 21, 21),
            ButtonBackground = Color.FromArgb(42, 42, 42),
            LinkForeground = Color.LightBlue,
            TextForeground = Color.White,
            SidebarBackground = Color.FromArgb(42, 42, 42),
        };

        public static Theme Windows10Dark = new Theme()
        {
            ApplicationBackground = Color.Black,
            ButtonBackground = Color.FromArgb(51, 51, 51),
            LinkForeground = Color.FromArgb(0, 120, 215),
            TextForeground = Color.White,
            SidebarBackground = Color.FromArgb(31, 31, 31)
        };

        public static Theme Windows10Light = new Theme()
        {
            ApplicationBackground = Color.White,
            ButtonBackground = Color.FromArgb(204, 204, 204),
            LinkForeground = Color.FromArgb(0, 120, 215),
            TextForeground = Color.Black
        };

        public static Theme Windows6Light = new Theme()
        {
            ApplicationBackground = Color.White,
            ButtonBackground = Color.FromArgb(204, 204, 204),
            LinkForeground = Color.FromArgb(0, 120, 215),
            TextForeground = Color.Black
        };

        public static Theme ZuneSlate = new Theme()
        {
            ApplicationBackground = Color.FromArgb(38, 22, 0),
            ButtonBackground = Color.FromArgb(77, 62, 42),
            LinkForeground = Color.FromArgb(171, 118, 32),
            TextForeground = Color.White
        };

        public static Theme Luna = new Theme()
        {
            ApplicationBackground = Color.FromArgb(38, 22, 0),
            ButtonBackground = Color.FromArgb(77, 62, 42),
            LinkForeground = Color.FromArgb(171, 118, 32),
            TextForeground = Color.Black
            //TODO: find the actual Luna colors
        };

        public static Theme Plex = new Theme()
        {
            ApplicationBackground = Color.FromArgb(216, 230, 245),
            ButtonBackground = Color.FromArgb(191, 211, 255),
            LinkForeground = Color.FromArgb(131, 182, 234),
            TextForeground = Color.Black
        };

        public static Theme System = new Theme()
        {
            ApplicationBackground = SystemColors.Control,
            ButtonBackground = Color.Transparent,
            LinkForeground = Color.FromArgb(0, 102, 204),
            TextForeground = Color.Black
        };

        public Color ApplicationBackground;
        public Color ButtonBackground;
        public Color TextForeground;
        public Color LinkForeground;
        public Color SidebarBackground;

        public void Apply(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (control is TreeView treeView)
            {
                control.BackColor = SidebarBackground;
            }
            else if (control is Button)
            {
            }
            else
            {
                control.BackColor = ApplicationBackground;
            }

            if (control is LinkLabel linkLabel)
            {
                linkLabel.LinkColor = LinkForeground;
            }
            else if (control is Label label && label.ForeColor == SystemColors.ControlText)
            {
                control.ForeColor = TextForeground;
            }

            if (control is Button button && ButtonBackground != Color.Transparent)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = ButtonBackground;
                button.FlatAppearance.BorderColor = ButtonBackground;
            }

            foreach (Control subControl in control.Controls)
            {
                Apply(subControl);
            }
        }
    }
}