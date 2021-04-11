using System;
using UnityEngine;

namespace CsCat
{
  public class GUIBackgroundColorScope : IDisposable
  {
    public GUIBackgroundColorScope(Color new_color)
    {
      color_pre = GUI.backgroundColor;
      GUI.backgroundColor = new_color;
    }

    [SerializeField] private Color color_pre { get; }

    public void Dispose()
    {
      GUI.backgroundColor = color_pre;
    }
  }
}