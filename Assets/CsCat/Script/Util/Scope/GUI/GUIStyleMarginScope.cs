using System;
using UnityEngine;

namespace CsCat
{
  public class GUIStyleMarginScope : IDisposable
  {
    private readonly RectOffset margin; //边缘，在GUILayout类函数下起作用，和其他控件的距离
    private readonly RectOffset overflow; //溢出区域，也就是在margin(和其他控件的距离)固定的情况下，背景部分再画多出去多少
    private readonly RectOffset padding; //内容和控件大小(也就是背景)的距离
    private readonly GUIStyle guiStyle;

    public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin) : this(guiStyle, margin, guiStyle.padding, guiStyle.overflow)
    {
    }

    public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin, RectOffset padding) : this(guiStyle, margin, padding,
      guiStyle.overflow)
    {
    }

    public GUIStyleMarginScope(GUIStyle guiStyle, RectOffset margin, RectOffset padding, RectOffset overflow)
    {
      this.guiStyle = guiStyle;
      this.margin = margin;
      this.padding = padding;
      this.overflow = overflow;
      guiStyle.margin = this.margin;
      guiStyle.padding = this.padding;
      guiStyle.overflow = this.overflow;
    }

    public void Dispose()
    {
      guiStyle.margin = margin;
      guiStyle.padding = padding;
      guiStyle.overflow = overflow;
    }
  }
}