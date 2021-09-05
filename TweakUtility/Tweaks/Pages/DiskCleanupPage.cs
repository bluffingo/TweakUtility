using TweakUtility.Tweaks.Views;
using static TweakUtility.Helpers.NativeHelpers;

namespace TweakUtility.Tweaks.Pages
{
    /// <remarks>
    /// Reference: https://docs.microsoft.com/en-us/windows/win32/lwef/disk-cleanup
    /// </remarks>
    internal class DiskCleanupPage : TweakPage
    {
        public DiskCleanupPage() : base("Disk Cleanup")
        {
            this.CustomView = new DiskCleanupPageView();
            this.Icon = ExtractIcon(@"%systemroot%\System32\cleanmgr.exe", -0);
        }
    }
}