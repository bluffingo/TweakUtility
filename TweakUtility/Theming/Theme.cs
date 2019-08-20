using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TweakUtility.Attributes;
using TweakUtility.Forms;
using static TweakUtility.Helpers.OperatingSystemVersions;

namespace TweakUtility.Theming
{
    internal static class Theme
    {
        public static Color ApplicationBackground
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return SystemColors.Window; //basically white on aero, no plans for aero transparency (like CustomizerGod) at the moment.
                }
                return SystemColors.Control;
            }
        }

        public static Color BottomPanelBackground => SystemColors.Control;

        public static Color LinkForeground
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return Color.FromArgb(0, 102, 204);
                }

                return Color.Black;
            }
        }

        public static Color SidebarBackground;

        public static Color TitleForeground
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return Color.FromArgb(0, 51, 153);
                }
                return Color.Black;
            }
        }

        public static Color CategoryForeground
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return Color.FromArgb(0, 51, 153);
                }
                return Color.Black;
            }
        }

        public static FontFamily TitleFontFamily
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return FontFamily.Families.First(f => f.Name == "Segoe UI");
                }
                return FontFamily.Families.First(f => f.Name == "Franklin Gothic Medium");
            }
        }

        public static Font TitleFont => new Font(TitleFontFamily, TitleSize, GraphicsUnit.Point);

        public static Font CategoryFont => new Font(CategoryFontFamily, CategorySize, GraphicsUnit.Point);

        public static FontFamily CategoryFontFamily
        {
            get
            {
                if (IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    return FontFamily.Families.First(f => f.Name == "Segoe UI");
                }

                ///"Tahoma is used as the system's default font.
                ///Tahoma should be used at 8, 9 or 11 point sizes."
                ///
                /// - Windows XP Visual Guidelines (http://interface.free.fr/Archives/GUI_Xp.pdf)
                return FontFamily.Families.First(f => f.Name == "Tahoma");
            }
        }

        public static int TitleSize => 14;

        public static int CategorySize => 11;

        public static void Apply(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (control is Form form)
            {
                form.BackColor = ApplicationBackground;
            }

            if (control is MainForm mainForm)
            {
                mainForm.bottomPanel.BackColor = BottomPanelBackground;
                mainForm.treeView.BackColor = SidebarBackground;
            }

            // are we going to do #2 later? yeah why not
            if (control is LinkLabel linkLabel)
            {
                linkLabel.LinkColor = LinkForeground;
            }

            foreach (Control subControl in control.Controls)
            {
                Apply(subControl);
            }
        }
    }
}