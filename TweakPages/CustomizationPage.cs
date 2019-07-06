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
        [Description("Disables the Notification Center/Action Center.")]
        [DefaultValue(false)]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
        public bool DisableNotificationCenter
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"Software\Policies\Microsoft\Windows\Explorer"))
                {
                    return (int)subKey.GetValue("DisableNotificationCenter", 0) == 1;
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"Software\Policies\Microsoft\Windows\Explorer"))
                {
                    subKey.SetValue("DisableNotificationCenter", value ? 1 : 0, RegistryValueKind.DWord);
                }
            }
        }
    }
}