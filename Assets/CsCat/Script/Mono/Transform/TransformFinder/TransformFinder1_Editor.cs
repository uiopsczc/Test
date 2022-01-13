#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TransformFinder1
	{
		public override void DrawGUI()
		{
			using (new EditorGUILabelWidthScope(100))
			{
				humanBodyBones = (HumanBodyBones) EditorGUILayout.Popup("humanBodyBones", (int) humanBodyBones,
					EnumUtil.GetNames<HumanBodyBones>().ToArray());
			}
		}
	}
}
#endif