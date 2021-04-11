
#if UNITY_EDITOR
using System;
using UnityEngine;

namespace CsCat
{
  public partial class SkinnedMeshRendererTimelinableTrack
  {
    [NonSerialized]
    private ReorderableListInfo _skinnedMeshRenderer_reorderableListInfo;
    public ReorderableListInfo skinnedMeshRenderer_reorderableListInfo { get { if (_skinnedMeshRenderer_reorderableListInfo == null) _skinnedMeshRenderer_reorderableListInfo = new ReorderableListInfo(skinnedMeshRenderer_list); return _skinnedMeshRenderer_reorderableListInfo; }
    }

    public override void DrawGUISetting_Detail()
    {
      base.DrawGUISetting_Detail();

      //draw skinnedMeshRenderer_list
      skinnedMeshRenderer_reorderableListInfo.SetElementHeight(EditorConst.Single_Line_Height+ 2 * ReorderableListConst.Padding);
      skinnedMeshRenderer_reorderableListInfo.reorderableList.drawElementCallback =
        (rect, index, isActive, isFocused) =>
        {
          rect.height -= 2 * ReorderableListConst.Padding;
          rect.y += ReorderableListConst.Padding;
          //只能使用Rect，否则用AreaScope会出错
          using (new EditorGUILabelWidthScope(130))
          {
            skinnedMeshRenderer_reorderableListInfo.reorderableList.list[index] = EditorGUIUtil.ObjectField<SkinnedMeshRenderer>(
              new Rect(rect.x, rect.y, rect.width, EditorConst.Single_Line_Height),
              "skinnedMeshRenderer", skinnedMeshRenderer_reorderableListInfo.reorderableList.list[index] as SkinnedMeshRenderer, true);
          }
        };
      skinnedMeshRenderer_reorderableListInfo.DrawGUI("skinnedMeshRenderer_list");
      SetSkinnedMeshRendererList_Of_ItemInfoes();

    }
  }
}
#endif