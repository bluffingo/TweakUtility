//Buggy at the time of 1.1.00's release, last worked in 1.0.99 (aka earlier builds of 1.1.00's developement)

using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class MsnMessengerPage
    {
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

                    if (a == null)
                    {
                        return SystemColors.ControlText;
                    }
                    else
                    {
                        return Color.FromArgb(a[0], a[1], a[2]);
                    }
                }
                set => RegistryHelper.SetValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\IM Color", new byte[4] { value.R, value.G, value.B, 0 });
            }

            [DisplayName("Nudge")]
            [Category("Sounds")]
            public string BuzzSound => RegistryHelper.GetEncodedStringValue($@"HKCU\SOFTWARE\Microsoft\MSNMessenger\PerPassportSettings\{_passportId}\Sounds\MSNMSGR_Buzz\Path", Encoding.Unicode);

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

            [DisplayName("Add Live 2009 sounds")]
            [Category("Sounds")]
            public void EnableLiveSounds()
            {
                string path = new FileInfo(NativeHelpers.GetApplicationPath("msnmsgr.exe")).DirectoryName;
                //it just doesn't work like that.

                bool backupExists = File.Exists(Path.Combine(path, "newalert.wma.bak"));

                if (backupExists)
                {
                    if (MessageBox.Show(Properties.Strings.MsnMessenger_AudioRestore, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (string item in Directory.GetFiles(path, "*.wma.bak"))
                        {
                            File.Move(item, item.Substring(0, item.Length - 4));
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show(Properties.Strings.MsnMessenger_AudioBackup, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        foreach (string item in Directory.GetFiles(path, "*.wma"))
                        {
                            File.Move(item, item + ".bak");
                        }
                    }

                    using (var client = new WebClient())
                    {
                        foreach (var fileName in new[] { "newalert.wma", "newemail.wma", "nudge.wma", "online.wma", "outgoing.wma", "phone.wma", "type.wma", "vimdone.wma" })
                        {
                            string url = $"https://raw.githubusercontent.com/Craftplacer/TweakUtility/master/Optional/wlm2009sounds/{fileName}";

                            client.DownloadFile(url, Path.Combine(path, fileName));
                        }
                    }
                }
            }

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