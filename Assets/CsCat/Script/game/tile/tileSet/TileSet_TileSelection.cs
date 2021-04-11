#if MicroTileMap
using System;

namespace CsCat
{
public partial class TileSet
{
  private TileSelection _tileSelection = null;
  public Action<TileSet> OnTileSelectionChanged;
  public TileSelection tileSelection
  {
    get
    {
      _tileSelection = (_tileSelection != null && !_tileSelection.tileData_list.IsNullOrEmpty()) ? _tileSelection : null;
      return _tileSelection;
    }
    set
    {
      TileSelection prev_value = _tileSelection;
      _tileSelection = (value != null && !value.tileData_list.IsNullOrEmpty()) ? value : null;
      if (_tileSelection != null)
      {
        _selected_tileId = TileSetConst.TileId_Empty;
        _selected_tileSetBrushId = TileSetConst.TileSetBrushId_Default;
      }
      if (prev_value != _tileSelection)
        OnTileSelectionChanged.InvokeIfNotNull(this);
    }
  }
}
}
#endif