using System;

namespace TweakUtility.Attributes
{
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string displayName) => this.DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));

        public string DisplayName { get; }
    }
}