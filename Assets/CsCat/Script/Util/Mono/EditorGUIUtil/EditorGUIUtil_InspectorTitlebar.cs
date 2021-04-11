#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static bool InspectorTitlebar(Rect position, ref bool is_foldout, Object target_obj, bool is_expandable)
    {
      return EditorGUIInspectorTitlebarScope.InspectorTitlebar(position, ref is_foldout, target_obj, is_expandable);
    }

    public static bool InspectorTitlebar(Rect position, ref bool is_foldout, Object[] target_objs, bool is_expandable)
    {
      return EditorGUIInspectorTitlebarScope.InspectorTitlebar(position, ref is_foldout, target_objs, is_expandable);
    }
  }
}
#endif