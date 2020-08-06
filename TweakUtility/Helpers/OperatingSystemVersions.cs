using System;
using System.Linq;
using System.Management;
using TweakUtility.Enums;
using TweakUtility.Tweaks.Pages;

namespace TweakUtility.Helpers
{
    internal static class OperatingSystemVersions
    {
        private static Version _currentVersion = null;

        #region Version Numbers

        private static readonly Version[] _versions = new[] {
          //new Version(major, minor, build)
            new Version(5, 1),
            new Version(5, 2),
            new Version(6, 0),
            new Version(6, 1, 7000),
            new Version(6, 1),
            new Version(6, 2, 8102),
            new Version(6, 2, 8250),
            new Version(6, 2, 8400),
            new Version(6, 2),
            new Version(6, 3),
            new Version(10, 0)
        };

        #endregion Version Numbers

        public static Version GetVersion(OperatingSystemVersion version) => version == OperatingSystemVersion.None ? null : _versions[(int)version - 1];

        public static Version CurrentVersion
        {
            get
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
        }

        public static bool IsSupported(OperatingSystemVersion mininum, OperatingSystemVersion maximum = OperatingSystemVersion.None) => IsSupported(GetVersion(mininum), GetVersion(maximum));

        public static bool IsSupportedCosmetic(OperatingSystemVersion mininum, OperatingSystemVersion maximum = OperatingSystemVersion.None) => IsSupported(GetVersion(mininum), PreferencesPage.GetCosmeticVersion(), GetVersion(maximum));

        /// <summary>
        /// Checks if the current operating system version matches the <paramref name="mininum"/> and <paramref name="maximum"/> version.
        /// </summary>
        /// <param name="mininum">The minimum supported operating system version.</param>
        /// <param name="maximum">The maximum supported operating system version.</param>
        /// <returns></returns>
        public static bool IsSupported(this Version mininum, Version maximum = null) => IsSupported(mininum, CurrentVersion, maximum);

        private static bool IsSupported(this Version mininum, Version current, Version maximum = null)
        {
            if (mininum is null)
            {
                throw new ArgumentNullException(nameof(mininum));
            }

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