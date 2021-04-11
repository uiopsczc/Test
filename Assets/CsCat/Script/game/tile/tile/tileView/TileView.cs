
#if MicroTileMap
using System;
using UnityEngine;

namespace CsCat
{
//tile的一个组合
[Serializable]
public class TileView
{
  [SerializeField]
  public string name;//名称
  [SerializeField]
  public TileSelection tileSelection;//该tileView中选中的tile

  public TileView(string name, TileSelection tileSelection)
  {
    this.name = name;
    this.tileSelection = tileSelection;
  }
}
}
#endif