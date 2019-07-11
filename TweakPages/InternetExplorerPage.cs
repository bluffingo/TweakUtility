using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public class InternetExplorerPage : TweakPage
    {
        public InternetExplorerPage() : base("Internet Explorer", subPages: new TabsPage())
        {
        }

        [DisplayName("User Agent")]
        [DefaultValue("Mozilla/4.0 (compatible; MSIE 8.0; Win32)")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsXP)]
        public string UserAgent
        {
            get => RegistryHelper.GetValue<string>(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\User Agent");
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\User Agent", value);
        }

        public class TabsPage : TweakPage
        {
            public TabsPage() : base("Tabs")
            {
            }

            [DisplayName("Blank page")]
            [DefaultValue("res://mshtml.dll/blank.htm")]
            [OperatingSystemSupported(OperatingSystemVersion.WindowsXP)]
            public string BlankPage
            {
                get => RegistryHelper.GetValue<string>(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank");
                set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank", value);
            }
        }
    }
}