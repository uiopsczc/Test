#if MicroTileMap
using System;
#if UNITY_EDITOR
#endif

namespace CsCat
{
[Serializable]
public struct TileSetBrushContainer
{
  public int id; // should be > 0
  public TileSetBrush tileSetBrush;
}
}
#endif