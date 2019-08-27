using System;
using System.Security.AccessControl;

namespace TweakUtility.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class RefreshRequiredAttribute : Attribute
    {
        public RefreshRequiredAttribute(RestartType type = RestartType.None) => this.Type = type;

		public RefreshRequiredAttribute(RestartType type, string argument) : this(type) => this.Argument = argument;

		public string Argument { get; }

        public RestartType Type { get; }
    }

    public enum RestartType
    {
        /// <summary>
        /// Requires nothing to be done, to apply the changes.
        /// </summary>
        None,

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

        /// <summary>
        /// Requires a process to be restarted, to apply the changes.
        /// </summary>
        ProcessRestart,

		/// <summary>
		/// Requires a service to be restarted, to apply the changes.
		/// </summary>
		ServiceRestart,

        /// <summary>
        /// Requires Tweak Utility to be restarted, to apply the changes.
        /// </summary>
        TweakUtility,

        /// <summary>
        /// Notifies the user has to reload the thing related to the option. As there isn't a known method to reload.
        /// </summary>
        Unknown,
    }
}