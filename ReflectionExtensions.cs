using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TweakUtility
{
    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this PropertyDescriptor descriptor) where T : Attribute => (T)descriptor.Attributes[typeof(T)];

        public static object GetHiddenValue(this Attribute attribute, string fieldName) => attribute.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(attribute);

        public static T GetHiddenValue<T>(this Attribute attribute, string fieldName) => (T)GetHiddenValue(attribute, fieldName);

        public static void SetHiddenValue(this Attribute attribute, string fieldName, object value) => attribute.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(attribute, value);
    }
}