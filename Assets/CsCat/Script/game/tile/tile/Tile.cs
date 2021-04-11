#if MicroTileMap
using System;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
[Serializable]
public class Tile
{
  public Rect uv;//uv
  public int auto_tile_group = 0;//auto_tile_group
  public TileColliderData tileColliderData;//碰撞数据
  public ArgContainer argContainer = new ArgContainer();//参数数据
  public TilePrefabData tilePrefabData;//预设数据
}
}
#endif