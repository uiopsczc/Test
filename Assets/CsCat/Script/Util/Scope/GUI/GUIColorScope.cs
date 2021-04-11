using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI.color   包括TextColor&BackgroundColor
  /// </summary>
  public class GUIColorScope : IDisposable
  {
    [SerializeField] private Color color_Pre { get; }

    public GUIColorScope(Color new_color) : this()
    {
      GUI.color = new_color;
    }

    public GUIColorScope()
    {
      color_Pre = GUI.color;
    }

    public void Dispose()
    {
      GUI.color = color_Pre;
    }
  }
}