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

		[RefreshRequired(RestartType.TweakUtility)]
		[DisplayName("Enable experimental features")]
		public static bool EnableExperimentalFeatures
		{
			get => Properties.Settings.Default.EnableExperimentalFeatures;
			set => Properties.Settings.Default.EnableExperimentalFeatures = value;
		}

		[DisplayName("Prefer sliders")]
		[Description("Makes number values with a specified range turn into sliders")]
		public static bool PreferSliders
		{
			get => Properties.Settings.Default.PreferSliders;
			set => Properties.Settings.Default.PreferSliders = value;
		}

		[DisplayName("Save window position and size")]
		public static bool SaveWindowInfo
		{
			get => Properties.Settings.Default.SaveWindowInfo;
			set => Properties.Settings.Default.SaveWindowInfo = value;
		}
	}
}