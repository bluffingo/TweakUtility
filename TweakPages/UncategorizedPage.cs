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
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string CleanupProgram
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath"))
                {
                    return (string)subKey.GetValue(null, RegistryValueKind.ExpandString);
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath", true))
                {
                    subKey.SetValue(null, value, RegistryValueKind.ExpandString);
                }
            }
        }

        [DisplayName("Defragmentation program")]
        [DefaultValue(@"%systemroot%\system32\dfrgui.exe")]
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string DefragmentationProgram
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath"))
                {
                    return (string)subKey.GetValue(null, RegistryValueKind.ExpandString);
                }
            }
            set
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath", true))
                {
                    subKey.SetValue(null, value, RegistryValueKind.ExpandString);
                }
            }
        }
    }

    public class AudioTransitions : TweakPage
    {
        public AudioTransitions() : base("Audio Transitions")
        {
        }

        public int VolumeDownTransitionTime
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\"))
                {
                    return (int)subKey.GetValue("VolumeDownTransitionTime", RegistryValueKind.DWord);
                }
            }

            set
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\", true))
                {
                    subKey.SetValue("VolumeDownTransitionTime", value, RegistryValueKind.DWord);
                }
            }
        }

        public int VolumeUpTransitionTime
        {
            get
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\"))
                {
                    return (int)subKey.GetValue("VolumeUpTransitionTime", RegistryValueKind.DWord);
                }
            }

            set
            {
                using (RegistryKey subKey = Program.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\", true))
                {
                    subKey.SetValue("VolumeUpTransitionTime", value, RegistryValueKind.DWord);
                }
            }
        }
    }
}