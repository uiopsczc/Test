using System;

namespace CsCat
{
  // t 时间
  // b 开始值
  // c 增量值  （结束值= 开始值 + 增量值）所以该值应该是（增量值 = 结束值 - 开始值）
  // d 总时长
  //https://blog.csdn.net/septwolves2015/article/details/52992844
  public partial class EaseCat
  {
    protected const float Two_PI = (float) Math.PI * 2;
    protected const float Half_PI = (float) Math.PI / 2;
  }
}