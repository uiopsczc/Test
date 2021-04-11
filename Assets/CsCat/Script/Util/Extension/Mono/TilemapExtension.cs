using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CsCat
{
  public  static partial class TilemapExtension
  {

    private static void SetTile(Tilemap tilemap, Vector3Int cell_pos, TileBase tileBase, Hashtable tile_detail_dict)
    {
      tilemap.SetTile(cell_pos, tileBase);
      TileFlags tileFlags = tile_detail_dict.Get<int>("tileFlags").ToEnum<TileFlags>();
      tilemap.SetTileFlags(cell_pos, tileFlags);

      tilemap.SetTransformMatrix(cell_pos,
        tile_detail_dict.Get<string>("transformMatrix").ToMatrix4x4OrDefault(null, Matrix4x4.identity));
    }
  }
}