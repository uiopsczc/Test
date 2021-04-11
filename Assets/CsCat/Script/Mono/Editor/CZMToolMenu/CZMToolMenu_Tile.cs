#if MicroTileMap
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class CZMToolMenu
{
  [MenuItem(CZMToolConst.MenuRoot + "Tile/创建TileSet")]
  public static TileSet CreateTileSet()
  {
    return ScriptableObjectUtil.CreateAsset<TileSet>();
  }

  [MenuItem(CZMToolConst.MenuRoot + "Tile/创建TileMap")]
  public static void CreateTileMap()
  {
    GameObject gameObject = new GameObject("TileMap");
    gameObject.AddComponent<TileMap>();
  }
}
}
#endif