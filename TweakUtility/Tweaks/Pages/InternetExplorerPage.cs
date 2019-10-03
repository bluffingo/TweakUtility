using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

//To-do: See issue #25

namespace TweakUtility.Tweaks.Pages
{
	//This fixes the bug where Tweak Utility would crash while retrieving the tweak page icon when the user uninstalled Internet Explorer as a feature.
	[RegistryKeyRequired(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\iexplore.exe\")]
    internal class InternetExplorerPage : TweakPage
    {
        internal InternetExplorerPage() : base("Internet Explorer") => this.Icon = NativeHelpers.ExtractIcon(NativeHelpers.GetApplicationPath("iexplore.exe"), -0);

        [DisplayName("Blank page")]
        public string BlankPage
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank", value);
        }

        [DisplayName("Desktop Item Navigation Failure")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string DesktopItemNavigationFailure
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\DesktopItemNavigationFailure");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\DesktopItemNavigationFailure", value);
        }

        [DisplayName("New InPrivate window")]
        //[DefaultValue("res://ieframe.dll/inprivate.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string InPrivate
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\InPrivate");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\InPrivate", value);
        }

        [DisplayName("Cancelled Navigation")]
        //[DefaultValue("res://ieframe.dll/navcancl.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string NavigationCancelled
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NavigationCanceled");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NavigationCanceled", value);
        }

        [DisplayName("Navigation Error")]
        //[DefaultValue("res://ieframe.dll/navcancl.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string NavigationFailure
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NavigationFailure");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NavigationFailure", value);
        }

        [DisplayName("No Add-Ons Message")]
        //[DefaultValue("res://ieframe.dll/noaddon.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string NoAddons
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NoAdd-ons");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NoAdd-ons", value);
        }

        [DisplayName("No Add-Ons Infomation")]
        //[DefaultValue("res://ieframe.dll/noaddoninfo.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string NoAddonsInfo
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NoAdd-onsInfo");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\NoAdd-onsInfo", value);
        }

        [DisplayName("Offline Notice")]
        //[DefaultValue("res://ieframe.dll/offcancl.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string OfflineInformation
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\OfflineInformation");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\OfflineInformation", value);
        }

        [DisplayName("Infomation sent not cached")]
        //[DefaultValue("res://ieframe.dll/repost.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string PostNotCached
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\PostNotCached");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\PostNotCached", value);
        }

        [DisplayName("Security Risk")]
        //[DefaultValue("res://ieframe.dll/securityatrisk.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string SecurityRisk
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\SecurityRisk");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\SecurityRisk", value);
        }

        [DisplayName("New Tab")]
        //[DefaultValue("res://ieframe.dll/securityatrisk.htm")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string TabsIE
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\Tabs");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\Tabs", value);
        }
    }
}