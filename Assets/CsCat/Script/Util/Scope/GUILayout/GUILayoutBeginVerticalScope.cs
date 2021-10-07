using System;
using UnityEngine;

namespace CsCat
{
    public class GUILayoutBeginVerticalScope : IDisposable
    {
        public GUILayoutBeginVerticalScope()
        {
            GUILayout.BeginVertical();
        }

        public GUILayoutBeginVerticalScope(params GUILayoutOption[] layoutOptions)
        {
            GUILayout.BeginVertical(layoutOptions);
        }

        public GUILayoutBeginVerticalScope(GUIStyle guiStyle, params GUILayoutOption[] layoutOptions)
        {
            GUILayout.BeginVertical(guiStyle, layoutOptions);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }
}