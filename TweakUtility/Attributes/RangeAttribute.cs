using System;

namespace TweakUtility.Attributes
{
    public class RangeAttribute : Attribute
    {
        public RangeAttribute(int minimum, int maximum)
        {
            this.Mininum = minimum;
            this.Maximum = maximum;
        }

        public RangeAttribute(float minimum, float maximum)
        {
            this.Mininum = minimum;
            this.Maximum = maximum;
        }

        public RangeAttribute(double minimum, double maximum)
        {
            this.Mininum = minimum;
            this.Maximum = maximum;
        }

        public RangeAttribute(short minimum, short maximum)
        {
            this.Mininum = minimum;
            this.Maximum = maximum;
        }

        public RangeAttribute(long minimum, long maximum)
        {
            this.Mininum = minimum;
            this.Maximum = maximum;
        }

        public object Mininum { get; }

        public object Maximum { get; }

        public bool InRange(int value) => (int)this.Mininum <= value && value >= (int)this.Maximum;

        public bool InRange(float value) => (float)this.Mininum <= value && value >= (float)this.Maximum;

        public bool InRange(short value) => (short)this.Mininum <= value && value >= (short)this.Maximum;

        public bool InRange(double value) => (double)this.Mininum <= value && value >= (double)this.Maximum;

        public bool InRange(long value) => (long)this.Mininum <= value && value >= (long)this.Maximum;
    }
}