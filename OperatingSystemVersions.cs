using System;
using System.Linq;
using System.Management;

using TweakUtility.Attributes;

namespace TweakUtility
{
    public static class OperatingSystemVersions
    {
        private static Version _currentVersion = null;

        private static readonly Version[] _versions = new[] {
            new Version(5, 1),
            new Version(5, 2),
            new Version(6, 0),
            new Version(6, 1),
            new Version(6, 2),
            new Version(6, 3),
            new Version(6, 4),
            new Version(10, 0)
        };

        public static Version GetVersion(OperatingSystemVersion version)
        {
            if (version == OperatingSystemVersion.None)
            {
                return null;
            }

            return _versions[(int)version - 1];
        }

        public static Version GetCurrentVersion()
        {
            if (_currentVersion == null)
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    string[] v = ((string)searcher.Get().Cast<ManagementObject>().FirstOrDefault().Properties["Version"].Value).Split('.');
                    _currentVersion = new Version(int.Parse(v[0]), int.Parse(v[1]), int.Parse(v[2]));
                }
            }

            return _currentVersion;
        }

        /// <summary>
        /// Checks if the current operating system version matches the <see cref="OperatingSystemSupportedAttribute"/>
        /// </summary>
        /// <param name="mininum">The minimum supported operating system version.</param>
        /// <param name="maximum">The maximum supported operating system version.</param>
        /// <returns></returns>
        public static bool IsSupported(this OperatingSystemSupportedAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            return IsSupported(attribute.Mininum, attribute.Maximum);
        }

        /// <summary>
        /// Checks if the current operating system version matches the <paramref name="mininum"/> and <paramref name="maximum"/> version.
        /// </summary>
        /// <param name="mininum">The minimum supported operating system version.</param>
        /// <param name="maximum">The maximum supported operating system version.</param>
        /// <returns></returns>
        public static bool IsSupported(this Version mininum, Version maximum = null)
        {
            if (mininum is null)
            {
                throw new ArgumentNullException(nameof(mininum));
            }

            Version current = GetCurrentVersion();

            if (current < mininum)
            {
                return false;
            } //Check if current version is older than the minimum

            if (maximum != null && maximum < current)
            {
                return false;
            } //Check if there's a maximum version and if the current version is too new.

            return true;
        }
    }
}