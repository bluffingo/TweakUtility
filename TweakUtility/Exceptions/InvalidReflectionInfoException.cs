using System;

namespace TweakUtility.Exceptions
{
    /// <summary>
    /// This exception is usually thrown by reflection methods, having multiple reflection info objects.
    /// </summary>
    public class InvalidReflectionInfoException : Exception
    {
        public InvalidReflectionInfoException() : base("The given reflection info is invalid/out of range.")
        {
        }
    }
}