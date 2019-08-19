using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.Drawing;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

/// TweakUtility - IMPORTANT NOTES
/// Please use vanilla versions for default values. Do not use customized/bootleg versions of Windows operating systems to get
/// the most-authentic default values.
/// Written by PF94, July 15th 2019

namespace TweakUtility.TweakPages
{
    internal class AdvancedPage : TweakPage
    {
        internal AdvancedPage() : base("Advanced", new OEMInformation(), new DiskCleanupPage()) => this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -22);

        // I'M HAVING ATTENTION
        [DisplayName("Verbose Messages")]
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
        [Category("Registration")]
        public string RegisteredOwner
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOwner", value);
        }

        [DisplayName("Organization")]
        [Category("Registration")]
        public string RegisteredOrganization
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\RegisteredOrganization", value);
        }

        [DisplayName("Title")]
        [Category("Legal Notice")]
        public string LegalNoticeCaption
        {
            get => RegistryHelper.GetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeCaption", "");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeCaption", value);
        }

        [DisplayName("Text")]
        [Category("Legal Notice")]
        public string LegalNoticeText
        {
            get => RegistryHelper.GetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeText", "");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\LegalNoticeText", value);
        }

        //[DisplayName("Windows System File Checker")]
        //[OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsVista)]
        ////[DefaultValue(WindowsSFCMode.Enabled)]
        //public WindowsSFCMode WindowsSFC
        //{
        //    get => (WindowsSFCMode)RegistryHelper.GetValue<int>(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon\SFCDisable");
        //    set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\Winlogon\SFCDisable", (int)value);
        //}

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
                this.Icon = Icons.Information;
            }

            public string Logo
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo");
                    }
                    else
                    {
                        return Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\OEMLogo.bmp");
                    }
                }

                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo", value);
                    }
                    else
                    {
                        using (var image = Image.FromFile(value))
                        {
                            image.Save(@"%SystemRoot%\system32\OEMLogo.bmp");
                        }
                    }
                }
            }

            public string Manufacturer
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer");
                    }
                    else
                    {
                        return NativeHelpers.IniReadValue("General", "Manufacturer", @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }

                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer", value);
                    }
                    else
                    {
                        NativeHelpers.IniWriteValue("General", "Manufacturer", value, @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
            }

            public string Model
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model");
                    }
                    else
                    {
                        return NativeHelpers.IniReadValue("General", "Model", @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model", value);
                    }
                    else
                    {
                        NativeHelpers.IniWriteValue("General", "Model", value, @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
            }

            [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
            [DisplayName("Support hours")]
            public string SupportHours
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours", value);
            }

            [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
            [DisplayName("Support phone number")]
            public string SupportPhone
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone", value);
            }

            [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
            [DisplayName("Support URL")]
            public string SupportURL
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL", value);
            }

            [Visible(true)]
            public void Preview()
            {
                if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
                {
                    Process.Start("control.exe", "system");
                }
                else
                {
                    Process.Start(new ProcessStartInfo("sysdm.cpl") { UseShellExecute = true });
                }
            }
        }
    }
}