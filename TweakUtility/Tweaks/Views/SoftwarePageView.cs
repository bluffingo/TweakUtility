using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Diagnostics;
using TweakUtility.Helpers;
using TweakUtility.Enums;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace TweakUtility.Tweaks.Views
{
    public partial class SoftwarePageView : UserControl
    {
        public SoftwarePageView()
        {
            InitializeComponent();
            this.Padding = new Padding(SystemInformation.VerticalScrollBarWidth);
            bottomPanel.Padding = new Padding(4, 4 + this.Padding.All / 2, 4, 4);
            bottomPanel.Height += this.Padding.All / 2;

            iconPictureBox.Top = nameLabel.Top;

            //nameLabel.Top = 8;
            //versionLabel.Top = nameLabel.Top + 5;
        }

        public Tuple<string, string, XmlDocument>[] getUwpApplications()
        {
            using (RegistryKey key = RegistryHelper.CurrentUser.OpenSubKey(@"SOFTWARE\Classes\Local Settings\Software\Microsoft\Windows\CurrentVersion\AppModel\Repository\Packages\"))
            {
                var names = key.GetSubKeyNames();

                var array = new Tuple<string, string, XmlDocument>[names.Length];

                for (int i = 0; i < names.Length; i++)
                {
                    var name = names[i];
                    using (var subKey = key.OpenSubKey(name))
                    {
                        string id = subKey.GetValue("PackageID") as string;
                        string folder = subKey.GetValue("PackageRootFolder") as string;

                        string xmlPath = $@"{folder}\AppxManifest.xml";

                        var xml = new XmlDocument();

                        try
                        {
                            if (File.Exists(xmlPath))
                                xml.Load(xmlPath);
                        }
                        catch (UnauthorizedAccessException)
                        {
                        }

                        array[i] = Tuple.Create(id, folder, xml);
                    }
                }

                return array;
            }
        }

        public UninstallInfo[] getDesktopApplications()
        {
            using (RegistryKey key = RegistryHelper.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                string[] names = key.GetSubKeyNames();
                UninstallInfo[] info = new UninstallInfo[names.Length];

                for (int i = 0; i < names.Length; i++)
                {
                    info[i] = new UninstallInfo(key.OpenSubKey(names[i]));
                }

                return info;
            }
        }

        private void SoftwarePageView_Load(object sender, EventArgs e)
        {
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows10))
            {
                foreach (var application in getUwpApplications())
                {
                    var item = new ListViewItem()
                    {
                        Tag = application.Item1,
                        ImageKey = application.Item1,
                        Text = application.Item1,
                    };

                    if (application.Item3.ChildNodes.Count != 0)
                    {
                        item.Text = application.Item3["Package"]["Properties"]["DisplayName"].Value;
                    }

                    if (GetIcon(application.Item2, application.Item3) is Image icon)
                    {
                        smallImageList.Images.Add(application.Item1, icon);
                    }

                    listView.Items.Add(item);
                }
            }

            foreach (UninstallInfo info in this.getDesktopApplications())
            {
                if (info.Hide)
                    continue;

                var item = new ListViewItem()
                {
                    Tag = info,
                    ImageKey = info.IconPath,
                    Text = info.DisplayName,
                };

                var icon = info.Icon;
                if (icon != null)
                {
                    smallImageList.Images.Add(info.IconPath, icon);
                    largeImageList.Images.Add(info.IconPath, icon);
                }

                listView.Items.Add(item);
            }
        }

        public Image GetIcon(string folder, XmlDocument xml)
        {
            if (xml.ChildNodes.Count != 0)
                if (File.Exists($@"{folder}\{xml["Package"]["Properties"]["Icon"]}"))
                    return Image.FromFile($@"{folder}\{xml["Package"]["Properties"]["Icon"]}");

            return null;
        }

        public class UninstallInfo : IDisposable
        {
            public RegistryKey RegistryKey { get; set; }

            public UninstallInfo(RegistryKey key)
            {
                this.RegistryKey = key;
            }

            public void Dispose() => RegistryKey.Dispose();

            public string DisplayName => (string)RegistryKey.GetValue("DisplayName");

            public string DisplayVersion => (string)RegistryKey.GetValue("DisplayVersion");

            public string UninstallString => (string)RegistryKey.GetValue("UninstallString");

            public string IconPath => (string)this.RegistryKey.GetValue("DisplayIcon", "", RegistryValueOptions.DoNotExpandEnvironmentNames);

            public Icon Icon
            {
                get
                {
                    string iconPath = this.IconPath;

                    if (string.IsNullOrEmpty(iconPath))
                    {
                        return null;
                    }

                    if (iconPath.StartsWith("\"") && iconPath.EndsWith("\""))
                    {
                        iconPath = iconPath.Substring(1, iconPath.Length - 2);
                    }

                    string[] split = iconPath.Split(',');

                    int iconIndex = split.Length > 1 ? int.Parse(split[1]) : 0;
                    Icon icon = NativeHelpers.ExtractIcon(Environment.ExpandEnvironmentVariables(split[0]), iconIndex);
                    return icon;
                }
            }

            public bool Hide => string.IsNullOrWhiteSpace(DisplayName);
        }

        private void NameLabel_TextChanged(object sender, EventArgs e)
        {
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(uninstallButton.Enabled = listView.SelectedItems.Count > 0))
                return;

            if (listView.SelectedItems.Count == 1)
            {
                if (listView.SelectedItems[0].Tag is UninstallInfo info)
                {
                    nameLabel.Text = info.DisplayName;
                    versionLabel.Text = info.DisplayVersion;

                    if (info.Icon == null)
                    {
                        iconPictureBox.Image = Icons.Application.ToBitmap();
                    }
                    else
                    {
                        iconPictureBox.Image = largeImageList.Images[info.IconPath];
                    }
                }
            }
            else
            {
                nameLabel.Text = "Mutiple applications";
                versionLabel.Text = string.Empty;
            }

            versionLabel.Left = nameLabel.Left + nameLabel.Width;
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                using (var indicator = new ProgressIndicator())
                {
                    indicator.Initialize(listView.SelectedItems.Count);

                    for (var i = 0; i < listView.SelectedItems.Count; i++)
                    {
                        if (listView.SelectedItems[i].Tag is UninstallInfo info)
                        {
                            indicator.SetProgress(i, $"Uninstalling {info.DisplayName}");
                            Process.Start(info.UninstallString).WaitForExit();
                        }
                    }
                }
            }
        }
    }
}