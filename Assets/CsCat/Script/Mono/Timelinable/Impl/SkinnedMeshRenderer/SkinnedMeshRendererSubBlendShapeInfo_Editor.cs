#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class SkinnedMeshRendererSubBlendShapeInfo
	{
		public virtual void DrawGUISetting(Rect rect, string[] skinnedMeshRendererNames, string[][] blendShapNames,
			float singleLineHeight, float padding)
		{
			rect.height -= 2 * padding;
			rect.y += padding;
			if (skinnedMeshRendererNames.IsNullOrEmpty() ||
			    skinnedMeshRendererIndex >= skinnedMeshRendererNames.Length)
				return;
			skinnedMeshRendererIndex =
				EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, singleLineHeight), "skinnedMeshRenderer_name",
					skinnedMeshRendererIndex, skinnedMeshRendererNames);
			if (blendShapNames.IsNullOrEmpty() || blendShapNames[skinnedMeshRendererIndex].IsNullOrEmpty() ||
			    blendShape_index >= blendShapNames[skinnedMeshRendererIndex].Length)
				return;
			blendShape_index = EditorGUI.Popup(
				new Rect(rect.x, rect.y + singleLineHeight, rect.width, singleLineHeight),
				"blendShape_name", blendShape_index, blendShapNames[skinnedMeshRendererIndex]);
		}
	}
}
#endif