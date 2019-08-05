using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    public abstract class Extension
    {
        public abstract string Name { get; }
        public virtual string Description { get; }
        public long Size => new FileInfo(this.GetType().Assembly.Location).Length;

        public virtual List<Type> GetTweakPages() => new List<Type>(0);
    }
}