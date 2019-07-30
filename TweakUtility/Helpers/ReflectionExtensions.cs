using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TweakUtility
{
    /// <summary>
    /// Hacking our way into .NET Framework!
    /// </summary>
    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this PropertyDescriptor descriptor) where T : Attribute => (T)descriptor.Attributes[typeof(T)];

        public static T GetAttributeReflection<T>(this object info) where T : Attribute
        {
            if (info is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetAttribute<T>();
            }
            else if (info is MethodInfo methodInfo)
            {
                return methodInfo.GetAttribute<T>();
            }
            else
            {
                throw new ArgumentException("not supported", nameof(info));
            }
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), false);

            return attributes.Length == 0 ? null : (T)attributes[0];
        }

        public static T GetAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        {
            object[] attributes = methodInfo.GetCustomAttributes(typeof(T), false);

            return attributes.Length == 0 ? null : (T)attributes[0];
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute => (T)type.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static T GetAttribute<T>(this object instance) where T : Attribute => instance.GetType().GetAttribute<T>();

        public static T GetAttribute<T>(this Enum @enum) where T : Attribute => (T)@enum.GetType().GetMember(@enum.ToString())[0].GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static object GetPropertyGridView(this PropertyGrid propertyGrid) => propertyGrid.GetField<object>("gridView");

        public static T GetField<T>(this object instance, string fieldName)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentException("message", nameof(fieldName));
            }

            return (T)instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(instance);
        }

        public static T GetProperty<T>(this object instance, string propertyName)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException("message", nameof(propertyName));
            }

            return (T)instance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(instance, null);
        }

        public static GridItemCollection GetAllGridEntryCollection(this object propertyGridView) => propertyGridView.GetField<GridItemCollection>("allGridEntries");

        public static GridItemCollection GetItems(this PropertyGrid propertyGrid) => propertyGrid.GetPropertyGridView().GetAllGridEntryCollection();
    }
}