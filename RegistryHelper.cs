using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TweakUtility
{
    public static class RegistryHelper
    {
        public static T GetValue<T>(string path)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                var value = subKey.GetValue(info.Item2, null);

                if (value == null)
                {
                    throw new Exception("Not found");
                }

                return (T)value;
            }
        }

        public static void SetValue(string path, object value, RegistryValueKind? valueKind = null)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                if (valueKind.HasValue)
                {
                    subKey.SetValue(info.Item2, value, valueKind.Value);
                }
                else
                {
                    subKey.SetValue(info.Item2, value);
                }
            }
        }

        private static string[] ProcessPath(string path)
        {
            var split = path.Split('\\').ToList();

            string hive = split[0]; split.RemoveAt(0);

            int nameIndex = split.Count - 1;
            string name = split[nameIndex]; split.RemoveAt(nameIndex);

            string joinedPath = string.Join("\\", split);

            return new string[3] { hive, joinedPath, string.IsNullOrWhiteSpace(name) ? null : name };
        }

        private static Tuple<RegistryKey, string> ProcessPathAdvanced(string path)
        {
            string[] info = ProcessPath(path);
            return Tuple.Create(GetHive(info[0]).CreateSubKey(info[1]), info[2]);
        }

        private static RegistryKey GetHive(string root)
        {
            switch (root.ToUpperInvariant())
            {
                default: throw new ArgumentOutOfRangeException(nameof(root));

                case "HKEY_LOCAL_MACHINE":
                case "HKLM":
                    return Program.LocalMachine;

                case "HKEY_CURRENT_USER":
                case "HKCU":
                    return Program.CurrentUser;
            }
        }
    }
}