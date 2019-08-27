using System;
using TweakUtility.Enums;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class NoticeAttribute : Attribute
    {
        public NoticeAttribute(NoticeType type, string text)
        {
            this.Type = type;
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public NoticeType Type { get; }
        public string Text { get; }
    }
}