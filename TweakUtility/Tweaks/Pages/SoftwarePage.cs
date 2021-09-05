using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    [Experimental]
    public class SoftwarePage : TweakPage
    {
        public SoftwarePage() : base("Software", new SnippingToolPage(), new InternetExplorerPage())
        {
            this.Icon = Icons.Software;
            this.CustomView = new SoftwarePageView();
        }
    }
}