#if MicroTileMap
using System.Linq;
#if UNITY_EDITOR
using System.Collections.Generic;
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileSet
{
  [SerializeField]
  public float pixels_per_unit = 100f;//多少像素转为世界坐标的1个单位，可以用个scele为1的正方体测试
  [SerializeField]
  public List<Tile> tile_list = new List<Tile>();
  [SerializeField]
  public int _row_tile_count = 0;//行数
  [SerializeField]
  public int _column_tile_count = 0;//列数
  [SerializeField]
  public List<TileView> tileView_list = new List<TileView>();
  [SerializeField]
  public string[] tileSetBrush_group_names = Enumerable.Range(0, 32).Select(x => x == 0 ? "Default" : "").ToArray();
  [SerializeField]
  public List<TileSetBrushContainer> tileSetBrush_list = new List<TileSetBrushContainer>();
  [SerializeField]
  private string[] tileSetBrush_type_mask_options;
  [SerializeField]
  private int _tileSetBrush_type_mask = -1;
}
}
#endif