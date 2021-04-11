using System;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   GUI.matrix   GUI使用的矩阵
  /// </summary>
  public class GUIMatrixScope : IDisposable
  {
    public GUIMatrixScope(Matrix4x4 new_martix)
    {
      martix_pre = GUI.matrix;
      GUI.matrix = new_martix;
    }

    [SerializeField] private Matrix4x4 martix_pre { get; }

    public void Dispose()
    {
      GUI.matrix = martix_pre;
    }
  }
}