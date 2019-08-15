using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

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
                object[] attributes = propertyInfo.GetCustomAttributes(typeof(VisibleAttribute), false);

                if (attributes != null && attributes.Length == 1 && !((VisibleAttribute)attributes[0]).Visible)
                {
                    continue;
                }

                entries.Add(new TweakOption(this, propertyInfo));
            }

            foreach (MethodInfo methodInfo in this.GetMethods())
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(VisibleAttribute), false);

                if (attributes != null && attributes.Length == 1 && ((VisibleAttribute)attributes[0]).Visible)
                {
                    entries.Add(new TweakAction(this, methodInfo));
                }
            }

            this.Entries = new ReadOnlyCollection<TweakEntry>(entries);
        }

        [Visible(false)]
        public ReadOnlyCollection<TweakEntry> Entries { get; }

        [Visible(false)]
        public ReadOnlyCollection<TweakPage> SubPages { get; }

        /// <summary>
        /// The control that should be shown instead of the dynmatically generated <see cref="TweakPageView"/>
        /// </summary>
        [Visible(false)]
        public UserControl CustomView { get; set; }

        [Visible(false)]
        public string Name { get; }

        /// [12:08 AM] PF94: also silk is used by roblox, so we're using roblox icons D:
        /// [12:08 AM] Craftplacer: ignore the fact
        /// [12:08 AM] Craftplacer: we just don't play roblox
        /// [12:08 AM] Craftplacer: we never seen roblox
        /// [12:09 AM] Craftplacer: roblox never existed to us
        /// [12:09 AM] PF94: i used to, but now i give robux to some guy who wants to rejoin this "Soccer" group
        [Visible(false)]
        public object Icon { get; set; }

        public string ExportIcon()
        {
            string id = this.GetType().FullName;
            string path = Path.Combine(Helpers.Helpers.GetTemporaryDirectory(), id + ".ico");

            Debug.WriteLine($"Exporting icon for {id} in {path}...");

            using (FileStream fileStream = File.OpenWrite(path))
            {
                Image image = null;

                if (this.Icon is Icon icon)
                    image = icon.ToBitmap();
                else if (this.Icon is Image img)
                    image = img;
                else
                    image = Icons.Folder.ToBitmap();

                using (Icon tempIcon = Helpers.Helpers.PngIconFromImage(image))
                {
                    tempIcon.Save(fileStream);
                }
            }

            return path;
        }

        [Visible(false)]
        public bool RequirementsMet => Helpers.Helpers.RequirementsMet(this.GetType());

        public List<MethodInfo> GetMethods() => this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();

        public List<PropertyInfo> GetProperties() => this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).ToList();
    }
}