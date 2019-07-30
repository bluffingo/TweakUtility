using System;
using System.Drawing;
using System.Text;

using static TweakUtility.NativeMethods;

namespace TweakUtility.Helpers
{
    public static class NativeHelpers
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
    }
}