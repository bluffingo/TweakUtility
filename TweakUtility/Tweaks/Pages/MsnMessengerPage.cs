using Microsoft.Win32;

using System.Collections.Generic;
using System.Linq;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    [RegistryKeyRequired(@"HKCU\SOFTWARE\Microsoft\MSNMessenger")]
    internal partial class MsnMessengerPage : TweakPage
    {
        internal MsnMessengerPage() : base("MSN Messenger", GetPassportPages())
        {
            this.Icon = NativeHelpers.ExtractIcon(NativeHelpers.GetApplicationPath("msnmsgr.exe"), 0);
        }

        private static PassportPage[] GetPassportPages()
        {
            using (RegistryKey key = RegistryHelper.GetKey(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings"))
            {
                IEnumerable<string> names = key.GetSubKeyNames().Where(name => name != "0");
                var pages = new PassportPage[names.Count()];

                for (int i = 0; i < names.Count(); i++)
                {
                    string name = names.ElementAt(i);

                    if (name == "0")
                    {
                        continue;
                    }

                    pages[i] = new PassportPage(name);
                }

                return pages;
            }
        }

        [DisplayName("Show emoticons")]
        public bool ShowEmoticons
        {
            get => RegistryHelper.GetBoolValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\ShowEmoticons");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\ShowEmoticons", new byte[4] { (byte)(value ? 1 : 0), 0, 0, 0 });
        }

        [DisplayName("Show custom emoticons")]
        public bool ShowCustomEmoticons
        {
            get => RegistryHelper.GetBoolValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\ShowCustomEmoticons");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\ShowCustomEmoticons", new byte[4] { (byte)(value ? 1 : 0), 0, 0, 0 });
        }

        [DisplayName("Always on top")]
        public bool AlwaysOnTop
        {
            get => RegistryHelper.GetBoolValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\AlwaysOnTop");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\AlwaysOnTop", new byte[4] { (byte)(value ? 1 : 0), 0, 0, 0 });
        }

        [DisplayName("Make MSN Messenger use the newest IE version")]
        public bool IEOverwrite
        {
            get => RegistryHelper.KeyExists(@"HKCU\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
            set
            {
                if (value)
                {
                    RegistryHelper.SetValue(@"HKCU\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION\msnmsgr.exe", 12001);
                }
                else
                {
                    RegistryHelper.DeleteKey(@"HKCU\Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION");
                }
            }
        }

        [DisplayName("Intro shown count")]
        public int IntroShownCount
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\IntroShownCount");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\IntroShownCount", value);
        }

        [DisplayName("Server")]
        [Category("Legacy MSN Messenger (1.0-4.7)")]
        public string LegacyMSNServer
        {
            get => RegistryHelper.GetValue(@"HKCU\SOFTWARE\Microsoft\MessengerService\Server", "");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MessengerService\Server", value);
        }
    }
}