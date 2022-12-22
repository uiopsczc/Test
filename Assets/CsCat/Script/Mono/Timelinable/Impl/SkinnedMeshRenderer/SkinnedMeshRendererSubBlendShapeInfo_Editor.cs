#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class SkinnedMeshRendererSubBlendShapeInfo
	{
		public virtual void DrawGUISetting(Rect rect, string[] skinnedMeshRendererNames, string[][] blendShapeNames,
			float singleLineHeight, float padding)
		{
			rect.height -= 2 * padding;
			rect.y += padding;
			if (skinnedMeshRendererNames.IsNullOrEmpty() ||
			    skinnedMeshRendererIndex >= skinnedMeshRendererNames.Length)
				return;
			skinnedMeshRendererIndex =
				EditorGUI.Popup(new Rect(rect.x, rect.y, rect.width, singleLineHeight), "skinnedMeshRendererName",
					skinnedMeshRendererIndex, skinnedMeshRendererNames);
			if (blendShapeNames.IsNullOrEmpty() || blendShapeNames[skinnedMeshRendererIndex].IsNullOrEmpty() ||
			    blendShapeIndex >= blendShapeNames[skinnedMeshRendererIndex].Length)
				return;
			blendShapeIndex = EditorGUI.Popup(
				new Rect(rect.x, rect.y + singleLineHeight, rect.width, singleLineHeight),
				"blendShapeName", blendShapeIndex, blendShapeNames[skinnedMeshRendererIndex]);
		}
	}
}
#endif