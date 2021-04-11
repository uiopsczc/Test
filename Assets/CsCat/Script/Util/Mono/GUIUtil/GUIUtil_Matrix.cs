using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static GUIMatrixScope Matrix(Matrix4x4 martix_new)
    {
      return new GUIMatrixScope(martix_new);
    }
  }
}