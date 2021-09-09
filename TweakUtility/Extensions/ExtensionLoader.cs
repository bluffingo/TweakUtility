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

        private IEnumerable<string> GetFiles() => Directory.GetFiles("extensions", "*.dll").Concat(Directory.GetFiles("extensions", "*.tuex"));

        public void LoadExtensions()
        {
            foreach (string path in this.GetFiles())
            {
                Debug.WriteLine("[Extension Loader] Loading DLL: " + path);
                byte[] data = File.ReadAllBytes(path);

                var assembly = Assembly.Load(data);
                this.LoadExtensions(assembly);
            }
        }

        internal Extension[] GetExtensions(Assembly assembly)
        {
            IEnumerable<Type> extensions = assembly.GetTypes().Where(t => t.BaseType == typeof(Extension));

            if (extensions.Count() == 0) //this assembly doesn't contain any extensions
            {
                GC.Collect(); // collects all unused memory
                GC.WaitForPendingFinalizers(); // wait until GC has finished its work
                GC.Collect();

                return null;
            }

            var array = new Extension[extensions.Count()];

            for (int i = 0; i < array.Length; i++)
            {
                var extension = extensions.ElementAt(i);
                object instance = Activator.CreateInstance(extension);

                Debug.Assert(instance is Extension);

                array[i] = instance as Extension;
            }

            return array;
        }

        private void LoadExtensions(Assembly assembly) => this.Extensions.AddRange(this.GetExtensions(assembly));
    }
}