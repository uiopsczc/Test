#if MicroTileMap
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CsCat
{
public partial class TileSetControl
{
  private Rect tileSetBrush_palette_scroll_area_rect;
  private TileSetBrush selected_tileSetBrush_in_inspector;
  private bool is_display_tileSetBrush_reordableList = false;

  public void DisplayTileSetBrush()
  {
    var e = Event.current;
    bool is_mouse_inside_tileSetBrush_palette_scroll_area =
      e.isMouse && tileSetBrush_palette_scroll_area_rect.Contains(e.mousePosition);

    using (new EditorGUILayoutBeginHorizontalScope())
    {
      string tileSetBrushId_label = tileSet.selected_tileSetBrushId > 0
        ? string.Format("(id:{0})", tileSet.selected_tileSetBrushId)
        : "";
      EditorGUILayout.LabelField("TileSetBrush Palette" + tileSetBrushId_label, EditorStyles.boldLabel);
      is_display_tileSetBrush_reordableList =
        GUILayoutUtil.ToggleButton("Display List", is_display_tileSetBrush_reordableList);
    }

//    tileSet.tileSetBrush_type_mask = EditorGUILayout.MaskField("TileSetBrush Mask", tileSet.tileSetBrush_type_mask,
//      tileSet.GetTileSetBrushTypeArray());

    int column_count = (int) (tile_scroll_size_rect.width / (tileSet.visual_tile_size.x + visual_tile_padding));
    if (column_count <= 0)
      column_count = 1;
    float tileSetBrushes_scroll_max_height = Screen.height / 4;
    //commented because m_rTileScrollSize.width.height was changed to Screen.height;  fBrushesScrollMaxHeight -= fBrushesScrollMaxHeight % 2; // sometimes because size of tile scroll affects size of brush scroll, the height is dancing between +-1, so this is always taking the pair value
    float tileSetBrush_palette_scroll_area_height = Mathf.Min(tileSetBrushes_scroll_max_height,
      4 + (tileSet.visual_tile_size.y + visual_tile_padding) * (1 + (tileSet.tileSetBrush_list.Count / column_count)));

    using (new EditorGUILayoutBeginVerticalScope(GUILayout.MinHeight(tileSetBrush_palette_scroll_area_height)))
    {
      if (is_display_tileSetBrush_reordableList)
        DisplayTileSetBrushReorderableList();
      else
      {
        bool is_refresh_tileSetBrushList = false;
        Vector2 tileSetBrushes_scroll_pos = sharedTileSetData.tileSetBrushes_scroll_pos;
        using (new EditorGUILayoutBeginScrollViewScope(ref tileSetBrushes_scroll_pos, GUIStyleConst.Scroll_Style))
        {
          if (is_update_scroll_pos)
            sharedTileSetData.tileSetBrushes_scroll_pos = tileSetBrushes_scroll_pos;

          Rect scroll_view_rect = new Rect(0, 0, tile_scroll_size_rect.width, 0);
          column_count =
            Mathf.Clamp((int) scroll_view_rect.width / (int) (tileSet.visual_tile_size.x + visual_tile_padding), 1,
              column_count);
          if (tileSet.tileSetBrush_list != null)
          {
            GUILayout.Space((tileSet.visual_tile_size.y + visual_tile_padding) *
                            (1 + (tileSet.tileSetBrush_list.Count - 1) / column_count));
            for (int i = 0, index = 0; i < tileSet.tileSetBrush_list.Count; ++i, ++index)
            {
              var tileSetBrushContainer = tileSet.tileSetBrush_list[i];
              if (tileSetBrushContainer.tileSetBrush == null || tileSetBrushContainer.tileSetBrush.tileSet != tileSet)
              {
                is_refresh_tileSetBrushList = true;
                continue;
              }

              if (!tileSetBrushContainer.tileSetBrush.is_show_in_palette ||
                  !tileSet.IsTileSetBrushVisibleByTypeMask(tileSetBrushContainer.tileSetBrush))
              {
                --index;
                continue;
              }

              int tile_x = index % column_count;
              int tile_y = index / column_count;
              Rect visual_tile_rect = new Rect(2 + tile_x * (tileSet.visual_tile_size.x + visual_tile_padding),
                2 + tile_y * (tileSet.visual_tile_size.y + visual_tile_padding), tileSet.visual_tile_size.x,
                tileSet.visual_tile_size.y);
              //Fix Missing Tileset reference
              if (tileSetBrushContainer.tileSetBrush.tileSet == null)
              {
                LogCat.warn(string.Format("Fixed missing tileset reference in brush {0}  Id({1})",
                  tileSetBrushContainer.tileSetBrush.name, tileSetBrushContainer.id));
                tileSetBrushContainer.tileSetBrush.tileSet = tileSet;
              }

              uint tileData = TileSetConst.TileData_Empty;
              if (tileSetBrushContainer.tileSetBrush.IsAnimated())
                tileData = tileSetBrushContainer.tileSetBrush.GetAnimTileData();
              else
                tileData = tileSetBrushContainer.tileSetBrush.PreviewTileData();
              TileSetEditor.DoGUIDrawTileFromTileData(visual_tile_rect, tileData, tileSet,
                tileSetBrushContainer.tileSetBrush.GetAnimUV());
              if ((is_left_mouse_released || is_right_mouse_released || mouseDoubleClick.IsDoubleClick) //左键放开，右键放开，双击鼠标
                  && is_mouse_inside_tileSetBrush_palette_scroll_area && visual_tile_rect.Contains(Event.current.mousePosition))//点钟该区域
              {
                tileSet.selected_tileSetBrushId = tileSetBrushContainer.id;
                RemoveTileSelection();
                if (mouseDoubleClick.IsDoubleClick)
                {
                  EditorGUIUtility.PingObject(tileSetBrushContainer.tileSetBrush);
                  selected_tileSetBrush_in_inspector = tileSetBrushContainer.tileSetBrush;
                }

                if (is_right_mouse_released)
                  TilePropertiesEditor.Show(tileSet);
              }
              else if (tileSet.selected_tileSetBrushId == tileSetBrushContainer.id)
              {
                Rect selection_rect = new Rect(2 + tile_x * (tileSet.visual_tile_size.x + visual_tile_padding),
                  2 + tile_y * (tileSet.visual_tile_size.y + visual_tile_padding),
                  (tileSet.visual_tile_size.x + visual_tile_padding),
                  (tileSet.visual_tile_size.y + visual_tile_padding));
                DrawUtil.HandlesDrawSolidRectangleWithOutline(selection_rect, new Color(0f, 0f, 0f, 0.1f),
                  new Color(1f, 1f, 0f, 1f));
              }
            }


            if (is_refresh_tileSetBrushList)
            {
              tileSet.RemoveInvalidTileSetBrushList();
              tileSet.UpdateTileSetBrushTypeArray();
            }
          }
        }

        if (e.type == EventType.Repaint)
          tileSetBrush_palette_scroll_area_rect = GUILayoutUtility.GetLastRect();
      }
    }

    if (GUILayout.Button("Import all tileSetBrush_list found in the project"))
    {
      AddAllTileSetBrushesFoundInTheProject(tileSet);
      EditorUtility.SetDirty(tileSet);
    }
  }


  public static ReorderableList CreateTileSetBrushReorderableList(TileSet tileSet)
  {
    ReorderableList tileSetBrush_reorderableList = new ReorderableList(tileSet.tileSetBrush_list,
      typeof(TileSetBrushContainer), true, true, true, true);
    tileSetBrush_reorderableList.displayAdd = tileSetBrush_reorderableList.displayRemove = false;
    tileSetBrush_reorderableList.drawHeaderCallback = (Rect rect) =>
    {
      EditorGUI.LabelField(rect, "TileSetBrushes", EditorStyles.boldLabel);
    };
    tileSetBrush_reorderableList.drawElementCallback = (Rect rect, int index, bool is_active, bool is_focused) =>
    {
      var tileSetBrushContainer = tileSet.tileSetBrush_list[index];
      if (tileSetBrushContainer.tileSetBrush)
      {
        Rect toggle_rect = new Rect(rect.x, rect.y, 16f, EditorGUIUtility.singleLineHeight);
        Rect tile_rect = new Rect(rect.x + 16f, rect.y, tileSet.visual_tile_size.x, tileSet.visual_tile_size.y);
        Rect tileId_rect = tile_rect;
        tileId_rect.x += tile_rect.width + 20;
        tileId_rect.width = rect.width - tileId_rect.x;
        tileId_rect.height = rect.height / 2;

        Rect tileUV = tileSetBrushContainer.tileSetBrush.GetAnimUV();
        if (tileUV != default(Rect))
        {
          GUI.Box(new Rect(tile_rect.position - Vector2.one, tile_rect.size + 2 * Vector2.one), "");
          GUI.DrawTextureWithTexCoords(tile_rect, tileSet.atlas_texture, tileUV, true);
        }

        tileSetBrushContainer.tileSetBrush.is_show_in_palette = EditorGUI.Toggle(toggle_rect, GUIContent.none,
          tileSetBrushContainer.tileSetBrush.is_show_in_palette, GUIStyleConst.Visible_Toggle_Style);
        GUI.Label(tileId_rect,
          string.Format("Id({0}){1}", tileSetBrushContainer.id, tileSetBrushContainer.tileSetBrush.name));
      }
      else
        tileSet.RemoveInvalidTileSetBrushList();
    };

    return tileSetBrush_reorderableList;
  }


  private void DisplayTileSetBrushReorderableList()
  {
    Event e = Event.current;
    if (sharedTileSetData.tileSetBrush_reorderableList == null ||
        sharedTileSetData.tileSetBrush_reorderableList.list != tileSet.tileSetBrush_list)
    {
      if (e.type != EventType.Layout)
      {
        sharedTileSetData.tileSetBrush_reorderableList = CreateTileSetBrushReorderableList(tileSet);
        sharedTileSetData.tileSetBrush_reorderableList.onSelectCallback += (ReorderableList reorderableList) =>
        {
          var tileSetBrush_list = tileSet.tileSetBrush_list[reorderableList.index];
          tileSet.selected_tileSetBrushId = tileSetBrush_list.id;
          RemoveTileSelection();
          if (mouseDoubleClick.IsDoubleClick)
          {
            EditorGUIUtility.PingObject(tileSetBrush_list.tileSetBrush);
            selected_tileSetBrush_in_inspector = tileSetBrush_list.tileSetBrush;
          }
        };
      }
    }
    else
    {
      using (new GUILayoutBeginVerticalScope(GUIStyleConst.Box_Style))
      {
        sharedTileSetData.tileSetBrush_reorderableList.index =
          tileSet.tileSetBrush_list.FindIndex(x => x.id == tileSet.selected_tileSetBrushId);
        sharedTileSetData.tileSetBrush_reorderableList.index = Mathf.Clamp(
          sharedTileSetData.tileSetBrush_reorderableList.index, -1, tileSet.tileSetBrush_list.Count - 1);
        sharedTileSetData.tileSetBrush_reorderableList.elementHeight = tileSet.visual_tile_size.y + 10f;
        sharedTileSetData.tileSetBrush_reorderableList.DoLayoutList();
        Rect list_rect = GUILayoutUtility.GetLastRect();
        if (e.isMouse && !list_rect.Contains(e.mousePosition))
          sharedTileSetData.tileSetBrush_reorderableList.ReleaseKeyboardFocus();
      }
    }
  }


  public static void AddAllTileSetBrushesFoundInTheProject(TileSet tileSet)
  {
    // Load all TilesetBrush assets found in the project
    string[] guids = AssetDatabase.FindAssets("t:TilesetBrush");
    foreach (string tileSetBrush_guid in guids)
    {
      string tileSetBrush_assetPath = AssetDatabase.GUIDToAssetPath(tileSetBrush_guid);
      AssetDatabase.LoadAssetAtPath<TileSetBrush>(tileSetBrush_assetPath);
    }

    // Get all loaded brushes
    TileSetBrush[] tileSetBrushesFound = (TileSetBrush[]) Resources.FindObjectsOfTypeAll(typeof(TileSetBrush));
    for (int i = 0; i < tileSetBrushesFound.Length; ++i)
    {
      if (tileSetBrushesFound[i].tileSet == tileSet)
        tileSet.AddTileSetBrush(tileSetBrushesFound[i]);
    }

    tileSet.UpdateTileSetBrushTypeArray();
    EditorUtility.SetDirty(tileSet);
  }
}
}
#endif