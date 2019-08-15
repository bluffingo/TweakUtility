using System;
using System.Runtime.CompilerServices;

namespace TweakUtility.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description) => this.Description = description ?? throw new ArgumentNullException(nameof(description));

        public string Description { get; }
    }
}