#if MicroTileMap
using System;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
[Serializable]
public struct TileColliderData
{
  public Vector2[] vertices;//collider的顶点
  public TileColliderType type;
  //复制
  public TileColliderData Clone()
  {
    if (this.vertices == null)
      this.vertices = new Vector2[0];
    Vector2[] cloned_vertices = new Vector2[this.vertices.Length];
    vertices.CopyTo(cloned_vertices, 0);
    return new TileColliderData { vertices = cloned_vertices, type = type };
  }

  
  //将顶点转为tileSet.tile_pixel_size的整数百分比
  public void SnapVertices(TileSet tileSet)
  {
    if (vertices != null)
      for (int i = 0; i < vertices.Length; ++i)
        vertices[i] = vertices[i].Snap2(tileSet.tile_pixel_size).ConvertElement(e=>Mathf.Clamp01(e));
  }

  public void ApplyFlippingFlags(uint tileData)
  {
    if (TileSetUtil.IsTileFlagFlipH(tileData))
      FlipH();
    if (TileSetUtil.IsTileFlagFlipV(tileData))
      FlipV();
    if (TileSetUtil.IsTileFlagRot90(tileData))
      Rot90();
  }

  public void RemoveFlippingFlags(uint tileData)
  {
    if (TileSetUtil.IsTileFlagFlipH(tileData))
      FlipH();
    if (TileSetUtil.IsTileFlagFlipV(tileData))
      FlipV();
    if (TileSetUtil.IsTileFlagRot90(tileData))
      Rot90Back();
  }


  public void FlipH()
  {
    for (int i = 0; i < vertices.Length; ++i)
      vertices[i].x = 1f - vertices[i].x;
    Array.Reverse(vertices);
  }

  public void FlipV()
  {
    for (int i = 0; i < vertices.Length; ++i)
      vertices[i].y = 1f - vertices[i].y;
    Array.Reverse(vertices);
  }

  public void Rot90()
  {
    for (int i = 0; i < vertices.Length; ++i)
    {
      //swap x,y
      float tempX = vertices[i].x;
      vertices[i].x = vertices[i].y;
      vertices[i].y = tempX;

      vertices[i].y = 1f - vertices[i].y;//本来是-vertices[i].y就行了，但现在在[0,1]之间的话，所以就变成了1 -vertices[i].y
    }
  }

  public void Rot90Back()
  {
    for (int i = 0; i < vertices.Length; ++i)
    {
      vertices[i].y = 1f - vertices[i].y;//参考Rot90
      // spaw x,y
      float tempX = vertices[i].x;
      vertices[i].x = vertices[i].y;
      vertices[i].y = tempX;
    }
  }

}
}
#endif