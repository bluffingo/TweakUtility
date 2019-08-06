using System.Diagnostics;
using System.Reflection;

namespace TweakUtility
{
    internal sealed class TweakAction : TweakEntry
    {
        private MethodInfo MethodInfo => reflectionInfo as MethodInfo;

        public object Invoke()
        {
            Debug.Assert(this.MethodInfo != null, "Method info is null");

            ParameterInfo[] parameters = this.MethodInfo.GetParameters();
            if (parameters.Length == 1 && parameters[0].ParameterType == typeof(ProgressIndicator))
            {
                using (var indicator = new ProgressIndicator())
                {
                    var parameters2 = new object[1] { indicator };
                    return this.MethodInfo.Invoke(this.parent, parameters2);
                }
            }
            else
            {
                return this.MethodInfo.Invoke(this.parent, new object[0]);
            }
        }

        internal TweakAction(TweakPage tweakPage, MethodInfo methodInfo) : base(tweakPage, reflectionInfo: methodInfo)
        {
        }
    }
}