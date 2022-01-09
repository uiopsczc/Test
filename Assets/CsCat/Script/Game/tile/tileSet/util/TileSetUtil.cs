#if MicroTileMap

namespace CsCat
{
public static class TileSetUtil
{
  //获取TileSetBrushId
  public static int GetTileSetBrushIdFromTileData(uint tileData)
  {
    return tileData !=TileSetConst.TileData_Empty ? (int)((tileData & TileSetConst.TileDataMask_TileSetBrushId) >> 16) : -1;
  }
  //获取TileId
  public static int GetTileIdFromTileData(uint tileData)
  {
    return (int)(tileData & TileSetConst.TileDataMask_TileId);
  }
  //获取TileId
  public static uint GetTileFlagsFromTileData(uint tileData)
  {
    return (tileData & TileSetConst.TileDataMask_Flags);
  }

  public static bool IsTileFlagFlipH(uint tileData)
  {
    return (tileData & TileSetConst.TileFlag_FlipH) != 0;
  }

  public static bool IsTileFlagFlipV(uint tileData)
  {
    return (tileData & TileSetConst.TileFlag_FlipV) != 0;
  }

  public static bool IsTileFlagRot90(uint tileData)
  {
    return (tileData & TileSetConst.TileFlag_Rot90) != 0;
  }
  

}
}
#endif