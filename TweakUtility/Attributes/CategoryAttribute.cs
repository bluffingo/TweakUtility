using System;

namespace TweakUtility.Attributes
{
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string category)
        {
            this.Category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public bool Localizable { get; set; } = false;
        public string Category { get; }
    }
}