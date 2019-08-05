using System.ComponentModel;

namespace TweakUtility.SampleExtension
{
    public class SampleTweakPage : TweakPage
    {
        public SampleTweakPage() : base("Sample Tweak Page")
        {
        }

        [DisplayName("Make Extensions")]
        public bool MakeExtensions { get; set; } = true;
    }
}