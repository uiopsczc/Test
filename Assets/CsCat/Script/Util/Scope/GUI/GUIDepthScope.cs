using System;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   GUI.depth   相当于SortingOrder
    /// </summary>
    public class GUIDepthScope : IDisposable
    {
        [SerializeField] private int _preDepth { get; }

        public GUIDepthScope(int newDepth)
        {
            _preDepth = GUI.depth;
            GUI.depth = newDepth;
        }


        public void Dispose()
        {
            GUI.depth = _preDepth;
        }
    }
}