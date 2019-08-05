using System;
using System.Reflection;

namespace TweakUtility
{
    internal sealed class TweakOption : TweakEntry
    {
        public Type Type
        {
            get
            {
                if (reflectionInfo is PropertyInfo propertyInfo)
                {
                    return propertyInfo.PropertyType;
                }
                else if (reflectionInfo is FieldInfo fieldInfo)
                {
                    return fieldInfo.FieldType;
                }
                else
                {
                    throw new Exception("reflection info is invalid");
                }
            }
        }

        internal TweakOption(TweakPage tweakPage, PropertyInfo propertyInfo) : base(tweakPage, reflectionInfo: propertyInfo)
        {
        }

        internal TweakOption(TweakPage tweakPage, FieldInfo fieldInfo) : base(tweakPage, reflectionInfo: fieldInfo)
        {
        }

        public T GetValue<T>()
        {
            if (reflectionInfo is PropertyInfo propertyInfo)
            {
                return (T)propertyInfo.GetValue(this.parent, null);
            }
            else if (reflectionInfo is FieldInfo fieldInfo)
            {
                return (T)fieldInfo.GetValue(this.parent);
            }
            else
            {
                throw new Exception("reflection info is invalid");
            }
        }

        public void SetValue(object value)
        {
            if (reflectionInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(this.parent, value, null);
            }
            else if (reflectionInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(this.parent, value);
            }
            else
            {
                throw new Exception("reflection info is invalid");
            }
        }
    }
}