using System;
using System.Collections.Generic;

namespace TweakUtility.Extensions
{
    public abstract class Extension
    {
        public abstract string Name { get; }
        public virtual string Description { get; }
        public abstract string Author { get; }

        public virtual List<Type> GetTweakPages() => new List<Type>(0);
    }
}