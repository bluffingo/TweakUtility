using System;
using System.IO;

namespace TweakUtility.Attributes
{
    public sealed class FileRequiredAttribute : RequirementAttribute
    {
        public FileRequiredAttribute(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"{nameof(path)} can't be null or whitespace.", nameof(path));

            this.Path = path;
        }

        public override bool Valid => File.Exists(this.Path);

        public string Path { get; }
    }
}