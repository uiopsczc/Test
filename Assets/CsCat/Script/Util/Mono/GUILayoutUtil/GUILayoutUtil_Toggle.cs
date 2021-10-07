using UnityEngine;

namespace CsCat
{
    public partial class GUILayoutUtil
    {
        public static bool Toggle(ref bool value, string text)
        {
            return AutoGUILayoutToggle.Toggle(ref value, text);
        }

        public static bool Toggle(bool value, Texture image)
        {
            return AutoGUILayoutToggle.Toggle(ref value, image);
        }

        public static bool Toggle(bool value, GUIContent content)
        {
            return AutoGUILayoutToggle.Toggle(ref value, content);
        }

        public static bool Toggle(bool value, string text, GUIStyle style)
        {
            return AutoGUILayoutToggle.Toggle(ref value, text, style);
        }

        public static bool Toggle(bool value, Texture image, GUIStyle style)
        {
            return AutoGUILayoutToggle.Toggle(ref value, image, style);
        }

        public static bool Toggle(bool value, GUIContent content, GUIStyle style)
        {
            return AutoGUILayoutToggle.Toggle(ref value, content, style);
        }
    }
}