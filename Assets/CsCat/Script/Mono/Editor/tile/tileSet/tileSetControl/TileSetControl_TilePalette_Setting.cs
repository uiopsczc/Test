#if MicroTileMap
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TileSetControl
{
  private Color prev_background_color;
  

  public TileView DrawTilePaletteSetting()
  {
    //选中的是那个tileView
    List<string> tileView_name_list = new List<string>() { "(All)" };
    tileView_name_list.AddRange(tileSet.tileView_list.Select(x => x.name));
    string[] tileView_names = tileView_name_list.ToArray();
    int[] tileView_values = Enumerable.Range(-1, tileSet.tileView_list.Count + 1).ToArray();
    using (var check = new EditorGUIBeginChangeCheckScope())
    {
      sharedTileSetData.tileView_reorderableList.index = EditorGUILayout.IntPopup("TileSet View", sharedTileSetData.tileView_reorderableList.index, tileView_names, tileView_values);
      if (check.IsChanged)
        RemoveTileSelection();
    }


    //column_tile_length
    tileView = sharedTileSetData.tileView_reorderableList != null && sharedTileSetData.tileView_reorderableList.index >= 0 ? tileSet.tileView_list[sharedTileSetData.tileView_reorderableList.index] : null;
    if (tileView == null)
      tileSet.column_tile_count_in_palette = Mathf.Clamp(EditorGUILayout.IntField("column_tile_count_in_palette", tileSet.column_tile_count_in_palette), 1, tileSet.column_tile_count);

    // Draw Background Color Selector
    //    tileSet.background_color = EditorGUILayout.ColorField("Background Color", tileSet.background_color);
    //    if (prev_background_color != tileSet.background_color || GUIStyleConst.Scroll_Style.normal.background == null)
    //    {
    //      prev_background_color = tileSet.background_color;
    //      if (GUIStyleConst.Scroll_Style.normal.background == null)
    //        GUIStyleConst.Scroll_Style.normal.background = new Texture2D(1, 1) { hideFlags = HideFlags.DontSave };
    //      GUIStyleConst.Scroll_Style.normal.background.SetPixel(0, 0, tileSet.background_color);
    //      GUIStyleConst.Scroll_Style.normal.background.Apply();
    //    }


    //tile palette中tile的缩放比例
    using (new EditorGUILayoutBeginHorizontalScope())
    {
      GUILayout.Label(EditorGUIUtility.FindTexture("ViewToolZoom"), GUILayout.Width(35f));
      float visual_tile_zoom = EditorGUILayout.Slider(tileSet.visual_tile_size.x / tileSet.tile_pixel_size.x, 0.25f, 4f);
      tileSet.visual_tile_size = visual_tile_zoom * tileSet.tile_pixel_size;
      if (GUILayout.Button("重置", GUILayout.Width(50f)))
        tileSet.visual_tile_size = new Vector2(32f * tileSet.tile_pixel_size.x / tileSet.tile_pixel_size.y, 32f);
    }

    return tileView;
  }

  private void RemoveTileSelection()
  {
    sharedTileSetData.pointed_tile_index_rect = sharedTileSetData.start_drag_tile_index_rect = sharedTileSetData.end_drag_tile_index_rect;
    tileSet.tileSelection = null;
  }
}
}
#endif