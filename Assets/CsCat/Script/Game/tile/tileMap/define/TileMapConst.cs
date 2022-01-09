#if MicroTileMap

using UnityEngine;
namespace CsCat
{
public static class TileMapConst
{
  // In the worst case scenario, each tile subdivided in 4 sub tiles, the maximum value should be <= 62.
  // The mesh vertices have a limit of 65535 62(width)*62(height)*4(subtiles)*4(vertices) = 61504
  // Warning: changing this value will break the tilemaps made so far. Change this before creating them.
  public const int TileMapChunk_Size = 60;
  public const string Undo_Operation_Name = "Paint Op. ";
  public const string OnTilePrefabCreation = "OnTilePrefabCreation";
  public static Color TileMap_Grid_Color = new Color(1, 1, 1, 60/255f);
  public static Color TileMap_Collider_Color = new Color(0, 1, 0, 160 / 255f);
}
}
#endif