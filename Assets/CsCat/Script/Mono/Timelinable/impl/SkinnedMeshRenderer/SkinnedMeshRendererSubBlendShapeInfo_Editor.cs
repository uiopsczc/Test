#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public partial class SkinnedMeshRendererSubBlendShapeInfo
  {
    public virtual void DrawGUISetting(Rect rect, string[] skinnedMeshRenderer_names, string[][] blendShap_names,
      float single_line_height, float padding)
    {
      rect.height -= 2 * padding;
      rect.y += padding;
      if (skinnedMeshRenderer_names.IsNullOrEmpty() || skinnedMeshRenderer_index >= skinnedMeshRenderer_names.Length)
        return;
      skinnedMeshRenderer_index =
        EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, single_line_height), "skinnedMeshRenderer_name",
          skinnedMeshRenderer_index, skinnedMeshRenderer_names);
      if (blendShap_names.IsNullOrEmpty() || blendShap_names[skinnedMeshRenderer_index].IsNullOrEmpty() ||
          blendShape_index >= blendShap_names[skinnedMeshRenderer_index].Length)
        return;
      blendShape_index = EditorGUI.Popup(new Rect(rect.x, rect.y + single_line_height, rect.width, single_line_height),
        "blendShape_name", blendShape_index, blendShap_names[skinnedMeshRenderer_index]);
    }
  }
}
#endif