using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
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
            ExtractIconEx(file, id, out _, out var small, 1);

            if (small == IntPtr.Zero)
                return null;

            return Icon.FromHandle(small);
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

        public static void AddIconToSubItem(this ListView listView, int row, int column, int iconIndex)
        {
            var lvi = new LV_ITEM
            {
                iItem = row,
                iSubItem = column,
                uiMask = LVIF_IMAGE,
                iImage = iconIndex
            };

            SendMessage(listView.Handle, LVM_SETITEM, 0, ref lvi);
        }

        public static void ShowSubItemIcons(this ListView listView, bool show)
        {
            int style = SendMessage(listView.Handle, LVM_GETEXTENDEDLISTVIEWSTYLE, 0, 0);

            if (show)
                style |= LVS_EX_SUBITEMIMAGES;
            else
                style &= ~LVS_EX_SUBITEMIMAGES;

            SendMessage(listView.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, 0, style);
        }
    }
}