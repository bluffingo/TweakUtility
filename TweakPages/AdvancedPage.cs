using System.ComponentModel;

namespace TweakUtility.TweakPages
{
    public class AdvancedPage : TweakPage
    {
        public AdvancedPage() : base("Advanced") => this.CustomView = new TweakPageView(this);

        [DisplayName("Owner")]
        [Category("Registration")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string RegisteredOwner
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner", value);
        }

        [DisplayName("Organization")]
        [Category("Registration")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
        public string RegisteredOrganization
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization", value);
        }

        [DisplayName("Verbose Messages")]
        [DefaultValue(false)]
        public bool VerboseMessages
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus") == 1;
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus", value ? 1 : 0);
        }

        [DisplayName("Windows System File Checker")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsXP)]
        [DefaultValue(WindowsSFCMode.Enabled)]
        public WindowsSFCMode WindowsSFC
        {
            get => (WindowsSFCMode)RegistryHelper.GetValue<int>(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon\SFCDisable");
            set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon\SFCDisable", (int)value);
        }

        public enum WindowsSFCMode
        {
            [Description("Disabled with prompts")]
            Enabled = 0,

            [Description("Disabled with reactivation prompts")]
            DisablePrompt = 1,

            [Description("Disabled without any reactivation prompts")]
            DisableNoPrompt = 2,

            [Description("Enabled without prompts")]
            EnabledNoPrompt = 4
        }
    }
}