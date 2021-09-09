using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    public class SoftwarePage : TweakPage
    {
        public SoftwarePage() : base("Software", new SnippingToolPage(), new InternetExplorerPage(), new SoftwareManagerPage())
        {
            this.Icon = Icons.Software;
        }
    }
}