using System;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
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
    /// <remarks>Operating systems in this list have to be identifiable with their version, this does NOT mean you can add custom builds with the same version. Also listed versions in this list have to be compatible with this application (.NET Framework 4)</remarks>
    public enum OperatingSystemVersion
    {
        /// <summary>
        /// No operating system specified
        /// </summary>
        None,

        WindowsXP,
        Windows2003,
        WindowsVista,
        Windows7,
        Windows8,
        Windows81,

        /// <summary>
        /// Builds 9833 to 9883 of Windows 10 from 2014.
        /// </summary>
        WindowsTech,

        Windows10
    }
}