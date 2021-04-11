using UnityEngine;

namespace CsCat
{
  public class GUIToggleScope
  {
    public static bool Toggle(Rect position, ref bool value, string text)
    {
      value = GUI.Toggle(position, value, text);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, Texture image)
    {
      value = GUI.Toggle(position, value, image);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, GUIContent content)
    {
      value = GUI.Toggle(position, value, content);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, string text, GUIStyle style)
    {
      value = GUI.Toggle(position, value, text, style);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, Texture image, GUIStyle style)
    {
      value = GUI.Toggle(position, value, image, style);
      return value;
    }

    public static bool Toggle(Rect position, ref bool value, GUIContent content, GUIStyle style)
    {
      value = GUI.Toggle(position, value, content, style);
      return value;
    }
  }
}