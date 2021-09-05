using System;
using System.Diagnostics;
using System.Drawing;

using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class AdvancedPage
    {
        private class OEMInformationPage : TweakPage
        {
            public OEMInformationPage() : base("OEM Information")
            {
                this.Icon = Icons.Information;
            }

            public string Logo
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo");
                    }
                    else
                    {
                        return Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\OEMLogo.bmp");
                    }
                }

                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Logo", value);
                    }
                    else
                    {
                        using (var image = Image.FromFile(value))
                        {
                            image.Save(@"%SystemRoot%\system32\OEMLogo.bmp");
                        }
                    }
                }
            }

            public string Manufacturer
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer");
                    }
                    else
                    {
                        return NativeHelpers.IniReadValue("General", "Manufacturer", @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }

                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Manufacturer", value);
                    }
                    else
                    {
                        NativeHelpers.IniWriteValue("General", "Manufacturer", value, @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
            }

            public string Model
            {
                get
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        return RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model");
                    }
                    else
                    {
                        return NativeHelpers.IniReadValue("General", "Model", @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
                set
                {
                    if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                    {
                        RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\Model", value);
                    }
                    else
                    {
                        NativeHelpers.IniWriteValue("General", "Model", value, @"%SystemRoot%\System32\OEMInfo.ini");
                    }
                }
            }

            [OperatingSystemSupported(OperatingSystemVersion.Windows7)]
            [DisplayName("Support hours")]
            public string SupportHours
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportHours", value);
            }

            [OperatingSystemSupported(OperatingSystemVersion.Windows7)]
            [DisplayName("Support phone number")]
            public string SupportPhone
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportPhone", value);
            }

            [OperatingSystemSupported(OperatingSystemVersion.Windows7)]
            [DisplayName("Support URL")]
            public string SupportURL
            {
                get => RegistryHelper.GetValue<string>(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL");
                set => RegistryHelper.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation\SupportURL", value);
            }

            [Visible(true)]
            public void Preview()
            {
                if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.Windows7))
                {
                    Process.Start("control.exe", "system");
                }
                else
                {
                    Process.Start(new ProcessStartInfo("sysdm.cpl") { UseShellExecute = true });
                }
            }
        }
    }
}