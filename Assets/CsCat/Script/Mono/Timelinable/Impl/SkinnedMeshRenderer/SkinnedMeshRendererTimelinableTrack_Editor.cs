
#if UNITY_EDITOR
using System;
using UnityEngine;

namespace CsCat
{
	public partial class SkinnedMeshRendererTimelinableTrack
	{
		[NonSerialized]
		private ReorderableListInfo _skinnedMeshRendererReorderableListInfo;
		public ReorderableListInfo skinnedMeshRendererReorderableListInfo => _skinnedMeshRendererReorderableListInfo ?? (_skinnedMeshRendererReorderableListInfo =
			                                                                     new ReorderableListInfo(skinnedMeshRendererList));

		public override void DrawGUISetting_Detail()
		{
			base.DrawGUISetting_Detail();

			//draw skinnedMeshRenderer_list
			skinnedMeshRendererReorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height + 2 * ReorderableListConst.Padding);
			skinnedMeshRendererReorderableListInfo.reorderableList.drawElementCallback =
			  (rect, index, isActive, isFocused) =>
			  {
				  rect.height -= 2 * ReorderableListConst.Padding;
				  rect.y += ReorderableListConst.Padding;
			//只能使用Rect，否则用AreaScope会出错
			using (new EditorGUILabelWidthScope(130))
				  {
					  skinnedMeshRendererReorderableListInfo.reorderableList.list[index] = EditorGUIUtil.ObjectField<SkinnedMeshRenderer>(
				  new Rect(rect.x, rect.y, rect.width, EditorConst.Single_Line_Height),
				  "skinnedMeshRenderer", skinnedMeshRendererReorderableListInfo.reorderableList.list[index] as SkinnedMeshRenderer, true);
				  }
			  };
			skinnedMeshRendererReorderableListInfo.DrawGUI("skinnedMeshRenderer_list");
			SetSkinnedMeshRendererListOfItemInfoes();

		}
	}
}
#endif