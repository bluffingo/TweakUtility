using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TweakUtility.Attributes;

namespace TweakUtility.Helpers
{
    public static class Helpers
    {
        private static byte[] pngiconheader = new byte[] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        internal static Icon PngIconFromImage(Image img, int size = 16)
        {
            using (var bmp = new Bitmap(img, new Size(size, size)))
            {
                byte[] png;
                using (var fs = new MemoryStream())
                {
                    bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    fs.Position = 0;
                    png = fs.ToArray();
                }

                using (var fs = new MemoryStream())
                {
                    if (size >= 256) size = 0;
                    pngiconheader[6] = (byte)size;
                    pngiconheader[7] = (byte)size;
                    pngiconheader[14] = (byte)(png.Length & 255);
                    pngiconheader[15] = (byte)(png.Length / 256);
                    pngiconheader[18] = (byte)(pngiconheader.Length);

                    fs.Write(pngiconheader, 0, pngiconheader.Length);
                    fs.Write(png, 0, png.Length);
                    fs.Position = 0;
                    return new Icon(fs);
                }
            }
        }

        public static string ResolvePath(string path)
        {
            path = Environment.ExpandEnvironmentVariables(path);

            if (File.Exists(path))
            {
                return path;
            }

            string fullpath = Path.GetFullPath(path);
            if (File.Exists(fullpath))
            {
                return fullpath;
            }

            string[] paths = (Environment.GetEnvironmentVariable("PATH") + @";C:\Windows\sysnative\").Split(';');
            foreach (string variablePath in paths)
            {
                string combined = Path.Combine(variablePath, path);
                if (File.Exists(combined))
                {
                    return combined;
                }
            }

            return null;
        }

        internal static Guid GetApplicationGuid() => Guid.Parse(((GuidAttribute)typeof(Program).Assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value);

        /// <summary>
        /// Checks if all requirement attributes on a type are valid.
        /// </summary>
        internal static bool RequirementsMet(Type type) => !type.GetAttributes().Where(a => a is RequirementAttribute).Any(a => a is RequirementAttribute b && !b.Valid);

        /// <summary>
        /// Gets the location of Tweak Utility's temporary directory, creates it when it doesn't exist.
        /// </summary>
        /// <returns></returns>
        public static string GetTemporaryDirectory()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "TweakUtility");

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            return tempPath;
        }

        /// <summary>
        /// Finds a suitable registry view for this system architecture
        /// </summary>
        internal static RegistryView RegistryView => Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
    }
}