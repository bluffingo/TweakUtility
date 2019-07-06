using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public class AdvancedPage : TweakPage
    {
        public AdvancedPage() : base("Advanced")
        {
        }

        [DisplayName("Verbose Messages")]
        [DefaultValue(false)]
        public bool VerboseMessages
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System"))
                {
                    return (int)subKey.GetValue("VerboseStatus", RegistryValueKind.DWord) == 1;
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Policies\System"))
                {
                    subKey.SetValue("VerboseStatus", value ? 1 : 0, RegistryValueKind.DWord);
                }
            }
        }

        [DisplayName("Windows System File Checker")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsXP)]
        [DefaultValue(WindowsSFCMode.Enabled)]
        public WindowsSFCMode WindowsSFC
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon"))
                {
                    return (WindowsSFCMode)subKey.GetValue("SFCDisable", RegistryValueKind.DWord);
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon"))
                {
                    subKey.SetValue("SFCDisable", (int)value, RegistryValueKind.DWord);
                }
            }
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