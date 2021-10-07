using System;
using UnityEngine;

namespace CsCat
{
    public class GizmosColorScope : IDisposable
    {
        [SerializeField] private Color _preColor { get; }

        public GizmosColorScope(Color newColor)
        {
            _preColor = GUI.color;
            Gizmos.color = newColor;
        }


        public void Dispose()
        {
            Gizmos.color = _preColor;
        }
    }
}