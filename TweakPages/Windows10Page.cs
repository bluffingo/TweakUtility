using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public class Windows10Page : TweakPage
    {
        public Windows10Page() : base("Windows 10") => this.CustomView = new TweakPageView(this);
    }
}