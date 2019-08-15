using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweakUtility.TweakPages
{
    public class PreferencesPage : TweakPage
    {
        public PreferencesPage() : base("Tweak Utility Preferences")
        {
        }

        [DisplayName("Automatically install extensions")]
        public bool AutoInstallExtensions
        {
            get => Properties.Settings.Default.AutoInstallExtensions;
            set => Properties.Settings.Default.AutoInstallExtensions = value;
        }
    }
}