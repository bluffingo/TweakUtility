using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    public class SoftwarePage : TweakPage
    {
        public SoftwarePage() : base("Programs", new SnippingToolPage(), new InternetExplorerPage(), new MsnMessengerPage())
        {
            this.Icon = Icons.Software;
            if (PreferencesPage.EnableExperimentalFeatures)
            {
                this.CustomView = new SoftwarePageView();
            }
        }
    }
}