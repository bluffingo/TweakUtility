using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TweakUtility.Enums;
using TweakUtility.Forms;
using TweakUtility.Helpers;

using static TweakUtility.Helpers.OperatingSystemVersions;

namespace TweakUtility.Theming
{
	internal static class Theme
	{
		/// <summary>
		/// Checks if the current system theme is dark.
		/// </summary>
		public static bool IsDark => SystemColors.Window.GetReadableColor();

		public static Color ApplicationBackground
		{
			get
			{
				if (IsSupportedCosmetic(OperatingSystemVersion.WindowsVista))
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
				if (IsSupportedCosmetic(OperatingSystemVersion.WindowsVista))
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
				if (IsDark)
				{
					return Color.White;
				}

				// Fluent Theme
				if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
				{
					return Color.Black;
				}

				if (IsSupportedCosmetic(OperatingSystemVersion.WindowsVista))
				{
					return Color.FromArgb(0, 51, 153);
				}

				return Color.Black;
			}
		}

		public static Color CategoryForeground => TitleForeground;

		public static FontFamily TitleFontFamily
		{
			get
			{
				// Fluent Theme
				if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
				{
					return FontFamily.Families.First(f => f.Name == "Segoe UI");
				}

				// Aero Theme
				if (IsSupportedCosmetic(OperatingSystemVersion.WindowsVista))
				{
					return FontFamily.Families.First(f => f.Name == "Segoe UI");
				}

				// Luna Theme
				return FontFamily.Families.First(f => f.Name == "Franklin Gothic Medium");
			}
		}

		public static Font TitleFont
		{
			get
			{
				if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
				{
					return new Font(TitleFontFamily, 24, GraphicsUnit.Pixel);
				}

				return new Font(TitleFontFamily, 14, GraphicsUnit.Point);
			}
		}

		public static Font CategoryFont
		{
			get
			{
				if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
				{
					return new Font(CategoryFontFamily, 20, GraphicsUnit.Pixel);
				}

				return new Font(CategoryFontFamily, 11, GraphicsUnit.Point);
			}
		}

		public static FontFamily CategoryFontFamily
		{
			get
			{
				// Fluent Theme
				if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
				{
					return FontFamily.Families.First(f => f.Name == "Segoe UI");
				}

				if (IsSupportedCosmetic(OperatingSystemVersion.WindowsVista))
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

		public static void Apply(Control control)
		{
			if (control == null)
				throw new ArgumentNullException(nameof(control));

			if (control is Form form)
				form.BackColor = ApplicationBackground;

			if (control is MainForm mainForm)
			{
				mainForm.bottomPanel.BackColor = BottomPanelBackground;
				mainForm.treeView.BackColor = SidebarBackground;
			}

			if (control is LinkLabel linkLabel)
				linkLabel.LinkColor = LinkForeground;

			if (IsSupportedCosmetic(OperatingSystemVersion.Windows10))
			{
				if (control is Button button)
				{
					button.FlatStyle = FlatStyle.Flat;
					button.FlatAppearance.BorderSize = 0;

					if (IsDark)
					{
						button.BackColor = Color.FromArgb(51, 51, 51);
					}
					else
					{
						button.BackColor = Color.FromArgb(204, 204, 204);
					}
				}
			}

			// Apply to sub-controls as well.
			foreach (Control subControl in control.Controls)
				Apply(subControl);
		}
	}
}