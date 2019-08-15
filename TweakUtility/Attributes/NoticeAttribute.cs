using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class NoticeAttribute : Attribute
    {
        public NoticeAttribute(NoticeAttributeType type, string text)
        {
            this.Type = type;
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public NoticeAttributeType Type { get; }
        public string Text { get; }
    }

    public enum NoticeAttributeType
    {
        Info,
        Tip,
        Warning
    }
}