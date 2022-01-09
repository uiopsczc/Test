#if MicroTileMap
using System.Linq;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileSet : ScriptableObject
{
  public Texture2D atlas_texture; //图片
  public Vector2 tile_pixel_size = new Vector2(32, 32); //每个tile的大小
  public Vector2 slice_offset;
  public Vector2 slice_padding;

  public int column_tile_count
  {
    get
    {
      return _column_tile_count > 0 ? _column_tile_count : Mathf.RoundToInt(atlas_texture.width / tile_pixel_size.x);
    }
  }

  public int row_tile_count
  {
    get { return _row_tile_count > 0 ? _row_tile_count : Mathf.RoundToInt(atlas_texture.height / tile_pixel_size.y); }
  }

  [SerializeField]
  private uint[] brush_group_auto_tile_matrix = Enumerable.Range(0, 31).Select(x => 1u << x).ToArray();
  public bool GetGroupAutoTile(int groupA, int groupB)
  {
    return (brush_group_auto_tile_matrix[groupA] & (1u << groupB)) != 0;
  }


}
}
#endif