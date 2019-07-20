using Newtonsoft.Json;
using System.ComponentModel;

using TweakUtility.Theming;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    public class Config : TweakPage
    {
        public Config() : base("Configuration", null)
        {
        }

        [DisplayName("Current theme")]
        [JsonIgnore]
        public Theme CurrentTheme { get; set; } = Theme.System;
    }
}