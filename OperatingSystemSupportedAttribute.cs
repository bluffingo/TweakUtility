using System;

namespace TweakUtility
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class OperatingSystemSupportedAttribute : Attribute
    {
        public OperatingSystemSupportedAttribute(OperatingSystemVersion mininum, OperatingSystemVersion maximum = OperatingSystemVersion.None)
        {
            if (mininum == OperatingSystemVersion.None)
            {
                throw new ArgumentOutOfRangeException(nameof(mininum));
            }

            this.Mininum = mininum;
            this.Maximum = maximum;
        }

        public OperatingSystemVersion Mininum { get; }
        public OperatingSystemVersion Maximum { get; }
    }
}