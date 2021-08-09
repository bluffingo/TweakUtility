using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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