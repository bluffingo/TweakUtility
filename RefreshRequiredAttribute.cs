using System;

namespace TweakUtility
{
    public class RefreshRequiredAttribute : Attribute
    {
        public RefreshRequiredAttribute(RestartType type) => this.Type = type;

        public RestartType Type { get; set; }
    }

    public enum RestartType
    {
        /// <summary>
        /// Requires the Windows Explorer to be restarted, to apply the changes.
        /// </summary>
        ExplorerRestart,

        /// <summary>
        /// Requires the system to be restarted, to apply the changes.
        /// </summary>
        SystemRestart,

        /// <summary>
        /// Requires the user to be logged off, to apply the changes.
        /// </summary>
        Logoff,
    }
}