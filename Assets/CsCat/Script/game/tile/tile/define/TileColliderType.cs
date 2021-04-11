#if MicroTileMap
#if UNITY_EDITOR
#endif

namespace CsCat
{
public enum TileColliderType
{
  None = 0,//没有
  Full,//四个角落顶点
  Polygon//自定义形状顶点
}
}
#endif