using System.Diagnostics;
using System.IO;
using System.Net;
using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class CustomizationPage : TweakPage
    {
        internal CustomizationPage() : base("Customization", new ColorsPage(), new BackgroundsPage())
        {
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
            {
                this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\imageres.dll", -197);
            }
            else
            {
                this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -250);
            }
        }

        public CustomizationPage(string name, params TweakPage[] subPages) : base(name, subPages)
        {
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
        [DisplayName("Enable Aero Lite Theme")]
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

        [OperatingSystemSupported(OperatingSystemVersion.Windows7)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        [DisplayName("Show seconds on taskbar")]
        public bool ShowSeconds
        {
            get => RegistryHelper.GetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSecondsInsystemClock", 0) == 1;
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSecondsInsystemClock", value ? 1 : 0);
        }

        [OperatingSystemSupported(OperatingSystemVersion.Windows7)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        [DisplayName("Show build number on Dekstop")]
        public bool ShowBuildNumberDesktop
        {
            get => RegistryHelper.GetValue(@"HKCU\Control Panel\Desktop\PaintDesktopVersion", 0) == 1;
            set => RegistryHelper.SetValue(@"HKCU\Control Panel\Desktop\PaintDesktopVersion", value ? 1 : 0);
        }
    }
}