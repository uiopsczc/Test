using System;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   GUI全局的skin
    /// </summary>
    public class GUISkinScope : IDisposable
    {
        private readonly GUISkin _preSkin;

        public GUISkinScope(GUISkin skin)
        {
            _preSkin = GUI.skin;
            GUI.skin = skin;
        }

        public void Dispose()
        {
            GUI.skin = _preSkin;
        }
    }
}