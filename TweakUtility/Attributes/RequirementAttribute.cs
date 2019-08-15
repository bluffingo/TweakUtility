using System;

namespace TweakUtility.Attributes
{
    public abstract class RequirementAttribute : Attribute
    {
        public abstract bool Valid { get; }
    }
}