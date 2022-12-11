using System;

namespace CsCat
{
	public class RangeCat
	{
		private readonly IComparable _min;
		private readonly IComparable _max;

		public RangeCat(IComparable min, IComparable max)
		{
			this._min = min;
			this._max = max;
		}

		public bool IsContains(IComparable value, bool isNotIncludeMin = false, bool isNotIncludeMax = false)
		{
			bool leftResult = !isNotIncludeMin ? value.CompareTo(_min) >= 0 : value.CompareTo(_min) > 0;
			bool rightResult = !isNotIncludeMax ? value.CompareTo(_max) <= 0 : value.CompareTo(_min) < 0;
			return leftResult && rightResult;
		}
	}
}