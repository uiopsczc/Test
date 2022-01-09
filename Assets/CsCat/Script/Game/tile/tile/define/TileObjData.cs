#if MicroTileMap
using System;
using UnityEngine;

namespace CsCat
{
[Serializable]
public class TileObjData
{
  public int index;// 在TileMapChunk的tileData_list的index
  public TilePrefabData tilePrefabData;//预设数据
  public GameObject gameObject = null;//gameObject
}
}
#endif