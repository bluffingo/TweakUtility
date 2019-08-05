using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweakUtility.TweakPages;

namespace TweakUtility.SampleExtension
{
    public class SampleExtension : Extension
    {
        public override string Name => "Sample Extension";
        public override string Description => "This is a example extension without any great functionality.";

        public override List<Type> GetTweakPages()
        {
            return new List<Type>()
            {
                typeof(SampleTweakPage)
            };
        }
    }
}