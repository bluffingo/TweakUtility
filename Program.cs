using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TweakUtility.Forms;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    internal static class Program
    {
        public static RegistryKey LocalMachine { get; private set; }
        public static RegistryKey CurrentUser { get; private set; }
        public static List<TweakPage> Pages { get; private set; }
        public static Config Config { get; private set; }

        [STAThread]
        private static void Main()
        {
            LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, GetRegistryView());
            CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, GetRegistryView());

            LoadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Pages = new List<TweakPage>()
            {
                new CustomizationPage(),
                new InternetExplorerPage(),
                new SnippingToolPage(),
                new AdvancedPage(),
                new UncategorizedPage()
            };

            if (IsSupported(OperatingSystemVersion.Windows10))
            {
                Pages.Add(new Windows10Page());
            }

            using (var splash = new SplashForm())
            {
                Application.Run(splash);
            }

            Application.Run(new MainForm());
        }

        public static void ApplyTheme(this Control control)
        {
            if (!Config.DarkMode)
            {
                //No need to mess with the UI if the user prefers eye cancer
                return;
            }

            control.BackColor = Color.FromArgb(21, 21, 21);
            control.ForeColor = Color.White;

            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.FromArgb(55, 55, 55);
                button.FlatAppearance.BorderColor = Color.FromArgb(25, 25, 25);
            }

            foreach (Control subControl in control.Controls)
            {
                ApplyTheme(subControl);
            }
        }

        private static void LoadConfig()
        {
            if (!File.Exists("config.json"))
            {
                Config = new Config();
                SaveConfig();
            }

            string json = File.ReadAllText("config.json");
            Config = JsonConvert.DeserializeObject<Config>(json);
        }

        public static void SaveConfig()
        {
            string json = JsonConvert.SerializeObject(Config);
            File.WriteAllText("config.json", json);
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

        public static bool IsSupported(this OperatingSystemSupportedAttribute attribute) => IsSupported(attribute.Mininum, attribute.Maximum);

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