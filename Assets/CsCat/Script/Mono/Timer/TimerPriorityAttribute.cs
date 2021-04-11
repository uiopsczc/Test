using System;

namespace CsCat
{
  /// <summary>
  /// Timer优先顺序attribute
  /// </summary>
  public class TimerPriorityAttribute : Attribute
  {
    #region field

    public int priority = 1;

    #endregion

    #region ctor

    public TimerPriorityAttribute(int priority)
    {
      this.priority = priority;
    }

    #endregion

  }
}