using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TweakUtility
{
    public class TweakPage
    {
        public TweakPage(string name, params TweakPage[] subPages)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));

            if (subPages == null || subPages.Length == 0)
            {
                this.SubPages = new ReadOnlyCollection<TweakPage>(new List<TweakPage>(0));
            }
            else
            {
                this.SubPages = new ReadOnlyCollection<TweakPage>(subPages.ToList());
            }

            var entries = new List<TweakEntry>();
            foreach (PropertyInfo propertyInfo in this.GetProperties())
            {
                entries.Add(new TweakOption(this, propertyInfo));
            }
            foreach (MethodInfo methodInfo in this.GetMethods())
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(BrowsableAttribute), false);

                if (attributes != null && attributes.Length == 1 && ((BrowsableAttribute)attributes[0]).Browsable)
                {
                    entries.Add(new TweakAction(this, methodInfo));
                }
            }
            this.Entries = new ReadOnlyCollection<TweakEntry>(entries);
        }

        [Browsable(false)]
        public ReadOnlyCollection<TweakEntry> Entries { get; }

        [Browsable(false)]
        public ReadOnlyCollection<TweakPage> SubPages { get; }

        /// <summary>
        /// The control that should be shown instead of the dynmatically generated <see cref="TweakPageView"/>
        /// </summary>
        [Browsable(false)]
        public UserControl CustomView { get; set; }

        [Browsable(false)]
        public string Name { get; }

        /// [12:08 AM] PF94: also silk is used by roblox, so we're using roblox icons D:
        /// [12:08 AM] Craftplacer: ignore the fact
        /// [12:08 AM] Craftplacer: we just don't play roblox
        /// [12:08 AM] Craftplacer: we never seen roblox
        /// [12:09 AM] Craftplacer: roblox never existed to us
        /// [12:09 AM] PF94: i used to, but now i give robux to some guy who wants to rejoin this "Soccer" group
        [Browsable(false)]
        public object Icon { get; set; }

        public List<MethodInfo> GetMethods() => this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();

        public List<PropertyInfo> GetProperties() => this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();
    }
}