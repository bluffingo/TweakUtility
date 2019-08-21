namespace TweakUtility.Enums
{
    /// <summary>
    /// Collection of supported operating systems.
    /// </summary>
    /// <remarks>Operating systems in this list have to be identifiable with their version, this does NOT mean you can add custom builds with the same version. Also, listed versions in this list have to be compatible with this application (.NET Framework 4)</remarks>
    public enum OperatingSystemVersion
    {
        None,

        WindowsXP,
        Windows2003,
        WindowsLonghorn4074,
        WindowsVista,
        Windows7Beta,
        Windows7,
        Windows8Developer,
        Windows8Consumer,
        Windows8Release,
        Windows8,
        Windows81,
        Windows10Beta10074,
        Windows10
    }
}