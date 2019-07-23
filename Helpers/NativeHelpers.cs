using System;
using System.Drawing;
using System.Text;

using static TweakUtility.NativeMethods;

namespace TweakUtility.Helpers
{
    public static class NativeHelpers
    {
        public static Icon GetIconFromGroup(string file, int id)
        {
            file = Helpers.ResolvePath(file);
            ExtractIconEx(file, id, out _, out IntPtr small, 1);
            try
            {
                return Icon.FromHandle(small);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ExtractStringFromDLL(string file, int number)
        {
            IntPtr lib = LoadLibraryEx(file, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE | LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE);
            var result = new StringBuilder(2048);
            LoadString(lib, number, result, result.Capacity);
            FreeLibrary(lib);
            return result.ToString();
        }
    }
}