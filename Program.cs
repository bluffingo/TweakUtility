using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using TweakUtility.TweakPages;

namespace TweakUtility
{
    internal static class Program
    {
        public static RegistryKey LocalMachine;
        public static RegistryKey CurrentUser;

        public static List<TweakPage> Pages = new List<TweakPage>()
        {
            new CustomizationPage(),
            new InternetExplorerPage(),
            new SnippingToolPage(),
            new AdvancedPage(),
            new UncategorizedPage()
        };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, GetRegistryView());
            CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, GetRegistryView());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var splash = new SplashForm())
            {
                Application.Run(splash);
            }

            Application.Run(new MainForm());
        }

        public static void RestartExplorer()
        {
            try
            {
                IntPtr handle = NativeMethods.FindWindow("Shell_TrayWnd", null);
                NativeMethods.PostMessage(handle, NativeMethods.WM_USER + 436, (IntPtr)0, (IntPtr)0);

                while (true)
                {
                    handle = NativeMethods.FindWindow("Shell_TrayWnd", null);

                    if (handle.ToInt32() == 0)
                    {
                        break;
                    }
                }
            }
            finally
            {
                Process.Start(new ProcessStartInfo("explorer.exe")
                {
                    UseShellExecute = true
                });
            }
        }

        private static RegistryView GetRegistryView() => Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

        public static bool IsSupported(this OperatingSystemVersion mininum, OperatingSystemVersion? maximum = null)
        {
            Version mininumV = OperatingSystemVersions.GetVersion(mininum);
            Version maximumV = null;

            if (maximum.HasValue && maximum.Value != OperatingSystemVersion.None)
            {
                maximumV = OperatingSystemVersions.GetVersion(maximum.Value);
            }

            return IsSupported(mininumV, maximumV);
        }

        public static bool IsSupported(this Version mininum, Version maximum = null)
        {
            if (mininum is null)
            {
                throw new ArgumentNullException(nameof(mininum));
            }

            Version current = OperatingSystemVersions.GetCurrentVersion();

            if (current < mininum)
            {
                return false;
            }

            if (maximum != null && maximum < current)
            {
                return false;
            }

            return true;
        }

        public static int ToBgrInt(this Color color) => (0 << 24) + (color.B << 16) + (color.G << 8) + color.R;

        public static System.Drawing.Color ToBgrColor(this int bgrColor)
        {
            byte[] bytes = BitConverter.GetBytes(bgrColor);
            return System.Drawing.Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        }
    }
}