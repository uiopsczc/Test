using System;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   GUI.matrix   GUI使用的矩阵
    /// </summary>
    public class GUIMatrixScope : IDisposable
    {
        [SerializeField] private Matrix4x4 _preMartix { get; }

        public GUIMatrixScope(Matrix4x4 newMartix)
        {
            _preMartix = GUI.matrix;
            GUI.matrix = newMartix;
        }


        public void Dispose()
        {
            GUI.matrix = _preMartix;
        }
    }
}