using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility
{
    /// <summary>
    /// Hacking our way into .NET Framework!
    /// </summary>
    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this PropertyDescriptor descriptor) where T : Attribute => (T)descriptor.Attributes[typeof(T)];

        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), false);

            return attributes.Length == 0 ? null : (T)attributes[0];
        }

        public static T GetAttribute<T>(this object instance) where T : Attribute => (T)instance.GetType().GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static T GetAttribute<T>(this Enum @enum) where T : Attribute => (T)@enum.GetType().GetMember(@enum.ToString())[0].GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static object GetPropertyGridView(this PropertyGrid propertyGrid) => propertyGrid.GetField<object>("gridView");

        public static T GetField<T>(this object instance, string fieldName) => (T)instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(instance);

        public static T GetProperty<T>(this object instance, string propertyName) => (T)instance.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(instance, null);

        public static GridItemCollection GetAllGridEntryCollection(this object propertyGridView) => propertyGridView.GetField<GridItemCollection>("allGridEntries");

        public static GridItemCollection GetItems(this PropertyGrid propertyGrid) => propertyGrid.GetPropertyGridView().GetAllGridEntryCollection();
    }
}