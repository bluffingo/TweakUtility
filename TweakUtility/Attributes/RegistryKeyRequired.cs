using System;

namespace TweakUtility.Attributes
{
    public sealed class RegistryKeyRequiredAttribute : Attribute
    {
        public string Path { get; }

        public RegistryKeyRequiredAttribute(string path) => Path = path ?? throw new ArgumentNullException(nameof(path));

        public bool Exists => RegistryHelper.KeyExists(Path);

        /// <summary>
        /// If <see cref="true"/>, the option/<see cref="TweakPages.TweakPage"/> will be hidden. If <see cref="false"/>, it will show up disabled.
        /// </summary>
        public bool Hide = true;
    }
}