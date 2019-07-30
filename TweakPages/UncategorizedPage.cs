using Microsoft.Win32;
using System.ComponentModel;
using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    internal class UncategorizedPage : TweakPage
    {
        public UncategorizedPage() : base("Uncategorized")
        {
        }

        [DisplayName("Cleanup program")]
        [DefaultValue(@"%SystemRoot%\System32\cleanmgr.exe /D %c")]
        public string CleanupProgram
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath\ ");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath\ ", value, RegistryValueKind.ExpandString);
        }

        [DisplayName("Defragmentation program")]
        [DefaultValue(@"%systemroot%\system32\dfrgui.exe")]
        public string DefragmentationProgram
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath\ ");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath\ ", value, RegistryValueKind.ExpandString);
        }

        [Category("Remote Desktop")]
        [DisplayName("Listening port")]
        [DefaultValue(3389)]
        public int RDPPortNumber
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\PortNumber");
            set => RegistryHelper.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\PortNumber", value);
        }

        [DisplayName("Metro (Developer Preview)")]
        [OperatingSystemSupported(OperatingSystemVersion.Windows8Developer)]
        public bool MetroDeveloper
        {
            get => RegistryHelper.GetValue<int>(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RPEnabled", 0) == 1;
            set => RegistryHelper.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RPEnabled", value ? 1 : 0);
        }

    }
}