#if MicroTileMap
using UnityEngine;

namespace CsCat
{
public partial class TileMapEditor
{
  public void OnSceneGUI()
  {
    mouseDoubleClick.Update();
    if(tileMap == null || tileMap.tileSet == null)
      return;
    Vector3 saved_pos = tileMap.transform.position;
    tileMap.transform.position += (Vector3)(Vector2.Scale(Camera.current.transform.position, (Vector2.one - tileMap.parallax_factor))); //apply parallax
    is_tileSetBrush_visible = edit_mode == TileMapEditorEditMode.Paint;
    if (edit_mode == TileMapEditorEditMode.Paint)
    {
      OnSceneGUI_DoPaint();
      if (GetTileSetBrushMode() == TileMapEditorBrushMode.Fill && Event.current.type == EventType.Repaint)
      {
        Color fill_preview_color = new Color(1f, 1f, 1f, 0.2f);
        Color empty_color = new Color(0f, 0f, 0f, 0f);
        Rect tile_rect = new Rect(Vector2.zero, tileMap.cell_size);
        foreach (Vector2 tile_pos in fill_preview_tile_pos_list)
        {
          tile_rect.position = tile_pos;
          DrawUtil.HandlesDrawSolidRectangleWithOutline( tile_rect, fill_preview_color, empty_color, tileMap.transform);
        }
      }
//      else if (_edit_mode == TileMapEditorEditMode.Map)
//        DoMapSceneGUI();
//      else if (_edit_mode == TileMapEditorEditMode.Collider)
//        DoColliderSceneGUI();
      TileSetBrushBehaviour.SetVisible(is_tileSetBrush_visible);
      tileMap.transform.position = saved_pos; // restore position
    }
  }


}
}
#endif