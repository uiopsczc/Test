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

    public bool IsContains(IComparable value, bool is_not_include_min = false, bool is_not_include_max = false)
    {
      bool left_result = !is_not_include_min ? value.CompareTo(min) >= 0 : value.CompareTo(min) > 0;
      bool right_result = !is_not_include_max ? value.CompareTo(max) <= 0 : value.CompareTo(min) < 0;
      return left_result && right_result;
    }




  }
}





