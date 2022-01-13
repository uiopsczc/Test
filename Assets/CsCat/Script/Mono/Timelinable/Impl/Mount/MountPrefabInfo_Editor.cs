#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class MountPrefabInfo
	{
		public virtual void DrawGUISetting(Rect rect, float singleLineHeight, float padding)
		{
			rect.height -= 2 * padding;
			rect.y += padding;
			//只能使用Rect，否则用AreaScope会出错
			using (new EditorGUILabelWidthScope(40))
			{
				prefab = EditorGUIUtil.ObjectField<GameObject>(
					new Rect(rect.x, rect.y, rect.width, singleLineHeight),
					"prefab", prefab, false);
			}

			localPosition = EditorGUI.Vector3Field(
				new Rect(rect.x, rect.y + 1 * singleLineHeight, rect.width, singleLineHeight),
				"localPosition", localPosition);
			localEulerAngles = EditorGUI.Vector3Field(
				new Rect(rect.x, rect.y + 3 * singleLineHeight, rect.width, singleLineHeight),
				"localEulerAngles", localEulerAngles);
			localScale = EditorGUI.Vector3Field(
				new Rect(rect.x, rect.y + 5 * singleLineHeight, rect.width, singleLineHeight),
				"localScale", localScale);
		}
	}
}
#endif