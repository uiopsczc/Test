using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static T ObjectField<T>(Object obj, bool is_allow_sceneObjects, params GUILayoutOption[] options)
      where T : Object
    {
      return (T) EditorGUILayout.ObjectField(obj, typeof(T), is_allow_sceneObjects, options);
    }

    public static T ObjectField<T>(string label, Object obj, bool is_allow_sceneObjects,
      params GUILayoutOption[] options) where T : Object
    {
      return (T) EditorGUILayout.ObjectField(label, obj, typeof(T), is_allow_sceneObjects, options);
    }

    public static T ObjectField<T>(GUIContent label, Object obj, bool is_allow_sceneObjects,
      params GUILayoutOption[] options) where T : Object
    {
      return (T) EditorGUILayout.ObjectField(label, obj, typeof(T), is_allow_sceneObjects, options);
    }
  }
}
#endif