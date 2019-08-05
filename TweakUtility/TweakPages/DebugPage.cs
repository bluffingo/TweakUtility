using System.ComponentModel;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    internal class DebugPage : TweakPage
    {
        internal DebugPage() : base("Debug")
        {
        }

        public string Text { get; set; }
        public int Integer { get; set; }
        public bool Boolean { get; set; }

        [Browsable(true)]
        public void Function() => MessageBox.Show("Hello world!");
    }
}