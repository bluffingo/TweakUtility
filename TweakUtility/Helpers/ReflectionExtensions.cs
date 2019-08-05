using System;
using System.Linq;

namespace TweakUtility.Helpers
{
    /// <summary>
    /// Hacking our way into .NET Framework!
    /// </summary>
    internal static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute => (T)type.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));

        public static T GetAttribute<T>(this Enum @enum) where T : Attribute => (T)@enum.GetType().GetMember(@enum.ToString())[0].GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(T));
    }
}