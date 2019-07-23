using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    public class InternetExplorerPage : TweakPage
    {
        public InternetExplorerPage() : base("Internet Explorer", subPages: new TabsPage())
        {
            this.Icon = Properties.Resources.iexplore;
        }

        [DisplayName("User Agent")]
        [DefaultValue("Mozilla/4.0 (compatible; MSIE 8.0; Win32)")]
        /*
        >is IE11 user
        >defaults user agent with tweakutility
        >every site detects browser as if it's IE8
        >sites look fucked and out of date
        */
        [OperatingSystemSupported(OperatingSystemVersion.WindowsXP, OperatingSystemVersion.WindowsVista)]
        public string UserAgent
        {
            get => RegistryHelper.GetValue<string>(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\User Agent");
            set => RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\User Agent", value);
        }

        public class TabsPage : TweakPage
        {
            public TabsPage() : base("Tabs")
            {
                this.Icon = Properties.Resources.iexplore_page;
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