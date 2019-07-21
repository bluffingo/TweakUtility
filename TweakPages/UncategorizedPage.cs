using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace TweakUtility.TweakPages
{
    internal class UncategorizedPage : TweakPage
    {
        public UncategorizedPage() : base("Uncategorized", subPages: new AudioTransitions())
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
    }

    public class AudioTransitions : TweakPage
    {
        public AudioTransitions() : base("Audio Transitions")
        {
        }

        public int VolumeDownTransitionTime
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeDownTransitionTime");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeDownTransitionTime", value);
        }

        public int VolumeUpTransitionTime
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeUpTransitionTime");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeUpTransitionTime", value);
        }
    }
}