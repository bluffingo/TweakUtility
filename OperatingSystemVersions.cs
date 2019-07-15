using System;
using System.Linq;
using System.Management;

/* TweakUtility - Operating System Version Check (IMPORTANT NOTES)
WARNING: Beta support is expriemental at the moment.
Not every beta has custom registry values, Please remember that

However, Important betas should be still supported in case
such as Longhorn 4074, The 3 Windows 8 Previews, etc.

The obscure Windows operating systems based on XP/2003's codebase should not be included.
This means that any of the Embeddeds (including POSready), Fundemental for Legacy PCs and Home Server 
should not be implemented, As even if they have a different "build" number and System, WinVer still says it's 2600.
This also applies for Windows 7 (or Server 2008 R2)'s  MultiPoint Server 2010, Embedded 7, Home Server 2011 and Thin PC.

This should be only official Microsoft versions, Do not bloat this list with Custom Versions that uses
custom "build" numbers, such as Windows 2007 by Glosswired, or any other mods.

Written and updated by PF94, July 15th 2019,
*/

namespace TweakUtility
{
    public static class OperatingSystemVersions
    {
        private static Version _currentVersion = null;

        private static readonly Version[] _versions = new[] {
          //new Version(major, minor, build),
            new Version(5, 1),
            new Version(5, 2),
            new Version(6, 0, 4074),
            new Version(6, 0),
            new Version(6, 1, 6801),
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
      //NameOfOperatingSystem
        None,
        WindowsXP,
        Windows2003,
        WindowsLonghorn4074,
        WindowsVista,
        Windows7m3,
        Windows7beta,
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
