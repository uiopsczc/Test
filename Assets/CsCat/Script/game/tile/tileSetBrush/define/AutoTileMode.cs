#if MicroTileMap
#if UNITY_EDITOR
#endif

namespace CsCat
{
public enum AutoTileMode
{
  Self = 1,
  Other = 1 << 1,
  Group = 1 << 2,
  EmptyCells = 1 << 3,
  TileMapBounds = 1 << 4,
}
}
#endif