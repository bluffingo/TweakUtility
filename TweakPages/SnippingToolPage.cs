using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using TweakUtility.Attributes;
using TweakUtility.Helpers;

namespace TweakUtility.TweakPages
{
    [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
    public class SnippingToolPage : TweakPage
    {
        public SnippingToolPage() : base("Snipping Tool")
        {
            try
            {
                ///HACK: This adds incompatibility with custom drive letters,
                ///      please look into a different solution, if possible.
                ///
                ///      - Craftplacer
                var path = @"C:\Windows\System32\SnippingTool.exe";

                this.Icon = NativeHelpers.GetIconFromGroup(path, 0);
            }
            catch
            {
                this.Icon = Properties.Resources.snippingTool;
            }
        }

        [DisplayName("Hide instructions")]
        public bool HideInstructions
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions") == 0;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions", value ? 0 : 1);
        }

        [Category("Custom Pen")]
        [DisplayName("Tip")]
        public CustomPenTip Tip
        {
            get => (CustomPenTip)RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenTip");
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CustomPenTip", (int)value);
        }

        [Category("Custom Pen")]
        [DisplayName("Color")]
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