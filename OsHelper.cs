using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweakUtility
{
    public static class OsHelper
    {
        public static readonly Version WindowsXP = new Version(5, 1);
        public static readonly Version Windows2003 = new Version(5, 2);
        public static readonly Version WindowsVista = new Version(6, 0);
        public static readonly Version Windows7 = new Version(6, 1);
        public static readonly Version Windows8 = new Version(6, 2);
        public static readonly Version Windows81 = new Version(6, 3);
        public static readonly Version Windows10 = new Version(10, 0);

        public static bool IsSupported(Version mininum, Version maximum = null)
        {
            if (mininum is null)
            {
                throw new ArgumentNullException(nameof(mininum));
            }

            Version current = Environment.OSVersion.Version;

            if (current < mininum)
            {
                return false;
            }

            if (maximum != null && maximum < current)
            {
                return false;
            }

            return true;
        }
    }
}