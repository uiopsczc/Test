using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI.enabled  如果false的话，之后的东西都会变灰，不能使用，直到为true
  /// </summary>
  public class GUIEnabledScope : IDisposable
  {
    public GUIEnabledScope(bool is_new_enabled)
    {
      enabled_pre = GUI.enabled;
      GUI.enabled = is_new_enabled;
    }

    [SerializeField] private bool enabled_pre { get; }

    public void Dispose()
    {
      GUI.enabled = enabled_pre;
    }
  }
}