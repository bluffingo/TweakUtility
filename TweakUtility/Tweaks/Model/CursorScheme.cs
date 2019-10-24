using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Model
{
    public class CursorScheme
    {
        public string AppStarting { get; set; }
        public string Arrow { get; set; }
        public string Hand { get; set; }
        public string Help { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public string NWPen { get; set; }
        public string SizeAll { get; set; }
        public string SizeWE { get; set; }
        public string SizeNESW { get; set; }
        public string SizeNS { get; set; }
        public string SizeNWSE { get; set; }
        public string UpArrow { get; set; }
        public string Wait { get; set; }

        public static string GetCurrentCursorSchemeName() => RegistryHelper.GetValue<string>(@"HKCU\Control Panel\Cursors\");

        public static CursorScheme GetCurrentCursorScheme()
        {
            using (var key = RegistryHelper.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Control Panel\Cursors\Schemes", false))
            {
                return new CursorScheme()
                {
                    Name = (string)key.GetValue(null),
                    Arrow = (string)key.GetValue("Arrow"),
                    Help = (string)key.GetValue("Help"),
                    AppStarting = (string)key.GetValue("AppStarting"),
                    Wait = (string)key.GetValue("Wait"),
                    NWPen = (string)key.GetValue("NWPen"),
                    No = (string)key.GetValue("No"),
                    SizeNS = (string)key.GetValue("SizeNS"),
                    SizeWE = (string)key.GetValue("SizeWE"),
                    SizeNWSE = (string)key.GetValue("SizeNWSE"),
                    SizeNESW = (string)key.GetValue("SizeNESW"),
                    SizeAll = (string)key.GetValue("SizeAll"),
                    UpArrow = (string)key.GetValue("UpArrow"),
                    Hand = (string)key.GetValue("Hand")
                };
            }
        }

        public static CursorScheme[] GetAvailableCursorSchemes()
        {
            using (var key = RegistryHelper.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Control Panel\Cursors\Schemes", false))
            {
                var schemeNames = key.GetValueNames();
                var schemes = new CursorScheme[schemeNames.Length];

                for (int i = 0; i < schemes.Length; i++)
                {
                    var name = schemeNames[i];
                    var value = (string)key.GetValue(name);
                    var values = value.Split(',');

                    var scheme = new CursorScheme()
                    {
                        Name = name,
                        Arrow = values[0],
                        Help = values[1],
                        AppStarting = values[2],
                        Wait = values[3],
                        // = values[4],
                        // = values[5],
                        NWPen = values[6],
                        No = values[7],
                        SizeNS = values[8],
                        SizeWE = values[9],
                        SizeNWSE = values[10],
                        SizeNESW = values[11],
                        SizeAll = values[12],
                        UpArrow = values[13],
                        Hand = values[14]
                    };

                    schemes[i] = scheme;
                }

                return schemes;
            }
        }

        public override string ToString() => this.Name;
    }
}