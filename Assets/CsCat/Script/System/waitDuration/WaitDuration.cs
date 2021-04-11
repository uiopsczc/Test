using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  //为Puase设计
  public class WaitDuration
  {
    #region delegate

    /// <summary>
    ///   更新
    /// </summary>
    public Action<WaitDuration> onUpdate;

    #endregion

    #region field

    /// <summary>
    ///   用于计算时间差的开始时间，可以改变其值
    /// </summary>
    public float start_time_to_calc;


    /// <summary>
    ///   时长
    /// </summary>
    private readonly float duration;

    /// <summary>
    ///   不可以改变的值，构造函数中指定
    /// </summary>
    private float start_time;

    #endregion

    #region public method

    public WaitDuration(float duration, Action<WaitDuration> onUpdate)
    {
      this.duration = duration;
      this.onUpdate = onUpdate;
    }


    public IEnumerator Start()
    {
      start_time = Time.realtimeSinceStartup;
      start_time_to_calc = start_time;
      while (Time.realtimeSinceStartup - start_time_to_calc < duration)
      {
        onUpdate?.Invoke(this);
        yield return null;
      }
    }

    #endregion
  }
}