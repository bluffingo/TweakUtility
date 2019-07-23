using System.ComponentModel;
using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
    public class Windows10Page : TweakPage
    {
        public Windows10Page() : base("Windows 10", new AudioTransitions())
        {
            this.Icon = Properties.Resources.windows10;
        }

        [DisplayName("Disable Notification Center")]
        [DefaultValue(false)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        public bool DisableNotificationCenter
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", value ? 1 : 0);
        }

        public class AudioTransitions : TweakPage
        {
            public AudioTransitions() : base("Audio Transitions")
            {
                this.Icon = Properties.Resources.volume;
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
}