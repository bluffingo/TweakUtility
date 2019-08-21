using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    internal class CustomizationPage : TweakPage
    {
        internal CustomizationPage() : base("Customization", new ColorsPage())
        {
            ///[4:33 AM] Craftplacer: https://files.catbox.moe/hipgjk.png
            ///[4:33 AM] Craftplacer: tech gore
            ///[4:33 AM] Craftplacer: but self made
            ///[4:34 AM] PF94: tweakutility's main gimmick is that it can customize your printer
            ///[4:34 AM] Craftplacer: lmao
            ///[4:34 AM] PF94: and put ink without buying ink
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\imageres.dll", -197);
            }
            else
            {
                this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -250);
            }
        }

        [DisplayName("Applications use light theme")]
        //[DefaultValue(false)]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)] //also works with Windows 10 RTM and november update, no need to make it 1607+ only.
        public bool AppsUseLightTheme
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme") == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme", value ? 1 : 0);
        }

        [DisplayName("System uses light theme")]
        //[DefaultValue(false)] //:shrug:
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)] //not really...
        public bool SystemUsesLightTheme
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\SystemUsesLightTheme") == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\SystemUsesLightTheme", value ? 1 : 0);
        }

        [OperatingSystemSupported(OperatingSystemVersion.Windows8)]
        [Visible(true)]
        [DisplayName("Enable Lite Theme")]
        public void EnableLiteTheme()
        {
            //extract file if it doesn't exist
            if (!File.Exists("aerolite.theme"))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://raw.githubusercontent.com/Craftplacer/TweakUtility/master/Optional/aerolite.theme", "aerolite.theme");
                }
                File.SetAttributes("aerolite.theme", File.GetAttributes("aerolite.theme") | FileAttributes.Hidden);
            }

            Process.Start(Path.GetFullPath("aerolite.theme"));
        }

        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        [DisplayName("Show seconds on taskbar")]
        public bool ShowSeconds
        {
            get => RegistryHelper.GetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSecondsInsystemClock", 0) == 1;
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSecondsInsystemClock", value ? 1 : 0);
        }

        public class ColorsPage : TweakPage
        {
            public ColorsPage() : base("Classic Theme Colors") => this.Icon = Properties.Resources.colors;

            #region Button Colors

            [Category("Button Colors")]
            [DisplayName("Button Text")]
            public Color ButtonText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonText", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Alternate Face")]
            public Color ButtonAlternateFace
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonAlternateFace");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonAlternateFace", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Dark Shadow")]
            public Color ButtonDarkShadow
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonDkShadow");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonDkShadow", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Face")]
            public Color ButtonFace
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonFace");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonFace", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Highlight")]
            public Color ButtonHighlight
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonHilight");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonHilight", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Shadow")]
            public Color ButtonShadow
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonShadow");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonShadow", value);
            }

            [Category("Button Colors")]
            [DisplayName("Button Light")]
            public Color ButtonLight
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ButtonLight");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ButtonLight", value);
            }

            #endregion Button Colors

            #region Menu Colors

            [Category("Menu Colors")]
            [DisplayName("Menu color")]
            public Color Menu
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\Menu");
                set => this.SetColor(@"HKCU\Control Panel\Colors\Menu", value);
            }

            [Category("Menu Colors")]
            [DisplayName("Menu bar color")]
            public Color MenuBar
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\MenuBar");
                set => this.SetColor(@"HKCU\Control Panel\Colors\MenuBar", value);
            }

            [Category("Menu Colors")]
            [DisplayName("Menu highlight color")]
            public Color MenuHilight
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\MenuHilight");
                set => this.SetColor(@"HKCU\Control Panel\Colors\MenuHilight", value);
            }

            [Category("Menu Colors")]
            [DisplayName("Menu text color")]
            public Color MenuText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\MenuText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\MenuText", value);
            }

            #endregion Menu Colors

            #region Title Bar Colors

            [Category("Title Bar Colors")]
            [DisplayName("Active title bar color")]
            public Color ActiveTitle
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InactiveTitle");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InactiveTitle", value);
            }

            [Category("Title Bar Colors")]
            [DisplayName("Active title bar gradient color")]
            public Color GradientActiveTitle
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\GradientActiveTitle");
                set => this.SetColor(@"HKCU\Control Panel\Colors\GradientActiveTitle", value);
            }

            [Category("Title Bar Colors")]
            [DisplayName("Active title bar text color")]
            public Color ActiveTitleText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\TitleText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\TitleText", value);
            }

            [Category("Title Bar Colors")]
            [DisplayName("Inactive title bar color")]
            public Color InactiveTitle
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InactiveTitle");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InactiveTitle", value);
            }

            [Category("Title Bar Colors")]
            [DisplayName("Inactive title bar gradient color")]
            public Color GradientInactiveTitle
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\GradientInactiveTitle");
                set => this.SetColor(@"HKCU\Control Panel\Colors\GradientInactiveTitle", value);
            }

            [Category("Title Bar Colors")]
            [DisplayName("Inactive title bar text color")]
            public Color InactiveTitleText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InactiveTitleText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InactiveTitleText", value);
            }

            #endregion Title Bar Colors

            #region Tooltip Colors

            [Category("Tooltip Colors")]
            [DisplayName("Tooltip background color")]
            public Color InfoWindow
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InfoWindow");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InfoWindow", value);
            }

            [Category("Tooltip Colors")]
            [DisplayName("Tooltip text color")]
            public Color InfoText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InfoText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InfoText", value);
            }

            #endregion Tooltip Colors

            #region Window Colors

            [Category("Window Colors")]
            [DisplayName("Window background color")]
            public Color Window
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\Window");
                set => this.SetColor(@"HKCU\Control Panel\Colors\Window", value);
            }

            [Category("Window Colors")]
            [DisplayName("Window frame color")]
            public Color WindowFrame
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\WindowFrame");
                set => this.SetColor(@"HKCU\Control Panel\Colors\WindowFrame", value);
            }

            [Category("Window Colors")]
            [DisplayName("Window text color")]
            public Color WindowText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\WindowText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\WindowText", value);
            }

            #endregion Window Colors

            #region Border Colors

            [Category("Border Colors")]
            [DisplayName("Active border color")]
            public Color ActiveBorder
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\ActiveBorder");
                set => this.SetColor(@"HKCU\Control Panel\Colors\ActiveBorder", value);
            }

            [Category("Border Colors")]
            [DisplayName("Inactive border color")]
            public Color InactiveBorder
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\InactiveBorder");
                set => this.SetColor(@"HKCU\Control Panel\Colors\InactiveBorder", value);
            }

            #endregion Border Colors

            #region Background Colors

            [Category("Background Colors")]
            [DisplayName("Application workspace color")]
            public Color AppWorkspace
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\AppWorkspace");
                set => this.SetColor(@"HKCU\Control Panel\Colors\AppWorkspace", value);
            }

            [Category("Background Colors")]
            [DisplayName("Background")]
            public Color Background
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\Background");
                set => this.SetColor(@"HKCU\Control Panel\Colors\Background", value);
            }

            #endregion Background Colors

            #region Highlight Colors

            [Category("Highlight Colors")]
            [DisplayName("Highlight background color")]
            public Color Hilight
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\Hilight");
                set => this.SetColor(@"HKCU\Control Panel\Colors\Hilight", value);
            }

            [Category("Highlight Colors")]
            [DisplayName("Highlight text color")]
            public Color HilightText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\HilightText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\HilightText", value);
            }

            #endregion Highlight Colors

            #region Miscellaneous Colors

            [Category("Miscellaneous Colors")]
            [DisplayName("Disabled text color")]
            public Color GrayText
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\GrayText");
                set => this.SetColor(@"HKCU\Control Panel\Colors\GrayText", value);
            }

            [Category("Miscellaneous Colors")]
            [DisplayName("Scrollbar color")]
            public Color Scrollbar
            {
                get => this.GetColor(@"HKCU\Control Panel\Colors\Scrollbar");
                set => this.SetColor(@"HKCU\Control Panel\Colors\Scrollbar", value);
            }

            #endregion Miscellaneous Colors

            private Color GetColor(string path)
            {
                string[] values = RegistryHelper.GetValue<string>(path).Split(' ');

                byte r = byte.Parse(values[0]);
                byte g = byte.Parse(values[1]);
                byte b = byte.Parse(values[2]);

                return Color.FromArgb(r, g, b);
            }

            private void SetColor(string path, Color color) => RegistryHelper.SetValue(path, $"{color.R} {color.G} {color.B}");
        }
    }
}