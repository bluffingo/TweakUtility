using System;
using System.Reflection;
using TweakUtility.Attributes;
using TweakUtility.Exceptions;

namespace TweakUtility
{
    public abstract class TweakEntry
    {
        /// <summary>The display name of this tweak entry.</summary>
        public string Name
        {
            get
            {
                string localizedName = Properties.Strings.ResourceManager.GetString(this.Id);
                if (!string.IsNullOrWhiteSpace(localizedName))
                    return localizedName;

                DisplayNameAttribute attribute = this.GetAttribute<DisplayNameAttribute>();
                if (!string.IsNullOrWhiteSpace(attribute?.DisplayName))
                    return attribute.DisplayName;

                return this.InternalName;
            }
        }

        public string InternalName
        {
            get
            {
                if (reflectionInfo is PropertyInfo propertyInfo)
                {
                    return propertyInfo.Name;
                }

                if (reflectionInfo is MethodInfo methodInfo)
                {
                    return methodInfo.Name;
                }

                if (reflectionInfo is FieldInfo fieldInfo)
                {
                    return fieldInfo.Name;
                }

                throw new InvalidReflectionInfoException();
            }
        }

        public bool Visible
        {
            get
            {
                var browsableAttribute = this.GetAttribute<VisibleAttribute>();
                if (browsableAttribute != null && !browsableAttribute.Visible)
                {
                    return false;
                }

                if (!this.RequirementsMet)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// A tweak entry id is a compound between the page type name and the entry's reflection info name, used to find it later on again.
        /// </summary>
        public string Id => parent.GetType().Name + "#" + this.InternalName;

        public bool CanWrite => reflectionInfo is PropertyInfo propertyInfo ? propertyInfo.CanWrite : true;

        public bool CanRead => reflectionInfo is PropertyInfo propertyInfo ? propertyInfo.CanRead : true;

        public bool RequirementsMet => Helpers.Helpers.RequirementsMet(this.GetType());

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
                throw new InvalidReflectionInfoException();
            }
        }
    }
}