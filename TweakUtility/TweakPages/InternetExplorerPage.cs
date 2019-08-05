using System.ComponentModel;
using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    internal class InternetExplorerPage : TweakPage
    {
        internal InternetExplorerPage() : base("Internet Explorer") => this.Icon = NativeHelpers.ExtractIcon(NativeHelpers.GetApplicationPath("iexplore.exe"), -0);

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