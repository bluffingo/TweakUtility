using System;
using TweakUtility.Tweaks.Pages;

namespace TweakUtility.Attributes
{
    /// <summary>
    /// Marks something as experimental, which will be hidden by default to the user.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ExperimentalAttribute : RequirementAttribute
    {
        public override bool Valid => PreferencesPage.EnableExperimentalFeatures;
    }
}