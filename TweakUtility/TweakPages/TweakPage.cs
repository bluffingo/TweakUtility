using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public class TweakPage
    {
        public TweakPage(string name, params TweakPage[] subPages)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.SubPages = subPages?.ToList();
        }

        [Browsable(false)]
        public List<TweakPage> SubPages { get; } = new List<TweakPage>();

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
    }
}