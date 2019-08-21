using System;
using TweakUtility.Attributes;
using TweakUtility.Enums;
using TweakUtility.Helpers;

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

        [DisplayName("Cosmetic verison")]
        [Description("This preference will override some OS-specific styling values")]
        public static OperatingSystemVersion CosmeticVersion
        {
            get => Properties.Settings.Default.CosmeticVersion;
            set => Properties.Settings.Default.CosmeticVersion = value;
        }

        /// <summary>
        /// Intended to be used by code.
        /// </summary>
        [Visible(false)]
        public static Version GetCosmeticVersion()
        {
            if (CosmeticVersion == OperatingSystemVersion.None)
            {
                return OperatingSystemVersions.CurrentVersion;
            }

            return OperatingSystemVersions.GetVersion(CosmeticVersion);
        }
    }
}