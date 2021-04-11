#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static GUIContent ObjectContent<T>()
    {
      return EditorGUIUtility.ObjectContent(null, typeof(T));
    }
  }
}
#endif