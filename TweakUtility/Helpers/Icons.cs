using System.Drawing;

using TweakUtility.Attributes;
using TweakUtility.Enums;
using static TweakUtility.Helpers.NativeHelpers;

namespace TweakUtility.Helpers
{
    public static class Icons
    {
        public static readonly Icon RecentDocuments = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -21);
        public static readonly Icon SystemFile = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -154);
        public static readonly Icon Folder = ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -4);
        public static readonly Icon File = ExtractIcon(@"%SystemRoot%\System32\shell32.dll", 0);
        public static readonly Icon Go = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -290);
        public static readonly Icon Options = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -274);
        public static readonly Icon InternetArrow = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -244);
        public static readonly Icon Notepad = ExtractIcon(@"%SystemRoot%\System32\notepad.exe", -0);
        public static readonly Icon Bulb = Properties.Resources.Lightbulb_16x;
        public static readonly Icon Information = getInformationIcon();
        public static readonly Icon Warning = getWarningIcon();
        public static readonly Icon Software = ExtractIcon(@"%SystemRoot%\system32\shell32.dll", -271);

        private static Icon getInformationIcon()
        {
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                return ExtractIcon(@"%SystemRoot%\System32\imageres.dll", -81);
            }
            else
            {
                return ExtractIcon(@"%SystemRoot%\System32\user32.dll", -104);
            }
        }

        private static Icon getWarningIcon()
        {
            if (OperatingSystemVersions.IsSupported(OperatingSystemVersion.WindowsVista))
            {
                return ExtractIcon(@"%SystemRoot%\System32\imageres.dll", -84);
            }
            else
            {
                return ExtractIcon(@"%SystemRoot%\System32\user32.dll", -101);
            }
        }
    }
}