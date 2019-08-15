using System;

namespace TweakUtility.Attributes
{
    public class VisibleAttribute : Attribute
    {
        public VisibleAttribute(bool visible) => this.Visible = visible;

        public bool Visible { get; }
    }
}