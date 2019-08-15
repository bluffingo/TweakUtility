using System;
using System.Collections.Generic;
using System.Linq;

namespace TweakUtility.Helpers
{
    /// <summary>
    /// Hacking our way into .NET Framework!
    /// </summary>
    internal static class ReflectionExtensions
    {
        public static IEnumerable<Attribute> GetAttributes(this Type type) => type.GetCustomAttributes(false).Cast<Attribute>();

        public static T GetAttribute<T>(this Type type) where T : Attribute => (T)type.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static T GetAttribute<T>(this Enum @enum) where T : Attribute => (T)@enum.GetType().GetMember(@enum.ToString())[0].GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));
    }
}