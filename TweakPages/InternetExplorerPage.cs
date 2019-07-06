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
                get
                {
                    using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\AboutURLs"))
                    {
                        return (string)subKey.GetValue("blank", "res://mshtml.dll/blank.htm");
                    }
                }
                set
                {
                    using (RegistryKey subKey = Program.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\AboutURLs"))
                    {
                        subKey.SetValue("blank", value, RegistryValueKind.String);
                    }
                }
            }
        }
    }
}