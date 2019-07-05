﻿using System;
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
            new Version(6, 0),
            new Version(6, 1),
            new Version(6, 2),
            new Version(6, 3),
            new Version(6, 4),
            new Version(10, 0)
        };

        public static Version GetVersion(OperatingSystemVersion version) => _versions[(int)version - 1];

        public static Version GetCurrentVersion()
        {
            if (_currentVersion == null)
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    string[] v1 = ((string)searcher.Get().Cast<ManagementObject>().FirstOrDefault().Properties["Version"].Value).Split('.');
                    _currentVersion = new Version(int.Parse(v1[0]), int.Parse(v1[1]));
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
        WindowsVista,
        Windows7,
        Windows8,
        Windows81,
        WindowsTechnical,
        Windows10
    }
}