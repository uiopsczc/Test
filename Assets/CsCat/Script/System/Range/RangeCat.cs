using System;

namespace CsCat
{
    public class RangeCat
    {
        private IComparable min;
        private IComparable max;

        public RangeCat(IComparable min, IComparable max)
        {
            this.min = min;
            this.max = max;
        }

        public bool IsContains(IComparable value, bool isNotIncludeMin = false, bool isNotIncludeMax = false)
        {
            bool leftResult = !isNotIncludeMin ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0;
            bool rightResult = !isNotIncludeMax ? value.CompareTo(max) <= 0 : value.CompareTo(min) < 0;
            return leftResult && rightResult;
        }
    }
}