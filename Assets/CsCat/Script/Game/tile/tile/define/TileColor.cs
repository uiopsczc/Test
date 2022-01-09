#if MicroTileMap
using System;
using UnityEngine;

namespace CsCat
{
//tile四个顶点uv的颜色
[Serializable]
public struct TileColor32
{
  public Color32 color0;
  public Color32 color1;
  public Color32 color2;
  public Color32 color3;
  public TileColor32(Color32 color)
  {
    color0 = color1 = color2 = color3 = color;
  }
  public TileColor32(Color32 color0, Color32 color1, Color32 color2, Color32 color3)
  {
    this.color0 = color0;
    this.color1 = color1;
    this.color2 = color2;
    this.color3 = color3;
  }
}
}
#endif