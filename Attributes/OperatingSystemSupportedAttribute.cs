using System;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class OperatingSystemSupportedAttribute : Attribute
    {
        public OperatingSystemSupportedAttribute(OperatingSystemVersion mininum, OperatingSystemVersion maximum = OperatingSystemVersion.None)
        {
            if (mininum == OperatingSystemVersion.None)
            {
                throw new ArgumentOutOfRangeException(nameof(mininum));
            }

            this.Mininum = OperatingSystemVersions.GetVersion(mininum);
            this.Maximum = OperatingSystemVersions.GetVersion(maximum);
        }

        public Version Mininum { get; }
        public Version Maximum { get; }
    }

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
        Windows7m3,
        Windows7Beta,
        Windows7,
        Windows8Developer,
        Windows8Consumer,
        Windows8Release,
        Windows8,
        Windows81,
        Windows10Tech, //Builds 9833 to 9883 of Windows 10 from 2014.
        Windows10Beta10074,
        Windows10
    }
}