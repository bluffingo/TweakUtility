using System;
using System.Windows.Forms;
using TweakUtility.Attributes;

namespace TweakUtility.Forms
{
    internal partial class MainForm
    {
        private bool TriggerCreeperPage(string query)
        {
            if (query.Equals("Creeper, aw man", StringComparison.InvariantCultureIgnoreCase))
            {
                if (DateTime.Now.Year == 2019 && DateTime.Now.Month == 8)
                {
                    this.SetView(new DeadMemeCreeperPage());
                }
                else
                {
                    MessageBox.Show("Hold up, you're trying to access a dead meme.", Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return false;
            }
            return true;
        }

        private class DeadMemeCreeperPage : TweakPage
        {
            public DeadMemeCreeperPage() : base("Creeper, aw man")
            {
            }

            //TO-DO: Deprecate this in the first build of TweakUtility released in September.
            [DisplayName(".")]
            public string T1 => "So we back in the mine, got our pickaxe swingin' from side to side, side, side to side";

            [DisplayName(".")]
            public string T2 => "This task a grueling one, hope to find some diamonds tonight, night, night, diamonds tonight";

            [DisplayName(".")]
            public string T3 => "Heads up";

            [DisplayName(".")]
            public string T4 => "You hear a sound, turn around and look up";

            [DisplayName(".")]
            public string T5 => "Total shock fills your body";

            [DisplayName(".")]
            public string T6 => "Oh no it's you again, I can never forget those eyes, eyes, eyes, eyes, eyes, eyes";

            [DisplayName(".")]
            public string T7 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string T8 => "Cause baby tonight, you grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string T9 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string T10 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again, stuff again, gain";

            [DisplayName(".")]
            public string T11 => "Just when you think you're safe, overhear some hissing from right behind, right, right behind";

            [DisplayName(".")]
            public string T12 => "That's a nice life you have, shame it's gotta end at this time, time, time, time, time, time, time";

            [DisplayName(".")]
            public string T13 => "Blows up, then your health bar drops and you could use a 1-up";

            [DisplayName(".")]
            public string T14 => "Get inside don't be tardy";

            [DisplayName(".")]
            public string T15 => "So now you're stuck in there, half a heart is left but don't die, die, die, die, die, die";

            [DisplayName(".")]
            public string T16 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string T17 => "Cause baby tonight, grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string T18 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string T19 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string T20 => "Creepers, you're mine ha ha";

            [DisplayName(".")]
            public string T21 => "Dig up diamonds, craft those diamonds, make some armor";

            [DisplayName(".")]
            public string T22 => "Get it baby, go and forge that like you so, TweakUtility pro";

            [DisplayName(".")]
            public string T23 => "The sword's made of diamonds, so come at me bro";

            [DisplayName(".")]
            public string T24 => "Ha, training in your room under the torch-light";

            [DisplayName(".")]
            public string T25 => "Hone that form to get you ready for the big fight";

            [DisplayName(".")]
            public string T26 => "Every single day in the whole night";

            [DisplayName(".")]
            public string T27 => "Creeper's out prowlin', (Whoo), alright";

            [DisplayName(".")]
            public string T28 => "Look at me, look at you";

            [DisplayName(".")]
            public string T29 => "Take my revenge that's what I'm gonna do";

            [DisplayName(".")]
            public string T30 => "I'm a warrior baby, what else is new";

            [DisplayName(".")]
            public string T31 => "And my blade's gonna tear through you";

            [DisplayName(".")]
            public string T32 => "Bring it";

            [DisplayName(".")]
            public string T33 => "Cause baby tonight, the creeper's tryin' to steal all our stuff again (Get your stuff)";

            [DisplayName(".")]
            public string T34 => "Yea, let's take back the world";

            [DisplayName(".")]
            public string T35 => "Yea baby tonight, grab your sword armor and go (It's on)";

            [DisplayName(".")]
            public string T36 => "Take your revenge (Whoo)";

            [DisplayName(".")]
            public string T37 => "Oh so fight, fight, like it's the last, last night of your life, life show them your bite (Whoo)";

            [DisplayName(".")]
            public string T38 => "Cause baby tonight, the creeper's tryin' to steal our stuff again";

            [DisplayName(".")]
            public string T39 => "Cause baby tonight, grab your pick shovel and bolt again, bolt again, gain";

            [DisplayName(".")]
            public string T40 => "And run, run until it's done, done, until the sun comes up in the morn'";

            [DisplayName(".")]
            public string T41 => "Cause baby tonight, come on, the creeper's, come on, tryin' to steal all our stuff again";

            [DisplayName(".")]
            public string T42 => "(Whoo)";
        }
    }
}