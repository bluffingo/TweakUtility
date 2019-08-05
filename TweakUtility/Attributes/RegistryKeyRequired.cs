using System;

using TweakUtility.Helpers;

namespace TweakUtility.Attributes
{
    public sealed class RegistryKeyRequiredAttribute : Attribute
    {
        public string Path { get; }

        public RegistryKeyRequiredAttribute(string path) => Path = path ?? throw new ArgumentNullException(nameof(path));

        public bool Exists => RegistryHelper.KeyExists(Path);
    }
}