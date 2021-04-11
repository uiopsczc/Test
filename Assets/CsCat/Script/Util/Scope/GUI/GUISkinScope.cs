using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI全局的skin
  /// </summary>
  public class GUISkinScope : IDisposable
  {
    private readonly GUISkin skin_pre;

    public GUISkinScope(GUISkin skin)
    {
      skin_pre = GUI.skin;
      GUI.skin = skin;
    }

    public void Dispose()
    {
      GUI.skin = skin_pre;
    }
  }
}