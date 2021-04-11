#if MicroTileMap
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace CsCat
{
public class SharedTileSetData
{
  public TileSet tileSet;
  public ReorderableList tileView_reorderableList;
  public ReorderableList tileSetBrush_reorderableList;
  public Vector2 tile_palette_scroll_pos;//在tile_palette中的滚动到的位置
  public Vector2 tileSetBrushes_scroll_pos;
  public int tileView_column_count_in_tile_palette = 1;
  public KeyValuePair<int, Rect> start_drag_tile_index_rect;//按下拖动时鼠标所在的tile矩形
  public KeyValuePair<int, Rect> end_drag_tile_index_rect;//拖动移动中鼠标所在的tile矩形
  public KeyValuePair<int, Rect> pointed_tile_index_rect;//放开鼠标，拖动结束时候鼠标所在的tile矩形
}
}
#endif