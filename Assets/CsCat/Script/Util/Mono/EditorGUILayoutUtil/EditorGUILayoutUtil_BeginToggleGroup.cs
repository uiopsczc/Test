#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUILayoutUtil
  {
    public static EditorGUILayoutBeginToggleGroupScope BeginToggleGroup(GUIContent label, ref bool toggle)
    {
      return new EditorGUILayoutBeginToggleGroupScope(label, ref toggle);
    }

    public static EditorGUILayoutBeginToggleGroupScope BeginToggleGroup(string label, ref bool toggle)
    {
      return new EditorGUILayoutBeginToggleGroupScope(label, ref toggle);
    }
  }
}
#endif