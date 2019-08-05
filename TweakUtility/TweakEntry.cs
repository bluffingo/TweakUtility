using System;
using System.ComponentModel;
using System.Reflection;
using TweakUtility.Attributes;

namespace TweakUtility
{
    public abstract class TweakEntry
    {
        public string Name
        {
            get
            {
                if (reflectionInfo is PropertyInfo propertyInfo)
                {
                    var attribute = GetAttribute<DisplayNameAttribute>();

                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.DisplayName))
                    {
                        return attribute.DisplayName;
                    }

                    return propertyInfo.Name;
                }
                else if (reflectionInfo is MethodInfo methodInfo)
                {
                    var attribute = GetAttribute<DisplayNameAttribute>();

                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.DisplayName))
                    {
                        return attribute.DisplayName;
                    }

                    return methodInfo.Name;
                }
                else if (reflectionInfo is FieldInfo fieldInfo)
                {
                    var attribute = GetAttribute<DisplayNameAttribute>();

                    if (attribute != null && !string.IsNullOrWhiteSpace(attribute.DisplayName))
                    {
                        return attribute.DisplayName;
                    }

                    return fieldInfo.Name;
                }
                else
                {
                    throw new Exception("invalid reflection info");
                }
            }
        }

        public bool Visible
        {
            get
            {
                var browsableAttribute = GetAttribute<BrowsableAttribute>();
                if (browsableAttribute != null && !browsableAttribute.Browsable)
                {
                    return false;
                }

                var supportedAttribute = GetAttribute<OperatingSystemSupportedAttribute>();
                if (supportedAttribute != null && !supportedAttribute.IsSupported)
                {
                    return false;
                }

                return true;
            }
        }

        public bool CanWrite => reflectionInfo is PropertyInfo propertyInfo ? propertyInfo.CanWrite : true;

        public bool CanRead => reflectionInfo is PropertyInfo propertyInfo ? propertyInfo.CanRead : true;

        public object reflectionInfo;
        public TweakPage parent;

        internal TweakEntry(TweakPage tweakPage, object reflectionInfo)
        {
            this.parent = tweakPage;
            this.reflectionInfo = reflectionInfo;
        }

        public T GetAttribute<T>() where T : Attribute
        {
            if (reflectionInfo is PropertyInfo propertyInfo)
            {
                object[] attributes = propertyInfo.GetCustomAttributes(typeof(T), false);
                return attributes.Length == 0 ? null : (T)attributes[0];
            }
            else if (reflectionInfo is MethodInfo methodInfo)
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(T), false);
                return attributes.Length == 0 ? null : (T)attributes[0];
            }
            else
            {
                throw new Exception("invalid reflection info");
            }
        }
    }
}