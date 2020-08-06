using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TweakUtility.Helpers
{
    /// <summary>
    /// Class containing methods for easy access and interaction with the Windows registry.
    /// </summary>
    public static class RegistryHelper
    {
        public static RegistryKey LocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryHelper.RegistryView);
        public static RegistryKey CurrentUser = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryHelper.RegistryView);

        public static bool GetBoolValue(string path)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                object value = subKey.GetValue(info.Item2, null);

                if (value is int integer)
                {
                    return integer == 1;
                }
                else if (value is byte[] array)
                {
                    return array[0] == 0x01;
                }
                else
                {
                    Debug.WriteLine($"defaulting to false, in getboolvalue, please look into ({Environment.StackTrace})");
                    return false;
                }
            }
        }

        public static string GetEncodedStringValue(string path, Encoding encoding)
        {
            byte[] a = GetValue<byte[]>(path);
            return encoding.GetString(a, 0, a.Length - (encoding == Encoding.Unicode ? 2 : 0));
        }

        /// <summary>
        /// Writes a string as byte array into the registry with the specified <paramref name="encoding"/>.
        /// </summary>
        public static void SetEncodedStringValue(string path, string value, Encoding encoding) => SetValue(path, encoding.GetBytes(value));

        public static T GetValue<T>(string path)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                object value = subKey.GetValue(info.Item2, null);

                if (value == null)
                    return default;

                return (T)value;
            }
        }

        public static T GetValue<T>(string path, T @default)
        {
            try
            {
                return GetValue<T>(path);
            }
            catch (NullReferenceException)
            {
                return @default;
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

        public static void DeleteValue(string path, bool throwOnMissingValue = true)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                subKey.DeleteValue(info.Item2, throwOnMissingValue);
            }
        }

        public static void DeleteKey(string path, bool throwOnMissingKey = true)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                subKey.DeleteSubKeyTree(info.Item2, throwOnMissingKey);
            }
        }

        public static RegistryKey GetKey(string path, bool writable = true)
        {
            Tuple<RegistryKey, string> info = ProcessPathAdvanced(path);

            using (RegistryKey subKey = info.Item1)
            {
                return subKey.OpenSubKey(info.Item2, writable);
            }
        }

        public static bool KeyExists(string path)
        {
            var info = ProcessPath(path);

            using (RegistryKey subKey = GetHive(info[0]).OpenSubKey(info[1], false))
            {
                if (subKey == null)
                {
                    return false;
                }

                var names = subKey.GetSubKeyNames();

                return names.Contains(info[2]);
            }
        }

        public static bool ValueExists(string path)
        {
            var info = ProcessPath(path);

            using (RegistryKey subKey = GetHive(info[0]).OpenSubKey(info[1], false))
            {
                return subKey.GetValue(info[2]) != null;
            }
        }

        private static string[] ProcessPath(string path)
        {
            var split = path.Split('\\').ToList();

            Debug.Assert(split[0] != "Computer", $"Please remove the Windows 10 REGEDIT prefix. ({path})");

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
                    return LocalMachine;

                case "HKEY_CURRENT_USER":
                case "HKCU":
                    return CurrentUser;
            }
        }

        /// <summary>
        /// Finds a suitable registry view for this system architecture
        /// </summary>
        internal static RegistryView RegistryView => Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
    }
}