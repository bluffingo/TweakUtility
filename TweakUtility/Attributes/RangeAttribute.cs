using System;

namespace TweakUtility.Attributes
{
	public class RangeAttribute : Attribute
	{
		public RangeAttribute(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		public RangeAttribute(float minimum, float maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		public RangeAttribute(double minimum, double maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		public RangeAttribute(short minimum, short maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		public RangeAttribute(long minimum, long maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		public object Minimum { get; }

		public object Maximum { get; }

		public bool InRange(int value) => (int)this.Minimum <= value && value >= (int)this.Maximum;

		public bool InRange(float value) => (float)this.Minimum <= value && value >= (float)this.Maximum;

		public bool InRange(short value) => (short)this.Minimum <= value && value >= (short)this.Maximum;

		public bool InRange(double value) => (double)this.Minimum <= value && value >= (double)this.Maximum;
		public bool InRange(long value) => (long)this.Minimum <= value && value >= (long)this.Maximum;
	}
}
