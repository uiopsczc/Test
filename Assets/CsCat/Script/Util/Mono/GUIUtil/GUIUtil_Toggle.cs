using UnityEngine;

namespace CsCat
{
  public partial class GUIUtil
  {
    public static bool Toggle(Rect position, ref bool value, string text)
    {
      return GUIToggleScope.Toggle(position, ref value, text);
    }

    public static bool Toggle(Rect position, bool value, Texture image)
    {
      return GUIToggleScope.Toggle(position, ref value, image);
    }

    public static bool Toggle(Rect position, bool value, GUIContent content)
    {
      return GUIToggleScope.Toggle(position, ref value, content);
    }

    public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
    {
      return GUIToggleScope.Toggle(position, ref value, text, style);
    }

    public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
    {
      return GUIToggleScope.Toggle(position, ref value, image, style);
    }

    public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
    {
      return GUIToggleScope.Toggle(position, ref value, content, style);
    }
  }
}