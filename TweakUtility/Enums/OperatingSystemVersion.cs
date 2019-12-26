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

        [DisplayName("Windows XP")]
        WindowsXP,

        [DisplayName("Windows Server 2003")]
        Windows2003,

        [DisplayName("Windows Longhorn (build 4047)")]
        WindowsLonghorn4074,

        [DisplayName("Windows Vista")]
        WindowsVista,

        [DisplayName("Windows 7 (Beta)")]
        Windows7Beta,

        [DisplayName("Windows 7")]
        Windows7,

        [DisplayName("Windows 8 (Developer Preview)")]
        Windows8Developer,

        [DisplayName("Windows 8 (Consumer Preview)")]
        Windows8Consumer,

        [DisplayName("Windows 8 (Release Preview)")]
        Windows8Release,

        [DisplayName("Windows 8")]
        Windows8,

        [DisplayName("Windows 8.1")]
        Windows81,

        [DisplayName("Windows 10 (beta build 10074)")]
        Windows10Beta10074,

        [DisplayName("Windows 10")]
        Windows10
    }
}