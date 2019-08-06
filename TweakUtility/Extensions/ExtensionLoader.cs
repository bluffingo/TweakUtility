using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TweakUtility.Extensions
{
    internal class ExtensionLoader
    {
        public List<Extension> Extensions { get; } = new List<Extension>();

        private string[] GetFiles() => Directory.GetFiles("extensions", "*.dll");

        public void LoadExtensions()
        {
            foreach (string path in this.GetFiles())
            {
                Debug.WriteLine("[EL] Loading DLL: " + path);
                byte[] data = File.ReadAllBytes(path);

                var assembly = Assembly.Load(data);
                this.LoadExtension(assembly);
            }
        }

        private void LoadExtension(Assembly assembly)
        {
            var extensions = assembly.GetTypes().Where(t => t.BaseType == typeof(Extension));

            if (extensions.Count() == 0) //this assembly doesn't contain any extensions
            {
                GC.Collect(); // collects all unused memory
                GC.WaitForPendingFinalizers(); // wait until GC has finished its work
                GC.Collect();
                return;
            }

            foreach (Type extension in extensions)
            {
                object instance = Activator.CreateInstance(extension);

                Debug.Assert(instance is Extension);

                this.Extensions.Add(instance as Extension);
            }
        }
    }
}