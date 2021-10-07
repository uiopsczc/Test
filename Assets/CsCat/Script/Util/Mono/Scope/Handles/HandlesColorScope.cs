#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
    public class HandlesColorScope : IDisposable
    {
        [SerializeField] private Color preColor { get; }

        public HandlesColorScope(Color newColor)
        {
            preColor = Handles.color;
            Handles.color = newColor;
        }


        public void Dispose()
        {
            Handles.color = preColor;
        }
    }
}
#endif