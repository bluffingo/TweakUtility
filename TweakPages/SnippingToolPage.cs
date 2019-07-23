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
            this.Icon = Properties.Resources.snippingTool;
        }

        [DisplayName("Hide instructions")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
        public bool HideInstructions
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions") == 0;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions", value ? 0 : 1);
        }

        [Category("Custom Pen")]
        [DisplayName("Tip")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
        public CustomPenTip Tip
        {
            get => (CustomPenTip)RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenTip");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenTip", (int)value);
        }

        [Category("Custom Pen")]
        [DisplayName("Color")]
        [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
        public Color Color
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenColor").ToBgrColor();
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenColor", value.ToBgrInt());
        }

        public enum CustomPenTip
        {
            [Description("Round tip pen")]
            Round = 0,

            [Description("Chisel tip pen")]
            Chisel = 1,
        }
    }
}