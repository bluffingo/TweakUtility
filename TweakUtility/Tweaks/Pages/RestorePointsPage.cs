using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    [Experimental]
    public class RestorePointsPage : TweakPage
    {
        public RestorePointsPage() : base("Restore Points")
        {
            Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\twext.dll", -100);
            CustomView = new RestorePointsView();
        }
    }
}