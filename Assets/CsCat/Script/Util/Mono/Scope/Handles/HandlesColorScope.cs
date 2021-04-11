
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
  public class HandlesColorScope : IDisposable
  {
    public HandlesColorScope(Color color_new)
    {
      color_pre = Handles.color;
      Handles.color = color_new;
    }

    [SerializeField] private Color color_pre { get; }

    public void Dispose()
    {
      Handles.color = color_pre;
    }
  }
}
#endif