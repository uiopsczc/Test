#if MicroTileMap
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public class TileSetBrush : ScriptableObject, ITileSetBrush
{
  [SerializeField]
  public AutoTileMode auto_tile_mode = AutoTileMode.Self;
  [SerializeField]
  private int group = 0;
  [SerializeField]
  public bool is_show_in_palette = true;

  
  
  public TileSet tileSet;
  int last_frame_token;

  Vector2[] uv_with_flags = new Vector2[4];

  public virtual Rect GetAnimUV()
  {
    int tileId = TileSetUtil.GetTileIdFromTileData(PreviewTileData());
    return tileSet && tileId != TileSetConst.TileId_Empty ? tileSet.tile_list[tileId].uv : default(Rect);
  }

  public virtual uint PreviewTileData()
  {
    return TileSetConst.TileData_Empty;
  }


  public virtual bool IsAnimated()
  {
    return false;
  }

  public virtual uint GetAnimTileData()
  {
    return PreviewTileData();
  }


  public static uint ApplyAndMergeTileFlags(uint tileData, uint tileData_flags)
  {
    //异或运算
    /*
      0^0 = 0，  如果异或的数据是0的话（后面那个），保持原来的值
      1^0 = 1， 
      0^1 = 1， 如果异或的数据是1的话（后面那个），反转原来的值
      1^1 = 0
     */
    tileData_flags &= TileSetConst.TileDataMask_Flags;
    if ((tileData & TileSetConst.TileFlag_Rot90) != 0)
    {
      if ((tileData_flags & TileSetConst.TileFlag_FlipH) != 0)
        tileData ^= TileSetConst.TileFlag_FlipV;
      if ((tileData_flags & TileSetConst.TileFlag_FlipV) != 0)
        tileData ^= TileSetConst.TileFlag_FlipH;
      if ((tileData_flags & TileSetConst.TileFlag_Rot90) != 0)
      {
        tileData ^= 0xE0000000;
      }
    }
    else
    {
      tileData ^= tileData_flags;
    }
    return tileData;
  }


  public bool AutoTileWith(int selfBrushId, int otherBrushId)
  {
    if (
      ((auto_tile_mode & AutoTileMode.Self) != 0 && selfBrushId == otherBrushId) ||
      ((auto_tile_mode & AutoTileMode.Other) != 0 && otherBrushId != selfBrushId && otherBrushId != (TileSetConst.TileDataMask_TileSetBrushId >> 16))
    )
    {
      return true;
    }
    if ((auto_tile_mode & AutoTileMode.Group) != 0)
    {
      var brush = tileSet.FindTileSetBrush(otherBrushId);
      if (brush)
        return tileSet.GetGroupAutoTile(group, brush.group);
      else if (otherBrushId == TileSetConst.TileSetBrushId_Default)
        return tileSet.GetGroupAutoTile(group, 0); //with normal tiles, use default group (0)
    }
    return false;
  }


  public virtual void OnErase(TileMapChunk chunk, int chunkGx, int chunkGy, uint tileData, int brushId)
  {
    ;
  }

  public virtual uint OnPaint(TileMapChunk chunk, int chunkGx, int chunkGy, uint tileData)
  {
    return tileData;
  }
  public virtual uint Refresh(TileMap tilemap, int gridX, int gridY, uint tileData)
  {
    return tileData;
  }

  public virtual uint[] GetSubTiles(TileMap tilemap, int gridX, int gridY, uint tileData)
  {
    return null;
  }

  public virtual int GetAnimationFrameIndex()
  {
    return 0;
  }

  public virtual Vector2[] GetAnimUVWithFlags(float inner_padding = 0f)
  {
    if (GetAnimationFrameIndex() == last_frame_token)
      return uv_with_flags;
    else
      last_frame_token = GetAnimationFrameIndex();

    uint tileData = GetAnimTileData();
    Rect tileUV = GetAnimUV();

    bool flipH = (tileData & TileSetConst.TileFlag_FlipH) != 0;
    bool flipV = (tileData & TileSetConst.TileFlag_FlipV) != 0;
    bool rot90 = (tileData & TileSetConst.TileFlag_Rot90) != 0;

    //NOTE: xMinMax and yMinMax is opposite if width or height is negative
    float u0 = tileUV.xMin + tileSet.atlas_texture.texelSize.x * inner_padding;
    float v0 = tileUV.yMin + tileSet.atlas_texture.texelSize.y * inner_padding;
    float u1 = tileUV.xMax - tileSet.atlas_texture.texelSize.x * inner_padding;
    float v1 = tileUV.yMax - tileSet.atlas_texture.texelSize.y * inner_padding;

    if (flipV)
    {
      float v = v0;
      v0 = v1;
      v1 = v;
    }
    if (flipH)
    {
      float u = u0;
      u0 = u1;
      u1 = u;
    }

    if (rot90)
    {
      uv_with_flags[0] = new Vector2(u1, v0);
      uv_with_flags[1] = new Vector2(u1, v1);
      uv_with_flags[2] = new Vector2(u0, v0);
      uv_with_flags[3] = new Vector2(u0, v1);
    }
    else
    {
      uv_with_flags[0] = new Vector2(u0, v0);
      uv_with_flags[1] = new Vector2(u1, v0);
      uv_with_flags[2] = new Vector2(u0, v1);
      uv_with_flags[3] = new Vector2(u1, v1);
    }
    return uv_with_flags;
  }
}
}
#endif