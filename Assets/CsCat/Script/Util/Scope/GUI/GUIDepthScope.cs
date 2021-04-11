using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI.depth   相当于SortingOrder
  /// </summary>
  public class GUIDepthScope : IDisposable
  {
    public GUIDepthScope(int new_depth)
    {
      depth_pre = GUI.depth;
      GUI.depth = new_depth;
    }

    [SerializeField] private int depth_pre { get; }

    public void Dispose()
    {
      GUI.depth = depth_pre;
    }
  }
}