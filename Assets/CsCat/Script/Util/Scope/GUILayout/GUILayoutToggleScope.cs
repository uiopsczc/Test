using UnityEngine;

namespace CsCat
{
    public class AutoGUILayoutToggle
    {
        public static bool Toggle(ref bool value, string text)
        {
            value = GUILayout.Toggle(value, text);
            return value;
        }

        public static bool Toggle(ref bool value, Texture image)
        {
            value = GUILayout.Toggle(value, image);
            return value;
        }

        public static bool Toggle(ref bool value, GUIContent content)
        {
            value = GUILayout.Toggle(value, content);
            return value;
        }

        public static bool Toggle(ref bool value, string text, GUIStyle style)
        {
            value = GUILayout.Toggle(value, text, style);
            return value;
        }

        public static bool Toggle(ref bool value, Texture image, GUIStyle style)
        {
            value = GUILayout.Toggle(value, image, style);
            return value;
        }

        public static bool Toggle(ref bool value, GUIContent content, GUIStyle style)
        {
            value = GUILayout.Toggle(value, content, style);
            return value;
        }
    }
}