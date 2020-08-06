using System;
using System.IO;
using TweakUtility.Helpers;

namespace TweakUtility.Tweaks.Pages
{
    //This fixes the bug where Tweak Utility would crash while retrieving the tweak page icon when the user uninstalled Internet Explorer as a feature.
    internal class WindowsExplorerPage : TweakPage
    {
        internal WindowsExplorerPage() : base("Windows Explorer") => this.Icon = NativeHelpers.ExtractIcon(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe"), -0);
    }
}