#if MicroTileMap
using UnityEngine;

namespace CsCat
{
public static class TileSetBrushUtil
{
  public static int GetGridX(Vector2 position, Vector2 cell_size)
  {
    return Mathf.FloorToInt((position.x + Vector2.kEpsilon) / cell_size.x);
  }

  public static int GetGridY(Vector2 position, Vector2 cellSize)
  {
    return Mathf.FloorToInt((position.y + Vector2.kEpsilon) / cellSize.y);
  }


  public static Vector2 GetSnappedPosition(Vector2 position, Vector2 cell_size)
  {
    Vector2 cell_center = position - cell_size / 2f;
    Vector2 snapped_position = cell_center.Snap(cell_size);
    return snapped_position;
  }
}
}
#endif