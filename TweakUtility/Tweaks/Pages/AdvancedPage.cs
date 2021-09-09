using Microsoft.Win32;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class AdvancedPage : TweakPage
    {
        internal AdvancedPage() : base("Advanced", new OEMInformationPage(), new HostsPage(), new DiskCleanupPage(), new RestorePointsPage(), new EnvironmentVariablesPage()) => this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -22);

        [DisplayName("Enable verbose messages")]
        public bool VerboseMessages
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus", value ? 1 : 0);
        }

        [DisplayName("Disable all Windows hotkeys (except lock)")]
        [RefreshRequired(RestartType.ExplorerRestart)]
        public bool DisableWindowsHotkeys
        {
            get => RegistryHelper.GetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoWinKeys", 0) == 1;
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoWinKeys", value ? 1 : 0);
        }

        [DisplayName("Disable specific hotkeys")]
        [RefreshRequired(RestartType.ExplorerRestart)]
        public string DisabledWindowsHotkeys
        {
            get => RegistryHelper.GetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\DisabledHotkeys", "");
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\DisabledHotkeys", value, RegistryValueKind.ExpandString);
        }

        [DisplayName("Owner")]
        [Category("Registration info")]
        public string RegisteredOwner
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner", value);
        }

        [DisplayName("Organization")]
        [Category("Registration info")]
        public string RegisteredOrganization
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization", value);
        }

        [DisplayName("Title")]
        [Category("Login screen legal notice")]
        public string LegalNoticeCaption
        {
            get => RegistryHelper.GetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeCaption", "");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeCaption", value);
        }

        [DisplayName("Text")]
        [Category("Login screen legal notice")]
        public string LegalNoticeText
        {
            get => RegistryHelper.GetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeText", "");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeText", value);
        }
    }
}