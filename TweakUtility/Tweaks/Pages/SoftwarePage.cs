using TweakUtility.Helpers;
using TweakUtility.Attributes;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    [Experimental]
    public class SoftwarePage : TweakPage
    {
        public SoftwarePage() : base("Software")
        {
            this.Icon = Icons.Software;
            this.CustomView = new SoftwarePageView();
        }
    }
}