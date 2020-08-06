using System.Drawing;
using System.Windows.Forms;
using TweakUtility.Attributes;
using TweakUtility.Theming;

namespace TweakUtility.Tweaks.Pages
{
    [Description("Various tweak entries for debugging purposes")]
    internal class DebugPage : TweakPage
    {
        internal DebugPage() : base("Debug")
        {
        }

        public string Text { get; set; }
        public int Integer { get; set; }
        public bool Boolean { get; set; }

        public Color Color => Color.FromArgb(128, 255, 0, 0);

        [Visible(true)]
        public void Function() => MessageBox.Show("Hello world!");

        public bool IsDark => Theme.IsDark;
    }
}