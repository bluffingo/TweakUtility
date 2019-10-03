using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    public class StartupPage : TweakPage
    {
        public StartupPage() : base("Startup") => this.CustomView = new StartupPageView();
    }
}