using System;
using UnityEngine;

namespace CsCat
{
    public class GUILabelAlignScope : IDisposable
    {
        private readonly TextAnchor old;

        public GUILabelAlignScope(TextAnchor alignment)
        {
            old = GUI.skin.label.alignment;
            GUI.skin.label.alignment = alignment;
        }

        public void Dispose()
        {
            GUI.skin.label.alignment = old;
        }
    }
}