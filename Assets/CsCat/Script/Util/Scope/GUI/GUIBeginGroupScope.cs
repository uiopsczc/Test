using System;
using UnityEngine;

namespace CsCat
{
    /// <summary>
    ///   之间里面draw的东西都是以这个新的Rect左上角为新的坐标（0，0）
    /// </summary>
    public class GUIBeginGroupScope : IDisposable
    {
        public GUIBeginGroupScope(Rect position)
        {
            GUI.BeginGroup(position);
        }

        public GUIBeginGroupScope(Rect position, string text)
        {
            GUI.BeginGroup(position, text);
        }

        public GUIBeginGroupScope(Rect position, Texture image)
        {
            GUI.BeginGroup(position, image);
        }

        public GUIBeginGroupScope(Rect position, GUIContent content)
        {
            GUI.BeginGroup(position, content);
        }

        public GUIBeginGroupScope(Rect position, GUIStyle style)
        {
            GUI.BeginGroup(position, style);
        }

        public GUIBeginGroupScope(Rect position, string text, GUIStyle style)
        {
            GUI.BeginGroup(position, text, style);
        }

        public GUIBeginGroupScope(Rect position, Texture image, GUIStyle style)
        {
            GUI.BeginGroup(position, image, style);
        }

        public GUIBeginGroupScope(Rect position, GUIContent content, GUIStyle style)
        {
            GUI.BeginGroup(position, content, style);
        }

        public void Dispose()
        {
            GUI.EndGroup();
        }
    }
}