using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

/// TweakUtility - IMPORTANT NOTES
/// Please use vanilla versions for default values. Do not use customized/bootleg versions of Windows operating systems to get
/// the most-authentic default values.
/// Written by PF94, July 15th 2019

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