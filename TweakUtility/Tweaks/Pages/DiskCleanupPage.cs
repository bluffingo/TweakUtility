using Microsoft.Win32;

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using TweakUtility.Tweaks.Views;
using static TweakUtility.Helpers.NativeHelpers;

namespace TweakUtility.Tweaks.Pages
{
    /// <remarks>
    /// Reference: https://docs.microsoft.com/en-us/windows/win32/lwef/disk-cleanup
    /// </remarks>
    internal class DiskCleanupPage : TweakPage
    {
        public DiskCleanupPage() : base("Disk Cleanup")
        {
            this.CustomView = new DiskCleanupPageView();
            this.Icon = ExtractIcon(@"%systemroot%\System32\cleanmgr.exe", -0);
        }
    }

    internal sealed class DiskCleanupHandler : IDisposable
    {
        public DiskCleanupHandler(RegistryKey key) => this.Key = key;

        [Browsable(false)]
        public RegistryKey Key { get; }

        public string[] Folder
        {
            get => ((string)this.Key.GetValue("Folder", "", RegistryValueOptions.DoNotExpandEnvironmentNames)).Split(new[] { '|', ':' });
            set => this.Key.SetValue("Folder", string.Join("|", value), RegistryValueKind.String);
        }

        [DisplayName("File list")]
        public string[] FileList
        {
            get => ((string)this.Key.GetValue("FileList", "", RegistryValueOptions.DoNotExpandEnvironmentNames)).Split(new[] { '|', ':' });
            set => this.Key.SetValue("FileList", string.Join("|", value), RegistryValueKind.String);
        }

        [DisplayName("Icon path")]
        public string IconPath
        {
            get => (string)this.Key.GetValue("IconPath", "", RegistryValueOptions.DoNotExpandEnvironmentNames);
            set => this.Key.GetValue("IconPath", value);
        }

        [Browsable(false)]
        public string KeyName => this.Key.Name;

        [DisplayName("Display name")]
        public string Display
        {
            get => this.Key.GetValue("Display", null) is string displayName ? displayName : null;
            set => this.Key.SetValue("Display", value, RegistryValueKind.String);
        }

        [Browsable(false)]
        public string DisplayName
        {
            get
            {
                if (this.Display == null)
                {
                    return Path.GetFileName(this.Key.Name);
                }

                if (this.Display[0] == '@')
                {
                    //Split DLL path and resource id
                    string[] split = this.Display.Substring(1).Split(',');

                    int id = int.Parse(split[1]);

                    if (id < 0)
                    {
                        id *= -1;
                    }

                    return ExtractString(split[0], id);
                }

                return this.Display;
            }
        }

        [DisplayName("Icon preview")]
        public Icon Icon
        {
            get
            {
                if (string.IsNullOrEmpty(this.IconPath))
                {
                    return null;
                }

                string[] split = this.IconPath.Split(',');

                if (split.Length == 1)
                {
                    return new Icon(split[0]);
                }

                Icon icon = ExtractIcon(Environment.ExpandEnvironmentVariables(split[0]), int.Parse(split[1]));
                return icon;
            }
        }

        public void Dispose()
        {
            this.Key.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}