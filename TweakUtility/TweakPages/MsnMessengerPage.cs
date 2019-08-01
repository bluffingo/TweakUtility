using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using TweakUtility.Attributes;

namespace TweakUtility.TweakPages
{
    [RegistryKeyRequired(@"HKCU\SOFTWARE\Microsoft\MSNMessenger")]
    public class MsnMessengerPage : TweakPage
    {
        public MsnMessengerPage() : base("MSN Messenger", GetPassportPages())
        {
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

                    if (name == "0") //empty passport idk why
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

        [DisplayName("Fix IE Script error")]
        public bool ieScriptFix
        {
            //set => RegistryHelper.SetValue()
            get => RegistryHelper.GetBoolValue(@"HKCU\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_GPU_RENDERING\msnmsgr.exe");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_GPU_RENDERING\msnmsgr.exe", 1);
        }

        [DisplayName("Intro shown count")]
        public int IntroShownCount
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\IntroShownCount");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\MSNMessenger\IntroShownCount", value);
        }

        //@PF94, problem fixed :P
        private class PassportPage : TweakPage
        {
            private readonly string _passportId;

            public PassportPage(string passportId) : base("Passport for " + passportId) => _passportId = passportId;

            [DisplayName("Text color")]
            public Color IMColor
            {
                get
                {
                    byte[] a = RegistryHelper.GetValue<byte[]>($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\IM Color");
                    return Color.FromArgb(a[0], a[1], a[2]);
                }
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\IM Color", new byte[4] { value.R, value.G, value.B, 0 });
            }

            [DisplayName("Nudge")]
            [Category("Sounds")]
            public string BuzzSound //a early name for nudge?
=> RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_Buzz\Path", Encoding.Unicode);

            [DisplayName("Contact online")]
            [Category("Sounds")]
            public string ContactOnlineSound => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_ContactOnline\Path", Encoding.Unicode);

            [DisplayName("New alert")]
            [Category("Sounds")]
            public string NewAlert => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_NewAlert\Path", Encoding.Unicode);

            [DisplayName("New mail")]
            [Category("Sounds")]
            public string NewMail => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_NewMail\Path", Encoding.Unicode);

            [DisplayName("New message")]
            [Category("Sounds")]
            public string NewMessage => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_NewMessage\Path", Encoding.Unicode);

            [DisplayName("Phone ring")]
            [Category("Sounds")]
            public string PhoneRing => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_PhoneRing\Path", Encoding.Unicode);

            [DisplayName("Outgoing phone ring")]
            [Category("Sounds")]
            public string PhoneRingOutgoing => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_PhoneRingOutgoing\Path", Encoding.Unicode);

            [DisplayName("Voice message finished")]
            [Category("Sounds")]
            public string VoiceIMFinished => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_VoiceIMFinished\Path", Encoding.Unicode);

            [DisplayName("Expand \"you me conversation\" area")]
            public bool ExpandConvYouMeArea
            {
                get => RegistryHelper.GetBoolValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\ExpandConvYouMeArea");
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\ExpandConvYouMeArea", new byte[4] { (byte)(value ? 1 : 0), 0, 0, 0 });
            }

            [DisplayName("Show what I'm listening to")]
            public bool PSMMode
            {
                get => RegistryHelper.GetBoolValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\PSMMode");
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\PSMMode", value ? 1 : 0);
            }

            [DisplayName("Show Windows Live Today after I sign in to Messenger")]
            public bool DisableMSNToday
            {
                get => RegistryHelper.GetBoolValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\DisableMSNToday");
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\DisableMSNToday", new byte[4] { (byte)(value ? 1 : 0), 0, 0, 0 });
            }

            [DisplayName("Accept unsafe file types")]
            public bool DangerousFT
            {
                get => RegistryHelper.GetBoolValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\DangerousFT");
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\DangerousFT", value ? 1 : 0);
            }
        }
    }
}