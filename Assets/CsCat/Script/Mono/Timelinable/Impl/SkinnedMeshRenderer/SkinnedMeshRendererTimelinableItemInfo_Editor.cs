#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;

namespace CsCat
{
	public partial class SkinnedMeshRendererTimelinableItemInfo
	{
		[NonSerialized] private ReorderableListInfo _skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo;

		public ReorderableListInfo skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo
		{
			get
			{
				if (_skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo == null)
					_skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo =
					  new ReorderableListInfo(skinnedMeshRendererSubBlendShapeInfo_list);
				return _skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo;
			}
		}


		public override void DrawGUISetting_Detail()
		{
			base.DrawGUISetting_Detail();
			using (new GUIEnabledScope(false))
			{
				EditorGUILayout.FloatField("blend_druation", duration);
			}

			var skinnedMeshRenderer_names = new string[skinnedMeshRenderer_list.Count];
			var blendShap_names = new string[skinnedMeshRenderer_list.Count][];
			for (int i = 0; i < skinnedMeshRenderer_list.Count; i++)
			{
				var skinnedMeshRenderer = skinnedMeshRenderer_list[i];
				if (skinnedMeshRenderer == null)
					continue;
				skinnedMeshRenderer_names[i] = skinnedMeshRenderer.name;
				int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
				blendShap_names[i] = new string[blendShapeCount];
				for (int j = 0; j < blendShapeCount; j++)
					blendShap_names[i][j] = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(j);
			}

			if (skinnedMeshRenderer_names.IsNullOrEmpty() || skinnedMeshRenderer_index >= skinnedMeshRenderer_names.Length)
				return;

			skinnedMeshRenderer_index =
			  EditorGUILayout.Popup("skinnedMeshRenderer_name", skinnedMeshRenderer_index, skinnedMeshRenderer_names);

			if (blendShap_names.IsNullOrEmpty() || blendShap_names[skinnedMeshRenderer_index].IsNullOrEmpty() ||
				blendShape_index >= blendShap_names[skinnedMeshRenderer_index].Length)
				return;
			blendShape_index =
			  EditorGUILayout.Popup("blendShape_name", blendShape_index, blendShap_names[skinnedMeshRenderer_index]);
			blendShape_weight = EditorGUILayout.Slider("Weight", blendShape_weight, 0, 100);


			//Draw subBlendShapeInfo_list
			skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height * 2 +
																					  2 * ReorderableListConst.Padding);
			skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.reorderableList.drawElementCallback =
			  (rect, index, isActive, isFocused) =>
			  {
				  var skinnedMeshRendererSubBlendShapeInfo =
			  skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.reorderableList.list[index] as
				SkinnedMeshRendererSubBlendShapeInfo;
				  skinnedMeshRendererSubBlendShapeInfo.DrawGUISetting(rect, skinnedMeshRenderer_names, blendShap_names,
			  EditorConst.Single_Line_Height,
			  ReorderableListConst.Padding);
			  };
			skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.DrawGUI("skinnedMeshRendererSubBlendShapeInfo_list");
		}
	}
}
#endif