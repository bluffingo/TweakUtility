using System.Reflection;

namespace TweakUtility
{
    internal sealed class TweakAction : TweakEntry
    {
        private MethodInfo _methodInfo => reflectionInfo as MethodInfo;

        public object Invoke()
        {
            ParameterInfo[] parameters = _methodInfo.GetParameters();
            if (parameters.Length == 1 && parameters[0].ParameterType == typeof(ProgressIndicator))
            {
                using (var indicator = new ProgressIndicator())
                {
                    return this._methodInfo.Invoke(this.parent, new object[1] { indicator });
                }
            }
            else
            {
                return this._methodInfo.Invoke(this.parent, new object[0]);
            }
        }

        internal TweakAction(TweakPage tweakPage, MethodInfo methodInfo) : base(tweakPage, reflectionInfo: methodInfo)
        {
        }
    }
}