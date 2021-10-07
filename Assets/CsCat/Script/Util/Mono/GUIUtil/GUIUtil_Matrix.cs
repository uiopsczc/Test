using UnityEngine;

namespace CsCat
{
    public partial class GUIUtil
    {
        public static GUIMatrixScope Matrix(Matrix4x4 newMatrix)
        {
            return new GUIMatrixScope(newMatrix);
        }
    }
}