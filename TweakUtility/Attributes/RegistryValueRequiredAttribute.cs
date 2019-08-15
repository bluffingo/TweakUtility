using System;
using TweakUtility.Helpers;

namespace TweakUtility.Attributes
{
    public class RegistryValueRequiredAttribute : RequirementAttribute
    {
        /// <summary>
        /// Checks if the registry value exists
        /// </summary>
        /// <param name="path">The path to the registry value</param>
        public RegistryValueRequiredAttribute(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"{nameof(path)} can't be null or whitespace.", nameof(path));

            this.Path = path;
        }

        /// <summary>
        /// Checks if the registry value has the value specified
        /// </summary>
        /// <param name="path">The path to the registry value</param>
        /// <param name="value">The value should be checked for</param>
        public RegistryValueRequiredAttribute(string path, object value) : this(path) => this.Value = value;

        public string Path { get; }

        public override bool Valid
        {
            get
            {
                //Checks if the registry path exists
                if (!RegistryHelper.ValueExists(this.Path))
                    return false;

                //Checks if the registry value has the value, if the value has been specified
                if (this.Value != null && RegistryHelper.GetValue<object>(this.Path) != this.Value)
                    return false;

                return true;
            }
        }

        public object Value { get; }
    }
}