using System.Drawing;

using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    [OperatingSystemSupported(OperatingSystemVersion.WindowsVista)]
    [RegistryKeyRequired(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool")]
    [Notice(NoticeType.Warning, "Make sure Snipping Tool is closed, before continuing.")]
    internal class SnippingToolPage : TweakPage
    {
        internal SnippingToolPage() : base("Snipping Tool")
        {
            this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\sysnative\SnippingTool.exe", 0);

            if (this.Icon == null)
            {
                this.Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\SnippingTool.exe", 0);
            }
        }

        [DisplayName("Hide instruction text")]
        public bool HideSnipInstructions
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions") == 0;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\DisplaySnipInstructions", value ? 0 : 1);
        }

        [DisplayName("Show screen overlay when Snipping Tool is active")]
        public bool CaptureWindowVisible
        {
            get => RegistryHelper.GetValue<int>(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CaptureWindowVisible") == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CaptureWindowVisible", value ? 1 : 0);
        }

        [OperatingSystemSupported(OperatingSystemVersion.Windows10)]
        [DisplayName("Expand screen sketch banner")]
        public bool IsScreenSketchBannerExpanded
        {
            get => RegistryHelper.GetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\IsScreenSketchBannerExpanded", 0) == 1;
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\IsScreenSketchBannerExpanded", value);
        }

        [DisplayName("Capture mode")]
        public Mode CaptureMode
        {
            get => (Mode)RegistryHelper.GetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CaptureMode", 2);
            set => RegistryHelper.SetValue(@"HKCU\SOFTWARE\Microsoft\Windows\TabletPC\Snipping Tool\CaptureMode", (int)value);
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
            [DisplayName("Round tip pen")]
            Round = 0,

            [DisplayName("Chisel tip pen")]
            Chisel = 1,
        }

        public enum Mode
        {
            [DisplayName("Free-form Snip")]
            FreeForm = 1,

            [DisplayName("Rectangular Snip")]
            Rectangular = 2,

            [DisplayName("Window Snip")]
            Window = 3
        }
    }
}