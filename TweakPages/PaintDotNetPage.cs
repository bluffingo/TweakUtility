using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TweakUtility.TweakPages
{
    public class PaintDotNetPage : TweakPage
    {
        public PaintDotNetPage(string name, params TweakPage[] subPages) : base("paint.net")
        {
            this.Icon = Properties.Resources.paintDotNet;
        }
    }
}