using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    public class Config : TweakPage
    {
        public Config() : base("Configuration", null)
        {
        }

        [DisplayName("Enforce dark mode")]
        public bool EnforceDarkMode { get; set; }

        public bool DarkMode
        {
            get
            {
                if (EnforceDarkMode)
                {
                    return true;
                }

                return !CustomizationPage.AppsUseLightTheme;
            }
        }
    }
}