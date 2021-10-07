using System;
using UnityEngine;

namespace CsCat
{
    public class GUIFontSizeScope : IDisposable
    {
        private readonly GUIStyle[] guiStyles;
        private readonly int[] sizes;


        public GUIFontSizeScope(float size, params GUIStyle[] guiStyles)
        {
            if (guiStyles == null || guiStyles.Length == 0)
                this.guiStyles = new[] {GUI.skin.label, GUI.skin.button, GUI.skin.toggle};
            else
            {
                this.guiStyles = new GUIStyle[guiStyles.Length];
                Array.Copy(guiStyles, this.guiStyles, guiStyles.Length);
            }

            sizes = new int[this.guiStyles.Length];
            for (var i = 0; i < this.guiStyles.Length; ++i)
            {
                sizes[i] = this.guiStyles[i].fontSize;
                this.guiStyles[i].fontSize = (int) size;
            }
        }


        public void Dispose()
        {
            for (var i = 0; i < guiStyles.Length; ++i) guiStyles[i].fontSize = sizes[i];
        }
    }
}