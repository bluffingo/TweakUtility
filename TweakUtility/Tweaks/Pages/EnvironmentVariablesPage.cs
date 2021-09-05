using System.Diagnostics;
using System.IO;
using System.Net;
using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    [Experimental]
    internal class EnvironmentVariablesPage : TweakPage
    {
        public EnvironmentVariablesPage() : base("Environment Variables") => this.CustomView = new EnvironmentVariablesPageView();
    }
}