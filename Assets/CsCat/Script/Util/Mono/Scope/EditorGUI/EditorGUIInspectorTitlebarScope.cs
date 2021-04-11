
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
namespace CsCat
{
  public class EditorGUIInspectorTitlebarScope
  {
    public static bool InspectorTitlebar(Rect position, ref bool is_foldout, Object target_obj, bool is_expandable)
    {
      is_foldout = EditorGUI.InspectorTitlebar(position, is_foldout, target_obj, is_expandable);
      return is_foldout;
    }

    public static bool InspectorTitlebar(Rect position, ref bool is_foldout, Object[] target_objs, bool is_expandable)
    {
      is_foldout = EditorGUI.InspectorTitlebar(position, is_foldout, target_objs, is_expandable);
      return is_foldout;
    }
  }
}
#endif