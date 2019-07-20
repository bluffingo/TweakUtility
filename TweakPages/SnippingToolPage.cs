using System.ComponentModel;
using System.Drawing;

using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    public class SnippingToolPage : TweakPage
    {
        public SnippingToolPage() : base("Snipping Tool")
        {
        }

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