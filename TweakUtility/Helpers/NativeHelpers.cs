using System;
using System.Drawing;
using System.IO;
using System.Text;

using static TweakUtility.Helpers.NativeMethods;

namespace TweakUtility.Helpers
{
    internal static class NativeHelpers
    {
        public static Bitmap ExtractImage(string file, int id)
        {
            file = Helpers.ResolvePath(file);

            IntPtr hModule = LoadLibraryEx(file, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE | LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
            IntPtr hResource = FindResource(hModule, id, "IMAGE");
            IntPtr hBitmap = LoadResource(hModule, hResource);
            var bitmap = Bitmap.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);
            return bitmap;
        }

        public static Icon ExtractIcon(string file, int id)
        {
            file = Helpers.ResolvePath(file);
            ExtractIconEx(file, id, out _, out IntPtr small, 1);
            try
            {
                return Icon.FromHandle(small);
            }
            catch
            {
                return null;
            }
        }

        public static string ExtractString(string file, int id)
        {
            IntPtr lib = LoadLibraryEx(file, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE | LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
            var result = new StringBuilder(2048);
            LoadString(lib, id, result, result.Capacity);
            FreeLibrary(lib);
            return result.ToString();
        }

        public static void IniWriteValue(string section, string key, string value, string path)
        {
            path = Environment.ExpandEnvironmentVariables(path);

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            WritePrivateProfileString(section, key, value, path);
        }

        public static string IniReadValue(string section, string key, string path)
        {
            path = Helpers.ResolvePath(path);

            var stringBuilder = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", stringBuilder, stringBuilder.Capacity, path);
            return stringBuilder.ToString();
        }

        public static string GetApplicationPath(string executableName) => RegistryHelper.GetValue<string>($@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\{executableName}\");
    }
}