
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditorInternal;

namespace CsCat
{
  public partial class MountTimelinableItemInfo
  {
    [NonSerialized] private ReorderableListInfo _mountPrefabInfo_reorderableListInfo;

    private ReorderableListInfo mountPrefabInfo_reorderableListInfo
    {
      get
      {
        if (_mountPrefabInfo_reorderableListInfo == null)
          _mountPrefabInfo_reorderableListInfo = new ReorderableListInfo(mountPrefabInfo_list);
        return _mountPrefabInfo_reorderableListInfo;
      }
    }


    public override void DrawGUISetting_Detail()
    {
      base.DrawGUISetting_Detail();
      mount_point_transformFinder_index = EditorGUILayout.Popup("transformFinder",
        mount_point_transformFinder_index,
        TransformFinderConst.transformFinderInfo_list.ConvertAll(t => t.name).ToArray());
      mount_point_transformFinder.DrawGUI();

      //Draw mountPrefabInfo_list
      mountPrefabInfo_reorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height * 7 +
                                                           2 * ReorderableListConst.Padding);
      mountPrefabInfo_reorderableListInfo.reorderableList.drawElementCallback =
        (rect, index, isActive, isFocused) =>
        {
          var mountPrefabInfo = mountPrefabInfo_reorderableListInfo.reorderableList.list[index] as MountPrefabInfo;
          mountPrefabInfo.DrawGUISetting(rect, EditorConst.Single_Line_Height, ReorderableListConst.Padding);
        };
      mountPrefabInfo_reorderableListInfo.DrawGUI("mountPrefabInfo_list");
    }
  }
}
#endif