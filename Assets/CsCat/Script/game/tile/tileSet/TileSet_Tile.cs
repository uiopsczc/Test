#if MicroTileMap
using System;

namespace CsCat
{
public partial class TileSet
{
  public Action<TileSet, int, int> OnTileSelected;
  public Tile selected_tile { get { return selected_tileId != TileSetConst.TileId_Empty ? tile_list[selected_tileId] : null; } }
  private int _selected_tileId = TileSetConst.TileId_Empty;
  public int selected_tileId
  {
    get
    {
      if (_selected_tileId >= tile_list.Count || _selected_tileId < 0)
        _selected_tileId = TileSetConst.TileId_Empty;
      return _selected_tileId;
    }

    set
    {
      int prev_tileId = _selected_tileId;
      _selected_tileId = value;
      _tileSelection = null;
      _selected_tileSetBrushId = TileSetConst.TileSetBrushId_Default;
      OnTileSelected.InvokeIfNotNull(this, prev_tileId, _selected_tileId);
    }
  }

  public Tile GetTile(int tileId)
  {
    if (tileId >= 0 && tileId < tile_list.Count)
      return tile_list[tileId];
    return null;
  }
}
}
#endif