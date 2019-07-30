using System.ComponentModel;
using System.Drawing;
using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    internal class CustomizationPage : TweakPage
    {
        public CustomizationPage() : base("Customization", new ColorsPage())
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
        [DefaultValue(false)]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)] //also works with Windows 10 RTM and november update, no need to make it 1607+ only.
        public bool AppsUseLightTheme
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme") == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme", value ? 1 : 0);
        }

        [DisplayName("System uses light theme")]
        [DefaultValue(false)] //:shrug:
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)] //not really...
        public bool SystemUsesLightTheme
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\SystemUsesLightTheme") == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize\SystemUsesLightTheme", value ? 1 : 0);
        }

        public enum WallpaperStyle
        {
            Tile = 0,
            Stretch = 2,
            Fit = 6,
            Fill = 10,
            Span = 22,
        }

        public class ColorsPage : TweakPage
        {
            public ColorsPage() : base("Classic Theme Colors") => this.Icon = Properties.Resources.colors;

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