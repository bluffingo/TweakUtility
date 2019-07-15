using System;
using System.Linq;
using System.Management;

namespace TweakUtility
{
    public static class OperatingSystemVersions
    {
        private static Version _currentVersion = null;

        private static readonly Version[] _versions = new[] {
            new Version(5, 1),
            new Version(5, 2),
            new Version(6, 0, 4074),
            new Version(6, 0),
            new Version(6, 1, 7000),
            new Version(6, 1),
            new Version(6, 2, 8102),
            new Version(6, 2, 8250),
            new Version(6, 2, 8400),
            new Version(6, 2),
            new Version(6, 3),
            new Version(6, 4),
            new Version(10, 0, 10074),
            new Version(10, 0)
        };

        public static Version GetVersion(OperatingSystemVersion version) => _versions[(int)version - 1];

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
    }

    public enum OperatingSystemVersion
    {
        None,
        WindowsXP,
        Windows2003,
        WindowsLonghorn4074,
        WindowsVista,
        Windows7beta,
        Windows7,
        Windows8Developer,
        Windows8Consumer,
        Windows8Release,
        Windows8,
        Windows81,
        WindowsTech, //Builds 9833 to 9883 of Windows 10 from 2014.
        Windows10Beta10074,
        Windows10
    }
}

//WARNING: Beta support is expriemental at the moment.