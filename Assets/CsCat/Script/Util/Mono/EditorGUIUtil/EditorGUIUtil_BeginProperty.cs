using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static EditorGUIBeginPropertyScope BeginProperty(Rect total_position, GUIContent label,
      SerializedProperty property)
    {
      return new EditorGUIBeginPropertyScope(total_position, label, property);
    }
  }
}
#endif