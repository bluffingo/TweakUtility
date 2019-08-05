using System.Reflection;

namespace TweakUtility
{
    internal sealed class TweakAction : TweakEntry
    {
        private MethodInfo _methodInfo => reflectionInfo as MethodInfo;

        public object Invoke(params object[] parameters) => this._methodInfo.Invoke(this.parent, parameters);

        internal TweakAction(TweakPage tweakPage, MethodInfo methodInfo) : base(tweakPage, reflectionInfo: methodInfo)
        {
        }
    }
}