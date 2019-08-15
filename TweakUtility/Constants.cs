using System.Drawing;
using System.Windows.Forms;

namespace TweakUtility
{
    internal static class Constants
    {
        public const int Design_Category_Padding_Top = 11;
        public const int Design_Category_Padding_Bottom = 7;
        public static readonly Padding Design_Category_Padding = new Padding(0, Design_Category_Padding_Top, 0, Design_Category_Padding_Bottom);
        public const string Application_Id = "Craftplacer.TweakUtility";

        public static readonly StringFormat NearCenterStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        public static readonly StringFormat CenterNearStringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Near
        };
    }
}