#if MicroTileMap
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CsCat
{
public partial class TileSetControl
{
  private TileView tileView;
  public void DrawTileView()
  {
    CreateTileViewReorderableList();
    DrawTileViewReorderableList();
  }

  private void CreateTileViewReorderableList()
  {
    if (sharedTileSetData.tileView_reorderableList == null || sharedTileSetData.tileView_reorderableList.list != tileSet.tileView_list)
    {
      sharedTileSetData.tileView_reorderableList = CreateTileViewReorderableList(tileSet);
      sharedTileSetData.tileView_reorderableList.onSelectCallback += (ReorderableList list) => { RemoveTileSelection(); };
      sharedTileSetData.tileView_reorderableList.onRemoveCallback += (ReorderableList list) => { RemoveTileSelection(); };
    }
  }

  public static ReorderableList CreateTileViewReorderableList(TileSet tileSet)
  {
    ReorderableList tileView_reorderableList = new ReorderableList(tileSet.tileView_list, typeof(TileView), true, true, true, true);
    tileView_reorderableList.onAddDropdownCallback = (Rect buttonRect, ReorderableList reorderableList) =>
    {
      GenericMenu menu = new GenericMenu();
      GenericMenu.MenuFunction add_tileSelection_func = () =>
      {
        TileSelection tileSelection = tileSet.tileSelection.Clone();
        tileSelection.FlipVertical(); // flip vertical to fit the tileset coordinate system ( from top to bottom )                   
        tileSet.AddTileView("new TileView", tileSelection);
        EditorUtility.SetDirty(tileSet);
      };
      GenericMenu.MenuFunction add_tileSetBrushSelection_func = () =>
      {
        TileSelection tileSelection = TileSetBrushBehaviour.CreateTileSelection();
        tileSet.AddTileView("new TileView", tileSelection);
        EditorUtility.SetDirty(tileSet);
      };
      GenericMenu.MenuFunction remove_all_tileViews_func = () =>
      {
        if (EditorUtility.DisplayDialog("警告!", "是否删除全部的TileViews?", "Yes", "No"))
        {
          tileSet.RemoveAllTileViewList();
          EditorUtility.SetDirty(tileSet);
        }
      };
      if (tileSet.tileSelection != null)
        menu.AddItem(new GUIContent("添加TileSelection"), false, add_tileSelection_func);
      else
        menu.AddDisabledItem(new GUIContent("添加TileSelection到TileView"));

      if (TileSetBrushBehaviour.GetTileSetBrushTileSet() == tileSet && TileSetBrushBehaviour.CreateTileSelection() != null)
        menu.AddItem(new GUIContent("Add TileSetBrush Selection"), false, add_tileSetBrushSelection_func);

      menu.AddSeparator("");
      menu.AddItem(new GUIContent("删除全部TileViews"), false, remove_all_tileViews_func);
      menu.AddSeparator("");
      menu.AddItem(new GUIContent("按Name排序"), false, tileSet.SortTileViewListByName);
      menu.ShowAsContext();
    };
    tileView_reorderableList.onRemoveCallback = (ReorderableList reorderableList) =>
    {
      if (EditorUtility.DisplayDialog("警告!", "是否确定删除这个TileView?", "Yes", "No"))
      {
        ReorderableList.defaultBehaviours.DoRemoveButton(reorderableList);
        EditorUtility.SetDirty(tileSet);
      }
    };
    tileView_reorderableList.drawHeaderCallback = (Rect rect) =>
    {
      EditorGUI.LabelField(rect, "TileViews", EditorStyles.boldLabel);
      Texture2D button_texture = tileView_reorderableList.elementHeight == 0f ? EditorGUIUtility.FindTexture("winbtn_win_max_h") : EditorGUIUtility.FindTexture("winbtn_win_min_h");
      if (GUI.Button(new Rect(rect.width - rect.height, rect.y, rect.height, rect.height), button_texture, EditorStyles.label))
      {
        tileView_reorderableList.elementHeight = tileView_reorderableList.elementHeight == 0f ? EditorGUIUtility.singleLineHeight : 0f;
        tileView_reorderableList.draggable = tileView_reorderableList.elementHeight > 0f;
      }
    };
    tileView_reorderableList.drawElementCallback = (Rect rect, int index, bool is_active, bool is_focused) =>
    {
      if (tileView_reorderableList.elementHeight == 0f)
        return;
      Rect label_rect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
      TileView tileView = tileView_reorderableList.list[index] as TileView;
      if (index == tileView_reorderableList.index)
      {
        string new_name = EditorGUI.TextField(label_rect, tileView.name);
        if (new_name != tileView.name)
          tileSet.RenameTileView(tileView.name, new_name);
      }
      else
        EditorGUI.LabelField(label_rect, tileView.name);
    };

    return tileView_reorderableList;
  }


  private void DrawTileViewReorderableList()
  {
    var e = Event.current;
    using (new GUIColorScope(Color.white))
    {
      using (new GUILayoutBeginVerticalScope(GUIStyleConst.Box_Style))
      {
        sharedTileSetData.tileView_reorderableList.index = Mathf.Clamp(sharedTileSetData.tileView_reorderableList.index, -1, tileSet.tileView_list.Count - 1);
        sharedTileSetData.tileView_reorderableList.DoLayoutList();
        Rect tileView_reorderableList = GUILayoutUtility.GetLastRect();
        if (e.isMouse && !tileView_reorderableList.Contains(e.mousePosition))
          sharedTileSetData.tileView_reorderableList.ReleaseKeyboardFocus();
      }
    }
  }
}
}
#endif