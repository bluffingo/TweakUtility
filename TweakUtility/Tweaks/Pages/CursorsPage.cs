using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    public class CursorsPage : TweakPage
    {
        public CursorsPage() : base("Cursors")
        {
            this.CustomView = new CursorsPageView();
            this.Icon = Properties.Resources.Cursor_16x;
        }
    }
}