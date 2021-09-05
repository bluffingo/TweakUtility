using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    internal partial class AdvancedPage
    {
        public class HostsPage : TweakPage
        {
            public HostsPage() : base("Hosts")
            {
                this.Icon = Icons.InternetArrow;
                this.CustomView = new HostsPageView();
            }
        }
    }
}