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
    internal class CustomizationPage : TweakPage
    {
        public CustomizationPage() : base("Customization")
        {
        }

        [DisplayName("Disable Notification Center")]
        [Description("Disables the Notification Center/Action Center. Defaults to false.")]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
        public bool DisableNotificationCenter
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\Explorer"))
                {
                    if (subKey == null)
                    {
                        return false;
                    }

                    return (int)subKey.GetValue("DisableNotificationCenter", RegistryValueKind.DWord) == 1;
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"Software\Policies\Microsoft\Windows\Explorer", true))
                {
                    subKey.SetValue("DisableNotificationCenter", value ? 1 : 0, RegistryValueKind.DWord);
                }
            }
        }
    }
}