#if MicroTileMap
namespace CsCat
{
public class TileSetConst
{
  public const int TileId_Empty = 0x0000FFFF;//取后16位（一个f占4位）
  public const int TileSetBrushId_Default = 0;
  public const uint TileData_Empty = 0xFFFFFFFF;

  //Tile Data Masks
  public const int TileDataMask_TileId = TileId_Empty;//
  public const uint TileDataMask_TileSetBrushId = 0x0FFF0000; // (brush_id 0是未定义的brush)
  public const uint TileDataMask_Flags = 0xF0000000; // Flags: (1bit)FlipX, (1bit)FlipY, (1bits)Rot90, (1 bit)tile是否需要update

  // Tile Data Flags
  public const uint TileFlag_FlipV = 0x80000000;//上下翻转
  public const uint TileFlag_FlipH = 0x40000000;//水平翻转
  public const uint TileFlag_Rot90 = 0x20000000;//旋转90度
  public const uint TileFlag_Updated = 0x10000000; // tile是否需要update

}
}
#endif