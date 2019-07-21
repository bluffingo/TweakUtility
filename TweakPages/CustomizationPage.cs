using System.ComponentModel;

using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    internal class CustomizationPage : TweakPage
    {
        public CustomizationPage() : base("Customization")
        {
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
        [DefaultValue(false)]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
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
    }
}