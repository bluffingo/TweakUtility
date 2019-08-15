using System;

using TweakUtility.Helpers;

namespace TweakUtility.Attributes
{
    public sealed class RegistryKeyRequiredAttribute : RequirementAttribute
    {
        public RegistryKeyRequiredAttribute(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"{nameof(path)} can't be null or whitespace.", nameof(path));

            this.Path = path;
        }

        public string Path { get; }
        public override bool Valid => RegistryHelper.KeyExists(this.Path);
    }
}