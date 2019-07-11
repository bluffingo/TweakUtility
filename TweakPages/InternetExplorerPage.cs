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
                get => RegistryHelper.GetValue<string>(@"HKLC\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank");
                set => RegistryHelper.SetValue(@"HKLC\SOFTWARE\Microsoft\Internet Explorer\AboutURLs\blank", value);
            }
        }
    }
}