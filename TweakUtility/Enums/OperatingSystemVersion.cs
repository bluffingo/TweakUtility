using TweakUtility.Attributes;

namespace TweakUtility.Enums
{
    /// <summary>
    /// Collection of supported operating systems.
    /// </summary>
    /// <remarks>Operating systems in this list have to be identifiable with their version, this does NOT mean you can add custom builds with the same version. Also, listed versions in this list have to be compatible with this application (.NET Framework 4)</remarks>
    public enum OperatingSystemVersion
    {
        None,

        [DisplayName("Windows 7")]
        Windows7,

        [DisplayName("Windows 8")]
        Windows8,

        [DisplayName("Windows 8.1")]
        Windows81,

        [DisplayName("Windows 10")]
        Windows10,

        [DisplayName("Windows 11 Cobalt")]
        Windows11Cobalt
    }
}