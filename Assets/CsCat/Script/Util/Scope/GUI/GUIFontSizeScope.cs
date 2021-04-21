using System;
using UnityEngine;

namespace CsCat
{
  public class GUIFontSizeScope : IDisposable
  {
    private readonly GUIStyle[] ps;
    private readonly int[] sizes;


    public GUIFontSizeScope(float size, params GUIStyle[] ps)
    {
      if (ps == null || ps.Length == 0)
      {
        this.ps = new[] { GUI.skin.label, GUI.skin.button, GUI.skin.toggle };
      }
      else
      {
        this.ps = new GUIStyle[ps.Length];
        Array.Copy(ps, this.ps, ps.Length);
      }

      sizes = new int[this.ps.Length];
      for (var i = 0; i < this.ps.Length; ++i)
      {
        sizes[i] = this.ps[i].fontSize;
        this.ps[i].fontSize = (int)size;
      }
    }


    public void Dispose()
    {
      for (var i = 0; i < ps.Length; ++i) ps[i].fontSize = sizes[i];
    }
  }
}