#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    public class HandlesMatrixScope : IDisposable
    {
        private Matrix4x4 _preMatrix { get; }

        public HandlesMatrixScope(Matrix4x4 newMatrix)
        {
            _preMatrix = Handles.matrix;
            Handles.matrix = newMatrix;
        }


        public void Dispose()
        {
            Handles.matrix = _preMatrix;
        }
    }
}
#endif