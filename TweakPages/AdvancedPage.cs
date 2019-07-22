using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using TweakUtility.Attributes;

/// TweakUtility - IMPORTANT NOTES
/// Please use vanilla versions for default values.Do not use customized/bootleg versions of Windows operating systems to get
/// the most-authentic default values.
/// Written by PF94, July 15th 2019

namespace TweakUtility.TweakPages
{
    public class AdvancedPage : TweakPage
    {
        public AdvancedPage() : base("Advanced", new OEMInformation())
        {
        }

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

        [Browsable(true)]
        [DisplayName("Delete OneDrive trails")]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
        public void DeleteOneDriveTrails()
        {
            string message = "OneDrive trails have been deleted.";

            Environment.SetEnvironmentVariable("OneDrive", "", EnvironmentVariableTarget.User);
            RegistryHelper.DeleteValue(@"HKCU\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run\OneDriveSetup", false);

            if (RegistryHelper.GetValue(@"HKCU\SOFTWARE\Microsoft\OneDrive\UserInitiatedUninstall", 0) == 1)
            {
                message += "\nDid you know, that OneDrive stored that *you* uninstalled it?";
            }

            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Microsoft\OneDrive", false);

            MessageBox.Show(message, "Tweak Utility", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public class OEMInformation : TweakPage
        {
            public OEMInformation() : base("OEM Information")
            {
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
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone ");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone ", value);
            }

            [DisplayName("Support URL")]
            public string SupportURL
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL ");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL ", value);
            }

            [Browsable(true)]
            public void Preview() => Process.Start(new ProcessStartInfo("control.exe", "system")
            {
                UseShellExecute = true
            });
        }
    }
}