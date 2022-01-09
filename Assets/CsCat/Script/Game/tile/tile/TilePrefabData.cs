#if MicroTileMap
using System;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
//预设数据
[Serializable]
public struct TilePrefabData
{
  public GameObject prefab;//预设
  public Vector3 offset;//偏移
  public TilePrefabDataOffsetMode offsetMode;//偏移模式：像素还是世界坐标的单位
  public bool is_show_tile_with_prefab;//是否显示prefab
  public bool is_show_prefab_preview_in_tile_palette;//是否在tile_palette面板中显示预设预览图

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (obj.GetType() != this.GetType())
      return false;

    TilePrefabData other = (TilePrefabData)obj;
    return (other.prefab == this.prefab) && (other.offset == this.offset) && (other.offsetMode == this.offsetMode);
  }

  public override int GetHashCode() { return base.GetHashCode(); }
  public static bool operator ==(TilePrefabData c1, TilePrefabData c2) { return c1.Equals(c2); }
  public static bool operator !=(TilePrefabData c1, TilePrefabData c2) { return !c1.Equals(c2); }
}
}
#endif