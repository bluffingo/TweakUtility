using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public class TweakPage
    {
        public TweakPage(string name, params TweakPage[] subPages)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            SubPages = subPages?.ToList();
        }

        [Browsable(false)]
        public List<TweakPage> SubPages = new List<TweakPage>();

        [Browsable(false)]
        public UserControl CustomView { get; set; }

        [Browsable(false)]
        public string Name { get; }
    }
}