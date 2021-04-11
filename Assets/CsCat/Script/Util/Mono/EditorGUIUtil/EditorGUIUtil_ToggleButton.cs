#if UNITY_EDITOR
using UnityEngine;

namespace CsCat
{
  public partial class EditorGUIUtil
  {
    public static bool ToggleButton(string label, bool value)
    {
      GUIStyle button_style = (GUIStyle) "Button";
      if (GUILayout.Button(label,
        value ? new GUIStyle("Button") {normal = {background = button_style.active.background}} : (GUIStyle) "Button"))
        value = !value;
      return value;
    }
  }
}
#endif