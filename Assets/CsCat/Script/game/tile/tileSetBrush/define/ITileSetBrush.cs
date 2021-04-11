#if MicroTileMap
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public interface ITileSetBrush 
{
  Vector2[] GetAnimUVWithFlags(float inner_padding = 0f);
}
}
#endif