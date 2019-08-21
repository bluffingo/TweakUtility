using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.ApplicationServices;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Extensions;
using TweakUtility.Forms;
using TweakUtility.Helpers;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    internal static class Program
    {
        private static Mutex Mutex = new Mutex(true, Helpers.Helpers.GetApplicationGuid().ToString());
        public static RegistryKey CurrentUser { get; private set; }
        public static RegistryKey LocalMachine { get; private set; }
        public static ExtensionLoader Loader { get; } = new ExtensionLoader();
        public static List<TweakPage> Pages { get; } = new List<TweakPage>();
        public static List<Backup> Backups { get; } = new List<Backup>();

        /// <returns>
        /// A flat view of all registered tweak pages.
        /// </returns>
        public static IEnumerable<TweakPage> GetAllTweakPages() => Pages.Flatten(p => p.SubPages);

        public static string ApplicationDirectory = new FileInfo(typeof(Program).Assembly.Location).DirectoryName;

        /// <summary>
        /// Creates folders later used by Tweak Utility.
        /// </summary>
        public static void CreateFolders()
        {
            /// <summary>
            /// Creates a folder with the specified <para see=""/>
            /// </summary>
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
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                using (var rm = new RestartManagerSession())
                {
                    rm.RegisterProcess(Process.GetProcessesByName("explorer"));
                    rm.Shutdown(RestartManagerSession.ShutdownType.Normal);
                    rm.Restart();
                }
            }
            else
            {
                Process[] processes = Process.GetProcessesByName("explorer");

                foreach (Process process in processes)
                {
                    process.Kill();
                }

                foreach (Process process in processes)
                {
                    process.Start();
                }
            }
        }

        /// <summary>
        /// Opens the GitHub issues page of Tweak Utility, preset with exception details.
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
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
#if !DEBUG
            try
            {
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Environment.CurrentDirectory = ApplicationDirectory;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr");

            if (HandleArguments(args)) //Don't start application if arguments/files were being handled (except --open)
            {
                return;
            }

            //Check if application is running already
            if (!Mutex.WaitOne(TimeSpan.Zero, true))
            {
                //Send broadcast to show the window of the current instance.
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST, NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
                return;
            }

            LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Helpers.Helpers.RegistryView);
            CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Helpers.Helpers.RegistryView);

            using (var splash = new SplashForm())
            {
                splash.Show();

                splash.SetStatus("Creating folders...");
                CreateFolders();

                splash.SetStatus("Retrieving OS Version...");
                _ = OperatingSystemVersions.CurrentVersion;

                splash.SetStatus("Loading extensions...");
                Loader.LoadExtensions();

                splash.SetStatus("Loading backups...");
                LoadBackups();

                splash.SetStatus("Initializing pages...");
                splash.InitializePages();

#if DEBUG
                splash.SetStatus("Unlocking debug page...");
                Pages.Add(new DebugPage());
#endif

                splash.Hide();
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

        internal static void LoadBackups()
        {
            IEnumerable<string> files = Directory.GetFiles("backups", "*.xml").Concat(Directory.GetFiles("backups", "*.tubk"));
            foreach (string filePath in files)
            {
                //HACK: Consider lazy loading backups (set file path, load on access)
                var backup = new Backup(filePath);
                Backups.Add(backup);
            }
        }

        /// <summary>
        /// Handles arguments.
        /// </summary>
        /// <returns>If the application can start // Arguments have been handled</returns>
        private static bool HandleArguments(string[] args)
        {
            if (args.Length == 0 || args.Any(a => a.Equals("--open", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            foreach (string argument in args)
            {
                string path = argument;

                // remove "
                if (path.StartsWith("\"") && path.EndsWith("\"")) path = path.Substring(1, path.Length - 2);

                // skip missing file
                if (!File.Exists(path)) continue;

                string fileExtension = Path.GetExtension(path).ToLower();

                if (fileExtension == ".tuex") HandleExtension(path);
                else if (fileExtension == ".tubk") HandleBackup(path);
            }

            return true;
        }

        private static void HandleExtension(string path)
        {
            var data = File.ReadAllBytes(path);
            var assembly = Assembly.Load(data);
            var extensions = Loader.GetExtensions(assembly);

            foreach (Extension extension in extensions)
            {
                using (var form = new ExtensionInstallForm(extension, path))
                {
                    form.ShowDialog();
                }
            }
        }

        private static void HandleBackup(string path)
        {
            //preloading backup to ensure it's even applicable before asking user to confirm
            var backup = new Backup(path);
            if (MessageBox.Show(Properties.Strings.Backups_Confirmation, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                backup.Apply();
        }
    }
}