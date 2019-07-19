using System.ComponentModel;
using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    public class Windows10Page : TweakPage
    {
        public Windows10Page() : base("Windows 10")
        {
        }

        [DisplayName("Disable Notification Center")]
        [DefaultValue(false)]
        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        public bool DisableNotificationCenter
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", value ? 1 : 0);
        }
    }
}