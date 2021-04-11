#if MicroTileMap
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMap
{
  public Material material
  {
    get
    {
      if (_material == null)
        _material = TileMapUtil.FindDefaultSpriteMaterial();
      return _material;
    }
    set
    {
      if (value != null && _material != value)
      {
        _material = value;
        Refresh();
      }
    }
  }

  public Color tintColor
  {
    get { return _tintColor; }
    set { _tintColor = value; }
  }


 
}

}
#endif