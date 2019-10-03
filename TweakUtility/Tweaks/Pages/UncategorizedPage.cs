using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Net;

using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    internal class UncategorizedPage : TweakPage
    {
        internal UncategorizedPage() : base("Uncategorized")
        {
        }

        [DisplayName("Cleanup program")]
        //[DefaultValue(@"%SystemRoot%\System32\cleanmgr.exe /D %c")]
        public string CleanupProgram
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath\ ");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\cleanuppath\ ", value, RegistryValueKind.ExpandString);
        }

        [DisplayName("Defragmentation program")]
        //[DefaultValue(@"%systemroot%\system32\dfrgui.exe")]
        public string DefragmentationProgram
        {
            get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath\ ");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\MyComputer\DefragPath\ ", value, RegistryValueKind.ExpandString);
        }

        [Category("Remote Desktop")]
        [DisplayName("Listening port")]
        //[DefaultValue(3389)]
        public int RDPPortNumber
        {
            get => RegistryHelper.GetValue<int>(@"HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\PortNumber");
            set => RegistryHelper.SetValue(@"HKLM\SYSTEM\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp\PortNumber", value);
        }

        [Visible(true)]
        [Category("Windows Media Player")]
        [DisplayName("Install Deskband")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
        public void InstallWMPDeskBand(ProgressIndicator indicator)
        {
            indicator.Initialize(5);

            using (var client = new WebClient())
            {
                string x86 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Windows Media Player", "wmpband.dll");
                string x64 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Windows Media Player", "wmpband.dll");

                if (!File.Exists(x86))
                {
                    indicator.SetProgress(1, "Downloading 32-bit deskband...");
                    client.DownloadFile("https://raw.githubusercontent.com/Craftplacer/TweakUtility/master/Optional/wmpband/32.dll", "32.dll");

                    indicator.SetProgress(3, "Moving 32-bit deskband file...");
                    File.Move("32.dll", x86);
                }

                if (!File.Exists(x64))
                {
                    indicator.SetProgress(2, "Downloading 64-bit deskband...");
                    client.DownloadFile("https://raw.githubusercontent.com/Craftplacer/TweakUtility/master/Optional/wmpband/64.dll", "64.dll");

                    indicator.SetProgress(3, "Moving 64-bit deskband file...");
                    File.Move("64.dll", x64);
                }

                string regsvrPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "regsvr32.exe");

                indicator.SetProgress(4, "Registering 32-bit deskband...");
                Process.Start(regsvrPath, $"\"{x86}\"").WaitForExit();

                indicator.SetProgress(5, "Registering 64-bit deskband...");
                Process.Start(regsvrPath, $"\"{x64}\"").WaitForExit();
            }
        }

        [DisplayName("Metro (Developer Preview)")]
        [OperatingSystemSupported(OperatingSystemVersion.Windows8Developer, OperatingSystemVersion.Windows8Developer)]
        public bool MetroDeveloper
        {
            get => RegistryHelper.GetValue<int>(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RPEnabled", 0) == 1;
            set => RegistryHelper.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RPEnabled", value ? 1 : 0);
        }
    }
}