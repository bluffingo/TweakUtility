using System;

using TweakUtility.Enums;
using TweakUtility.Helpers;

using static TweakUtility.Helpers.OperatingSystemVersions;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class OperatingSystemSupportedAttribute : RequirementAttribute
    {
        public OperatingSystemSupportedAttribute(OperatingSystemVersion mininum, OperatingSystemVersion maximum = OperatingSystemVersion.None)
        {
            if (mininum == OperatingSystemVersion.None)
            {
                throw new ArgumentOutOfRangeException(nameof(mininum));
            }

            this.Mininum = GetVersion(mininum);
            this.Maximum = GetVersion(maximum);
        }

        public Version Mininum { get; }
        public Version Maximum { get; }

        public override bool Valid => OperatingSystemVersions.IsSupported(this.Mininum, this.Maximum);
    }
}