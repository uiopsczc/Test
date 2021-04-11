#if MicroTileMap
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
[CustomEditor(typeof(TileMap))]
public partial class TileMapEditor : Editor
{
  private TileMap tileMap;
  private TileSet tileMap_tileSet;
  static TileMapEditorEditMode edit_mode = TileMapEditorEditMode.Paint;
  private static TileSetControl tileSetControl;
  MouseDoubleClick mouseDoubleClick = new MouseDoubleClick();
  List<Vector2> fill_preview_tile_pos_list = new List<Vector2>(500);
  public static TileMapEditorBrushMode tileMapEditorBrushMode = TileMapEditorBrushMode.Paint;
  int mouse_grid_x;
  int mouse_grid_y;
  public static bool is_display_help_box = false;
  

  [SerializeField]
  private bool is_toggle_map_bounds_edit = false;

  void OnEnable()
  {
    tileMap = (TileMap)target;
    tileMap_tileSet = tileMap.tileSet;
    RegisterTilesetEvents(tileMap_tileSet);
    TileToolbar.instance.tileSetBrushToolbar.TriggerButton(0);
    TileToolbar.instance.tileSetBrushPaintToolbar.TriggerButton(0);
    //fix missing material on prefabs tilemaps (when pressing play for example)
    if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(tileMap.gameObject)))
      tileMap.Refresh(true, false, false, false);
    if (tileMap.parent_tileMapGroup)
    {
      tileMap.parent_tileMapGroup.selected_tileMap = tileMap;
    }
  }

  void OnDisable()
  {
    UnregisterTilesetEvents(tileMap_tileSet);
    TileSetBrushBehaviour.SetVisible(false);
  }
}
}
#endif