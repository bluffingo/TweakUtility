using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using static TweakUtility.NativeMethods;
using System.Runtime.InteropServices;

namespace TweakUtility.TweakPages
{
    public partial class DiskCleanupPageView : UserControl
    {
        private List<DiskCleanupHandler> Handlers { get; } = new List<DiskCleanupHandler>();

        public DiskCleanupPageView() => this.InitializeComponent();

        private void DiskCleanupPageView_Load(object sender, EventArgs e)
        {
            propertyGrid.CommandsVisibleIfAvailable = false;

            Handlers.Clear();

            using (RegistryKey key = Program.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\VolumeCaches\"))
            {
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    RegistryKey subKey = key.OpenSubKey(subKeyName, true);

                    if ((string)subKey.GetValue(null, "") != "{C0E13E61-0CC6-11d1-BBB6-0060978B2AE6}")
                    {
                        subKey.Close();
                        continue;
                    }

                    Handlers.Add(new DiskCleanupHandler(subKey));
                }
            }

            listView.Items.Clear();
            imageList.Images.Clear();

            foreach (DiskCleanupHandler handler in Handlers)
            {
                var item = new ListViewItem(handler.GetDisplayName())
                {
                    Tag = handler
                };

                Icon icon = handler.Icon;
                if (icon != null)
                {
                    imageList.Images.Add(handler.KeyName, icon);
                    item.ImageKey = handler.KeyName;
                }

                listView.Items.Add(item);
            }
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) => propertyGrid.SelectedObject = e.Item?.Tag;
    }

    /// <remarks>
    /// Reference: https://docs.microsoft.com/en-us/windows/win32/lwef/disk-cleanup
    /// </remarks>
    public class DiskCleanupPage : TweakPage
    {
        public DiskCleanupPage() : base("Disk Cleanup") => this.CustomView = new DiskCleanupPageView();
    }

    public class DiskCleanupHandler : IDisposable
    {
        public DiskCleanupHandler(RegistryKey key) => _key = key;

        private readonly RegistryKey _key;

        public string[] Folder
        {
            get => ((string)_key.GetValue("Folder", "", RegistryValueOptions.DoNotExpandEnvironmentNames)).Split(new[] { '|', ':' });
            set => _key.SetValue("Folder", string.Join("|", value), RegistryValueKind.String);
        }

        [DisplayName("File list")]
        public string[] FileList
        {
            get => ((string)_key.GetValue("FileList", "", RegistryValueOptions.DoNotExpandEnvironmentNames)).Split(new[] { '|', ':' });
            set => _key.SetValue("FileList", string.Join("|", value), RegistryValueKind.String);
        }

        [DisplayName("Icon path")]
        public string IconPath
        {
            get => (string)_key.GetValue("IconPath", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
            set => _key.GetValue("IconPath", value);
        }

        [Browsable(false)]
        public string KeyName => _key.Name;

        public string Display
        {
            get => _key.GetValue("Display", null) is string displayName ? displayName : null;
            set => _key.SetValue("Display", value, RegistryValueKind.String);
        }

        public string GetDisplayName()
        {
            if (Display == null)
            {
                return Path.GetFileName(_key.Name);
            }

            if (Display[0] == '@')
            {
                //Split DLL path and resource id
                string[] split = Display.Substring(1).Split(',');

                var id = int.Parse(split[1]);

                if (id < 0)
                {
                    id *= -1;
                }

                return ExtractStringFromDLL(split[0], id);
            }

            return Display;
        }

        [DisplayName("Icon preview")]
        public Icon Icon
        {
            get
            {
                if (string.IsNullOrWhiteSpace(IconPath))
                {
                    return null;
                }

                string[] split = IconPath.Split(',');

                if (split.Length == 1)
                {
                    return new Icon(split[0]);
                }

                var icon = GetIconFromGroup(Environment.ExpandEnvironmentVariables(split[0]), int.Parse(split[1]));
                return icon;
            }
        }

        public void Dispose() => _key.Dispose();

        private Icon GetIconFromGroup(string file, int id)
        {
            ExtractIconEx(file, id, out _, out IntPtr small, 1);
            try
            {
                return Icon.FromHandle(small);
            }
            catch
            {
                return null;
            }
        }

        public static string ExtractStringFromDLL(string file, int number)
        {
            IntPtr lib = LoadLibraryEx(file, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE | LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
            var result = new StringBuilder(2048);
            LoadString(lib, number, result, result.Capacity);
            FreeLibrary(lib);
            return result.ToString();
        }
    }
}