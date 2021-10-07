#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
    public class HandlesUtil_Matrix
    {
        public static HandlesMatrixScope Matrix(Matrix4x4 newMatrix)
        {
            return new HandlesMatrixScope(newMatrix);
        }
    }
}
#endif