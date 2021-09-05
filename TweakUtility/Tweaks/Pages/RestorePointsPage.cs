using System.Diagnostics;
using System.IO;
using System.Net;
using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    public class RestorePointsPage : TweakPage
    {
        public RestorePointsPage() : base("RestorePoints")
        {
            Icon = NativeHelpers.ExtractIcon(@"%SystemRoot%\system32\twext.dll", -100);
            CustomView = new RestorePointsView();
        }
    }
}