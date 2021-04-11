#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class MountPrefabInfo
  {
    public virtual void DrawGUISetting(Rect rect, float single_line_height, float padding)
    {
      rect.height -= 2 * padding;
      rect.y += padding;
      //只能使用Rect，否则用AreaScope会出错
      using (new EditorGUILabelWidthScope(40))
      {
        prefab = EditorGUIUtil.ObjectField<GameObject>(
          new Rect(rect.x, rect.y, rect.width, single_line_height),
          "prefab", prefab, false);
      }

      localPosition = EditorGUI.Vector3Field(
        new Rect(rect.x, rect.y + 1 * single_line_height, rect.width, single_line_height),
        "localPosition", localPosition);
      localEulerAngles = EditorGUI.Vector3Field(
        new Rect(rect.x, rect.y + 3 * single_line_height, rect.width, single_line_height),
        "localEulerAngles", localEulerAngles);
      localScale = EditorGUI.Vector3Field(
        new Rect(rect.x, rect.y + 5 * single_line_height, rect.width, single_line_height),
        "localScale", localScale);
    }
  }
}
#endif