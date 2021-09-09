using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class CustomizationPage
    {
        public class BackgroundsPage : TweakPage
        {
            public BackgroundsPage() : base("Backgrounds")
            {
                this.CustomView = new BackgroundsPageView();
            }
        }
    }
}