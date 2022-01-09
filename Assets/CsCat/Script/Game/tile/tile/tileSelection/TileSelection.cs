#if MicroTileMap
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
// 选中的多个tile
[Serializable]
public class TileSelection
{
  [SerializeField]
  public List<uint> tileData_list;//选中的tileData
  [SerializeField]
  public int column_count = 1;//列数


  public List<uint> selection_tileData_list { get { return tileData_list; } }
  
  public TileSelection() : this(null, 0) { }
  public TileSelection(List<uint> selection_tileData_list, int column_count)
  {
    this.tileData_list = selection_tileData_list != null ? selection_tileData_list : new List<uint>();
    this.column_count = Mathf.Max(1, column_count);
  }

  public TileSelection Clone()
  {
    List<uint> selection_tileData_list = new List<uint>(this.tileData_list);
    int column_count = this.column_count;
    return new TileSelection(selection_tileData_list, column_count);
  }
  //里面的格子二维数组[][]顺序上下交换
  public void FlipVertical()
  {
    List<uint> fliped_tileData_list = new List<uint>();
    int row_count = 1 + (tileData_list.Count - 1) / column_count;
    for (int row = row_count - 1; row >= 0; --row)
    {
      for (int column = 0; column < column_count; ++column)
      {
        int index = row * column_count + column;
        fliped_tileData_list.Add(tileData_list[index]);
      }
    }
    tileData_list = fliped_tileData_list;
  }
}
}
#endif