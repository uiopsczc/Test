#if MicroTileMap
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMapChunk
{
  [SerializeField, HideInInspector]
  private List<TileObjData> tileObjData_list = new List<TileObjData>();
  private List<GameObject> tile_gameObject_to_be_removed_list = new List<GameObject>();

  public void RefreshTileObjects()
  {
    // 销毁tile为null或者prefab为null的object
    for (int i = 0; i < tileObjData_list.Count; ++i)
    {
      TileObjData tileObjData = tileObjData_list[i];
      uint tileData = tileData_list[tileObjData.index];
      int tileId = TileSetUtil.GetTileIdFromTileData(tileData);
      Tile tile = tileSet.GetTile(tileId);
      if (tile == null || tile.tilePrefabData.prefab == null)
        DestroyTileObject(tileObjData.index);
    }

    //重建
    for (int tile_index = 0; tile_index < tileData_list.Count; ++tile_index)
    {
      uint tileData = tileData_list[tile_index];
      int tileId = TileSetUtil.GetTileIdFromTileData(tileData);
      Tile tile = tileSet.GetTile(tileId);
      if (tile != null && tile.tilePrefabData.prefab != null)
        CreateTileObject(tile_index, tile.tilePrefabData);
    }
  }

  private void DestroyTileObject(int tile_index)
  {
    TileObjData tileObjData = FindTileObjDataByTileIndex(tile_index);
    if (tileObjData != null)
    {
      tile_gameObject_to_be_removed_list.Add(tileObjData.gameObject);
      tileObjData_list.Remove(tileObjData);
    }
  }

  private TileObjData FindTileObjDataByTileIndex(int tile_index)
  {
    for (int i = 0; i < tileObjData_list.Count; ++i)
    {
      TileObjData tileObjData = tileObjData_list[i];
      if (tileObjData.index == tile_index)
        return tileObjData;
    }
    return null;
  }


  private void DestroyTileObject(GameObject obj)
  {
    if (obj != null)
    {
#if UNITY_EDITOR
      if (parent_tileMap.is_undo_enabled)
        Undo.DestroyObjectImmediate(obj);
      else
        DestroyImmediate(obj);
#else
      DestroyImmediate(obj);
#endif
    }
  }


  private GameObject CreateTileObject(int tile_index, TilePrefabData tilePrefabData)
  {
    if (tilePrefabData.prefab != null)
    {
      TileObjData tileObjData = FindTileObjDataByTileIndex(tile_index);
      GameObject tile_gameObject = null;
      int grid_x = tile_index % width;
      int grid_y = tile_index / width;
      if (tileObjData == null || tileObjData.tilePrefabData != tilePrefabData || tileObjData.gameObject == null)
      {
#if UNITY_EDITOR
        tile_gameObject = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefabData.prefab);
        // allow destroy the object with undo operations
        if (parent_tileMap.is_undo_enabled)
          Undo.RegisterCreatedObjectUndo(tile_gameObject, TileMapConst.Undo_Operation_Name + parent_tileMap.name);
#else
          tile_gameObject = (GameObject)Instantiate(tilePrefabData.prefab, Vector3.zero, transform.rotation);
#endif
        SetTileObjTransform(tile_gameObject, grid_x, grid_y, tilePrefabData, tileData_list[tile_index]);
        if (tileObjData != null)
        {
          tile_gameObject_to_be_removed_list.Add(tileObjData.gameObject);
          tileObjData.gameObject = tile_gameObject;
          tileObjData.tilePrefabData = tilePrefabData;
        }
        else
          tileObjData_list.Add(new TileObjData() { index = tile_index, gameObject = tile_gameObject, tilePrefabData = tilePrefabData });
        tile_gameObject.SendMessage(TileMapConst.OnTilePrefabCreation,
            new OnTilePrefabCreationData()
            {
              parent_tileMap = parent_tileMap,
              grid_x = offset_grid_x + grid_x,
              grid_y = offset_grid_y + grid_y
            }, SendMessageOptions.DontRequireReceiver);
        return tile_gameObject;
      }
      else if (tileObjData.gameObject != null)
      {
#if UNITY_EDITOR
        //+++ Break tilemap prefab and restore tile prefab link
        GameObject parentPrefab = PrefabUtility.FindRootGameObjectWithSameParentPrefab(tileObjData.gameObject);
        if (parentPrefab != tileObjData.gameObject)
        {
          DestroyImmediate(tileObjData.gameObject);
          tileObjData.gameObject = PrefabUtility.InstantiatePrefab(tileObjData.tilePrefabData.prefab) as GameObject;
        }
#endif
        SetTileObjTransform(tileObjData.gameObject, grid_x, grid_y, tilePrefabData, tileData_list[tile_index]);
        tileObjData.gameObject.SendMessage(TileMapConst.OnTilePrefabCreation,
            new OnTilePrefabCreationData()
            {
              parent_tileMap = parent_tileMap,
              grid_x = offset_grid_x + grid_x,
              grid_y = offset_grid_y + grid_y
            }, SendMessageOptions.DontRequireReceiver);
        return tileObjData.gameObject;
      }
    }
    return null;
  }


  private void SetTileObjTransform(GameObject tile_gameObject, int grid_x, int grid_y, TilePrefabData tilePrefabData, uint tileData)
  {
    // 比如格子[0,0],中点坐标应该是[0.5 * cell_size.x,0.5 * cell_size.y]
    Vector3 chunk_localPosition = new Vector3((grid_x + .5f) * cell_size.x, (grid_y + .5f) * cell_size.y, tilePrefabData.prefab.transform.position.z);

    if (tilePrefabData.offsetMode == TilePrefabDataOffsetMode.Pixels)
    {
      float ppu = tileSet.tile_pixel_size.x / cell_size.x;
      chunk_localPosition += tilePrefabData.offset / ppu;
    }
    else //if (tilePrefabData.offsetMode == TileOffsetMode.Unit)
      chunk_localPosition += tilePrefabData.offset;
    Vector3 worldPosition = transform.TransformPoint(chunk_localPosition);

    tile_gameObject.transform.position = worldPosition;
    tile_gameObject.transform.rotation = transform.rotation;
    tile_gameObject.transform.parent = transform.parent;
    tile_gameObject.transform.localRotation = tilePrefabData.prefab.transform.localRotation;
    tile_gameObject.transform.localScale = tilePrefabData.prefab.transform.localScale;
    //+++ Apply tile flags
    Vector3 localScale = tile_gameObject.transform.localScale;
    if (TileSetUtil.IsTileFlagRot90(tileData))
      tile_gameObject.transform.localRotation *= Quaternion.Euler(0, 0, -90);
    //For Rot180 and Rot270 avoid changing the scale
    if (TileSetUtil.IsTileFlagFlipH(tileData)&& TileSetUtil.IsTileFlagFlipV(tileData))
      tile_gameObject.transform.localRotation *= Quaternion.Euler(0, 0, -180);
    else
    {
      if (TileSetUtil.IsTileFlagFlipH(tileData))
        localScale.x = -tile_gameObject.transform.localScale.x;
      if (TileSetUtil.IsTileFlagFlipV(tileData))
        localScale.y = -tile_gameObject.transform.localScale.y;
    }
    tile_gameObject.transform.localScale = localScale;
  }

}
}
#endif