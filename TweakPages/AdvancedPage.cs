﻿using System.ComponentModel;
using System.Diagnostics;
using TweakUtility.Attributes;
using TweakUtility.Helpers;

/// TweakUtility - IMPORTANT NOTES
/// Please use vanilla versions for default values.Do not use customized/bootleg versions of Windows operating systems to get
/// the most-authentic default values.
/// Written by PF94, July 15th 2019

namespace TweakUtility.TweakPages
{
    public class AdvancedPage : TweakPage
    {
        public AdvancedPage() : base("Advanced", new OEMInformation(), new DiskCleanupPage()) => this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -22);

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
        public bool VerboseMessages
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System\VerboseStatus", value ? 1 : 0);
        }

        [DisplayName("Windows System File Checker")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsVista)]
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

        private class OEMInformation : TweakPage
        {
            public OEMInformation() : base("OEM Information")
            {
                if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\imageres.dll", -81);
                }
                else
                {
                    this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\user32.dll", -104);
                }
            }

            public string Logo
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo", value);
            }

            public string Manufacturer
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer", value);
            }

            public string Model
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model", value);
            }

            [DisplayName("Support hours")]
            public string SupportHours
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours", value);
            }

            [DisplayName("Support phone number")]
            public string SupportPhone
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone", value);
            }

            [DisplayName("Support URL")]
            public string SupportURL
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL", value);
            }

            [Browsable(true)]
            public void Preview() => Process.Start(new ProcessStartInfo("control.exe", "system")
            {
                UseShellExecute = true
            });
        }
    }
}