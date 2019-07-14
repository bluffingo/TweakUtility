using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TweakUtility.TweakPages
{
    public class SnippingToolPage : TweakPage
    {
        public SnippingToolPage() : base("Snipping Tool") => this.CustomView = new TweakPageView(this);

        [Category("Custom Pen")]
        [DisplayName("Color")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
        public Color CustomPenColor
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenColor").ToBgrColor();
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenColor", value.ToBgrInt());
        }
    }
}