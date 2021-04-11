using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI.contentColor   只是text的color
  /// </summary>
  public class GUIContentColorScope : IDisposable
  {
    public GUIContentColorScope(Color new_color)
    {
      color_pre = GUI.contentColor;
      GUI.contentColor = new_color;
    }

    [SerializeField] private Color color_pre { get; }

    public void Dispose()
    {
      GUI.contentColor = color_pre;
    }
  }
}