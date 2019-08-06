using System;
using System.Collections.Generic;

using TweakUtility.Extensions;

namespace TweakUtility.SampleExtension
{
    public class SampleExtension : Extension
    {
        public override string Name => "Sample Extension";
        public override string Description => "This is a example extension without any great functionality.";

        public override string Author => "Tweak Utility Team";

        public override List<Type> GetTweakPages()
        {
            return new List<Type>()
            {
                typeof(SampleTweakPage)
            };
        }
    }
}