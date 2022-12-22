
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;

namespace CsCat
{
	public partial class MountTimelinableItemInfo
	{
		[NonSerialized] private ReorderableListInfo _mountPrefabInfoReorderableListInfo;

		private ReorderableListInfo mountPrefabInfoReorderableListInfo => _mountPrefabInfoReorderableListInfo ?? (_mountPrefabInfoReorderableListInfo =
			                                                                  new ReorderableListInfo(mountPrefabInfoList));


		public override void DrawGUISettingDetail()
		{
			base.DrawGUISettingDetail();
			mountPointTransformFinderIndex = EditorGUILayout.Popup("transformFinder",
			  mountPointTransformFinderIndex,
			  TransformFinderConst.transformFinderInfoList.ConvertAll(t => t.name).ToArray());
			mountPointTransformFinder.DrawGUI();

			//Draw mountPrefabInfo_list
			mountPrefabInfoReorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height * 7 +
																 2 * ReorderableListConst.Padding);
			mountPrefabInfoReorderableListInfo.reorderableList.drawElementCallback =
			  (rect, index, isActive, isFocused) =>
			  {
				  var mountPrefabInfo = mountPrefabInfoReorderableListInfo.reorderableList.list[index] as MountPrefabInfo;
				  mountPrefabInfo.DrawGUISetting(rect, EditorConst.Single_Line_Height, ReorderableListConst.Padding);
			  };
			mountPrefabInfoReorderableListInfo.DrawGUI("mountPrefabInfoList");
		}
	}
}
#endif