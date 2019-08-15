using System.Drawing;
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
    }
}