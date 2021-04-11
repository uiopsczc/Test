#if MicroTileMap
using UnityEngine;
namespace CsCat
{
public partial class TileMapEditor
{
  bool is_tileSetBrush_visible = false;
  public static TileMapEditorBrushMode tileSetBrush_mode = TileMapEditorBrushMode.Paint;

  private void ResetTileSetBrushMode()
  {
    tileSetBrush_mode = TileMapEditorBrushMode.Paint;
    TileToolbar.instance.tileSetBrushToolbar.TriggerButton(0);
  }

  private TileMapEditorBrushMode GetTileSetBrushMode()
  {
    if (Event.current.shift && TileSetBrushBehaviour.instance.paint_mode == TileSetBrushPaintMode.Pencil)
      return TileMapEditorBrushMode.Erase;
    return tileSetBrush_mode;
  }
}
}
#endif