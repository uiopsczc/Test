using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static T ObjectField<T>(Rect position, Object obj, bool is_allow_sceneObjects) where T : Object
    {
      return (T) EditorGUI.ObjectField(position, obj, typeof(T), is_allow_sceneObjects);
    }

    public static T ObjectField<T>(Rect position, string label, Object obj, bool is_allow_sceneObjects)
      where T : Object
    {
      return (T) EditorGUI.ObjectField(position, label, obj, typeof(T), is_allow_sceneObjects);
    }

    public static T ObjectField<T>(Rect position, GUIContent label, Object obj, bool is_allow_sceneObjects)
      where T : Object
    {
      return (T) EditorGUI.ObjectField(position, obj, typeof(T), is_allow_sceneObjects);
    }
  }
}
#endif