using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TweakUtility.TweakPages
{
    public partial class StartupPageView : UserControl
    {
        public StartupPageView()
        {
            this.InitializeComponent();
        }
    }

    public class StartupPage : TweakPage
    {
        public StartupPage() : base("Startup") => this.CustomView = new StartupPageView();
    }
}