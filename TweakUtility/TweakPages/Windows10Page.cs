using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
    internal class Windows10Page : TweakPage
    {
        internal Windows10Page() : base("Windows 10", new AudioTransitions())
        {
            this.Icon = Properties.Resources.windows10;
        }

        //Can we check what version of Windows 10 the end user is running? I'm (PF94) running 1903 and the "Classic Volume" control doesn't work, The calender/clock hasen't worked since November Update or Aniversery update.

        [DisplayName("Disable Notification Center")]
        //[DefaultValue(false)]
        [RefreshRequired(RestartType.ExplorerRestart)]
        public bool DisableNotificationCenter
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\Software\Policies\Microsoft\Windows\Explorer\DisableNotificationCenter", value ? 1 : 0);
        }

        [DisplayName("Enable classic volume control")]
        public bool EnableMtcUvc
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\EnableMtcUvc", 1) == 0;
            set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows NT\CurrentVersion\EnableMtcUvc", value ? 0 : 1);
        }

        [DisplayName("Enable classic tray clock")]
        public bool UseWin32TrayClockExperience
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseWin32TrayClockExperience", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseWin32TrayClockExperience", value ? 1 : 0);
        }

        [DisplayName("Enable classic battery flyout")]
        public bool UseWin32BatteryFlyout
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseWin32BatteryFlyout", 0) == 1;
            set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseWin32BatteryFlyout", value ? 1 : 0);
        }

        [RefreshRequired(RestartType.ExplorerRestart)]
        [DisplayName("Enable classic notifications panel")]
        public bool UseActionCenterExperience
        {
            get => RegistryHelper.GetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseActionCenterExperience", 1) == 0;
            set => RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\ImmersiveShell\UseActionCenterExperience", value ? 0 : 1);
        }

        /* [Browsable(true)]
        [DisplayName("Remove ALL UWP Applications")]
        public void RemoveUWP()
        {
            //okay
        }*/

        [Visible(true)]
        [DisplayName("Uninstall OneDrive")]
        [RegistryKeyRequired(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\OneDriveSetup.exe\UninstallString")]
        public void UninstallOneDrive()
        {
            string uninstallString = RegistryHelper.GetValue<string>(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\OneDriveSetup.exe\UninstallString");
            if (uninstallString == null)
            {
                if (MessageBox.Show(Properties.Strings.OneDrive_Uninstall_InstallerNotFound, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                string uninstallPath = uninstallString.Split('/')[0];
                uninstallPath = uninstallPath.Substring(0, uninstallPath.Length - 2);

                var process = Process.Start(new ProcessStartInfo(uninstallPath, "/uninstall") { UseShellExecute = true });
                process.WaitForExit(300000);

                if (process.ExitCode != 0 &&
                    MessageBox.Show(Properties.Strings.OneDrive_Uninstall_Warning, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            bool userUninstall = RegistryHelper.GetValue(@"HKCU\SOFTWARE\Microsoft\OneDrive\UserInitiatedUninstall", 0) == 1;
            Environment.SetEnvironmentVariable("OneDrive", "", EnvironmentVariableTarget.User);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Classes\AppID\OneDrive.EXE", false);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Classes\grvopen", false);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Classes\odopen", false);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Microsoft\OneDrive", false);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\Microsoft\SkyDrive", false);
            RegistryHelper.DeleteKey(@"HKCU\SOFTWARE\SyncEngines\Providers\OneDrive", false);
            RegistryHelper.DeleteKey(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\OneDriveRamps", false);
            RegistryHelper.DeleteValue(@"HKCU\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run\OneDriveSetup", false);
            RegistryHelper.DeleteValue(@"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run\OneDrive", false);

            for (int i = 1; i < 9; i++)
            {
                RegistryHelper.DeleteKey($@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers\OneDrive{i}", false);
            }

            //Show MessageBox asking user to confirm setting this flag, when flag isn't set.
            if (!userUninstall && MessageBox.Show(Properties.Strings.OneDrive_Uninstall_SetFlagMessage, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\OneDrive\UserInitiatedUninstall", 1);
            }

            NativeMethods.SHGetKnownFolderPath(new Guid("A52BBA46-E9E1-435F-B3D9-28DAA648C0F6"), 0, IntPtr.Zero, out string oneDrivePath);
            if (Directory.Exists(oneDrivePath) && MessageBox.Show(Properties.Strings.OneDrive_Uninstall_DeleteFolder, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            retry:
                try
                {
                    Directory.Delete(oneDrivePath, true);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(string.Format(Properties.Strings.OneDrive_Uninstall_DeleteFolder_Failed, ex.Message), Properties.Strings.Application_Name, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
                    {
                        goto retry;
                    }
                }
            }

            MessageBox.Show(Properties.Strings.OneDrive_Uninstall_Success, Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private class AudioTransitions : TweakPage
        {
            public AudioTransitions() : base("Audio Transitions") => this.Icon = Properties.Resources.volume;

            [DisplayName("Transition time for turning volume down")]
            public int VolumeDownTransitionTime
            {
                get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeDownTransitionTime");
                set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeDownTransitionTime", value);
            }

            [DisplayName("Transition time for turning volume up")]
            public int VolumeUpTransitionTime
            {
                get => RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeUpTransitionTime");
                set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Audio\VolumeUpTransitionTime", value);
            }
        }
    }
}