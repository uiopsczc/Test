#if MicroTileMap
using UnityEngine;

namespace CsCat
{
public static class TileMapChunkUtil
{
  //在chunk数组中的[][]坐标中x，chunck数组与grid格子数组类似，(只不过是grid_x/TileMapConst.TileMapChunk_Size,所以里面的元素个数相对于grid格子数组里面的个数少)
  public static Vector2Int GetChunkXY(int world_grid_x, int world_grid_y)
  {
    int chunk_x = (world_grid_x < 0 ? (world_grid_x + 1 - TileMapConst.TileMapChunk_Size) : world_grid_x) / TileMapConst.TileMapChunk_Size;
    int chunk_y = (world_grid_y < 0 ? (world_grid_y + 1 - TileMapConst.TileMapChunk_Size) : world_grid_y) / TileMapConst.TileMapChunk_Size;
    return new Vector2Int(chunk_x,chunk_y);
  }

  public static Vector2Int GetChunkXY(Vector2Int world_grid_xy)
  {
    return GetChunkXY(world_grid_xy.x, world_grid_xy.y);
  }

  public static Vector2Int GetOffsetGridXY(int chunk_x, int chunk_y)
  { 
    return new Vector2Int(chunk_x * TileMapConst.TileMapChunk_Size, chunk_y * TileMapConst.TileMapChunk_Size);
  }

  public static Vector2Int GetOffsetGridXY(Vector2Int chunk_xy)
  {
    return GetOffsetGridXY(chunk_xy.x, chunk_xy.y);
  }

  //相对于所属于的chunk的localGridXY
  public static Vector2Int GetLocalGridXYOfChunk(int world_grid_x, int world_grid_y)
  {
    world_grid_x = (world_grid_x < 0 ? -world_grid_x - 1 : world_grid_x);
    world_grid_y = (world_grid_y < 0 ? -world_grid_y - 1 : world_grid_y);
    return new Vector2Int(world_grid_x % TileMapConst.TileMapChunk_Size, world_grid_y % TileMapConst.TileMapChunk_Size);
  }
  //相对于所属于的chunk的localGridXY
  public static Vector2Int GetLocalGridXYOfChunk(Vector2Int world_grid_xy)
  {
    return GetLocalGridXYOfChunk(world_grid_xy.x, world_grid_xy.y);
  }

  
}
}
#endif