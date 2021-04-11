#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public class HandlesUtil_Matrix
  {
    public static HandlesMatrixScope Matrix(Matrix4x4 matrix_new)
    {
      return new HandlesMatrixScope(matrix_new);
    }
  }
}
#endif