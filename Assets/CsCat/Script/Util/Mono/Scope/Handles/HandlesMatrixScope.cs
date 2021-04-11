
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class HandlesMatrixScope : IDisposable
  {
    public HandlesMatrixScope(Matrix4x4 matrix_new)
    {
      martix_pre = Handles.matrix;
      Handles.matrix = matrix_new;
    }

    private Matrix4x4 martix_pre { get; }

    public void Dispose()
    {
      Handles.matrix = martix_pre;
    }
  }
}
#endif