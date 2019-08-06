using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Windows.Forms;

using TweakUtility.Extensions;
using TweakUtility.Forms;
using TweakUtility.Helpers;

namespace TweakUtility
{
    internal static class Program
    {
        public static readonly StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public static RegistryKey CurrentUser;
        public static RegistryKey LocalMachine;
        public static ExtensionLoader Loader { get; } = new ExtensionLoader();
        public static List<TweakPage> Pages { get; } = new List<TweakPage>();

        public static void CreateFolders()
        {
            void CreateFolder(string name, string display, string description, bool important = false)
            {
                string path = Path.GetFullPath(name);

                if (Directory.Exists(path))
                {
                    return;
                }

                Directory.CreateDirectory(path);

                new DirectoryInfo(path).Attributes |= FileAttributes.System;

                string desktopPath = Path.Combine(path, "desktop.ini");
                string text = "[.ShellClassInfo]\r\n";

                if (!string.IsNullOrWhiteSpace(display))
                {
                    text += $"LocalizedResourceName={display}\r\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    text += $"ToolTip={description}\r\n";
                }

                if (important)
                {
                    text += $"ConfirmFileOp=1\r\n";
                }

                File.WriteAllText(desktopPath, text);
                File.SetAttributes(desktopPath, File.GetAttributes(desktopPath) | FileAttributes.Hidden);
            }

            CreateFolder("extensions", Properties.Strings.Extensions, Properties.Strings.Extensions_FolderDescription);
            CreateFolder("backups", Properties.Strings.Backups, Properties.Strings.Backups_FolderDescription, true);
        }

        /// <summary>
        /// Opens an URL
        /// </summary>
        public static void OpenURL(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

        /// <summary>
        /// Finds a running Windows Explorer instance and causes it restart.
        /// </summary>
        public static void RestartExplorer()
        {
            using (var rm = new RestartManagerSession())
            {
                rm.RegisterProcess(Process.GetProcessesByName("explorer"));
                rm.Shutdown(RestartManagerSession.ShutdownType.Normal);
                rm.Restart();
            }
        }

        /// <summary>
        /// Opens the GitHub Issues page of TweakUtility, preset with exception details.
        /// </summary>
        public static void SendCrashReport(Exception ex)
        {
            string title = HttpUtility.UrlEncode(ex.Message);
            string body = $"***{Properties.Strings.Report_Disclaimer}***\n\n"
                + $"**Message**\n{ex.Message}\n\n"
                + $"**Source**\n{ex.Source}\n\n"
                + $"**Stack Trace**\n```{ex.StackTrace}```\n\n";

            body = HttpUtility.UrlEncode(body);

            string url = $"https://github.com/Craftplacer/TweakUtility/issues/new?labels=crash+report&title={title}&body={body}";

            OpenURL(url);
        }

        /// <summary>
        /// Finds a suitable registry view for this system architecture
        /// </summary>
        private static RegistryView GetRegistryView() => Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if !DEBUG
            try
            {
#endif
            LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, GetRegistryView());
            CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, GetRegistryView());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");

            using (var splash = new SplashForm())
            {
                Application.Run(splash);
            }

            using (var main = new MainForm())
            {
                Application.Run(main);
            }
#if !DEBUG
            }
            catch (Exception ex)
            {
                SendCrashReport(ex);
                throw;
            }
#endif
        }
    }
}