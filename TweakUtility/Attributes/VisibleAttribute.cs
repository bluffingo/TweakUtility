using System;

namespace TweakUtility.Attributes
{
    public class VisibleAttribute : RequirementAttribute
    {
        public VisibleAttribute(bool visible) => this.Visible = visible;

        public bool Visible { get; }

        public override bool Valid => Visible;
    }
}