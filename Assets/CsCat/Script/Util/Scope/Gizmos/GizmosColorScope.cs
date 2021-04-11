using System;
using UnityEngine;

namespace CsCat
{
  public class GizmosColorScope : IDisposable
  {
    public GizmosColorScope(Color new_color)
    {
      color_pre = GUI.color;
      Gizmos.color = new_color;
    }

    [SerializeField] private Color color_pre { get; }

    public void Dispose()
    {
      Gizmos.color = color_pre;
    }
  }
}