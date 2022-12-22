#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;

namespace CsCat
{
	public partial class SkinnedMeshRendererTimelinableItemInfo
	{
		[NonSerialized] private ReorderableListInfo _skinnedMeshRendererSubBlendShapeInfoReorderableListInfo;

		public ReorderableListInfo skinnedMeshRendererSubBlendShapeInfoReorderableListInfo => _skinnedMeshRendererSubBlendShapeInfoReorderableListInfo ??
		                                                                                      (_skinnedMeshRendererSubBlendShapeInfoReorderableListInfo =
			                                                                                      new ReorderableListInfo(skinnedMeshRendererSubBlendShapeInfoList));


		public override void DrawGUISettingDetail()
		{
			base.DrawGUISettingDetail();
			using (new GUIEnabledScope(false))
			{
				EditorGUILayout.FloatField("blendDuration", duration);
			}

			var skinnedMeshRendererNames = new string[skinnedMeshRendererList.Count];
			var blendShapeNames = new string[skinnedMeshRendererList.Count][];
			for (int i = 0; i < skinnedMeshRendererList.Count; i++)
			{
				var skinnedMeshRenderer = skinnedMeshRendererList[i];
				if (skinnedMeshRenderer == null)
					continue;
				skinnedMeshRendererNames[i] = skinnedMeshRenderer.name;
				int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
				blendShapeNames[i] = new string[blendShapeCount];
				for (int j = 0; j < blendShapeCount; j++)
					blendShapeNames[i][j] = skinnedMeshRenderer.sharedMesh.GetBlendShapeName(j);
			}

			if (skinnedMeshRendererNames.IsNullOrEmpty() || skinnedMeshRendererIndex >= skinnedMeshRendererNames.Length)
				return;

			skinnedMeshRendererIndex =
			  EditorGUILayout.Popup("skinnedMeshRendererName", skinnedMeshRendererIndex, skinnedMeshRendererNames);

			if (blendShapeNames.IsNullOrEmpty() || blendShapeNames[skinnedMeshRendererIndex].IsNullOrEmpty() ||
				blendShapeIndex >= blendShapeNames[skinnedMeshRendererIndex].Length)
				return;
			blendShapeIndex =
			  EditorGUILayout.Popup("blendShapeName", blendShapeIndex, blendShapeNames[skinnedMeshRendererIndex]);
			blendShapeWeight = EditorGUILayout.Slider("Weight", blendShapeWeight, 0, 100);


			//Draw subBlendShapeInfo_list
			skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height * 2 +
																					  2 * ReorderableListConst.Padding);
			skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.reorderableList.drawElementCallback =
			  (rect, index, isActive, isFocused) =>
			  {
				  var skinnedMeshRendererSubBlendShapeInfo =
			  skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.reorderableList.list[index] as
				SkinnedMeshRendererSubBlendShapeInfo;
				  skinnedMeshRendererSubBlendShapeInfo.DrawGUISetting(rect, skinnedMeshRendererNames, blendShapeNames,
			  EditorConst.Single_Line_Height,
			  ReorderableListConst.Padding);
			  };
			skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.DrawGUI("skinnedMeshRendererSubBlendShapeInfoList");
		}
	}
}
#endif