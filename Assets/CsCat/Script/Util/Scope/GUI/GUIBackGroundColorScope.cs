using System;
using UnityEngine;

namespace CsCat
{
    public class GUIBackgroundColorScope : IDisposable
    {
        [SerializeField] private Color preColor { get; }

        public GUIBackgroundColorScope(Color newColor)
        {
            preColor = GUI.backgroundColor;
            GUI.backgroundColor = newColor;
        }


        public void Dispose()
        {
            GUI.backgroundColor = preColor;
        }
    }
}