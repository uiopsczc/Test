#if MicroTileMap

using UnityEngine;

namespace CsCat
{
public static class TileMapUtil
{
  public static Material FindDefaultSpriteMaterial()
  {
#if UNITY_EDITOR
    return UnityEditor.AssetDatabase.GetBuiltinExtraResource<Material>("Sprites-Default.mat"); 
#else
    return Resources.GetBuiltinResource<Material>("Sprites-Default.mat");
#endif
  }

  static public int GetGridX(TileMap tilemap, Vector2 locPosition)
  {
    return TileSetBrushUtil.GetGridX(locPosition, tilemap.cell_size);
  }

  /// <summary>
  /// Gets the grid Y position for a given tilemap and local position. To convert from world to local position use tilemap.transform.InverseTransformPoint(worldPosition).
  /// Avoid using positions multiple of cellSize like 0.32f if cellSize = 0.16f because due float imprecisions the return value could be wrong.
  /// </summary>
  /// <param name="tilemap"></param>
  /// <param name="locPosition"></param>
  /// <returns></returns>
  static public int GetGridY(TileMap tilemap, Vector2 locPosition)
  {
    return TileSetBrushUtil.GetGridY(locPosition, tilemap.cell_size);
  }
}
}
#endif