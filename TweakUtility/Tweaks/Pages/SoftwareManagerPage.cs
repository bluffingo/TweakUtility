
using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Views;

namespace TweakUtility.Tweaks.Pages
{
    /// <summary>
    /// I moved the Software Manager page into it's own seperate page as it is experimental, and I didn't wanted to hide IE/Snipping tool.
    /// -GR 9/8/2021 (mfw i have to use shitty amd r5 gpu from 2014 because hd 6850 fucking died, dad claims that my old 8gb stick from
    /// gaming pc isn't compatible with the acer so i have this fucking dumb 6gb bullshit. this is why chaziz/gamerappa production is halted)
    /// </summary>
    [Experimental]
    public class SoftwareManagerPage : TweakPage
    {
        public SoftwareManagerPage() : base("Uninstall Software")
        {
            this.Icon = Icons.Software;
            this.CustomView = new SoftwarePageView();
        }
    }
}