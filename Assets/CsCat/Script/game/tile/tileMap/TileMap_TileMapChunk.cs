#if MicroTileMap
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMap
{
  private TileMapChunk GetOrCreateTileMapChunk(int world_grid_x, int world_grid_y, bool is_create_if_doesnt_exist = false)
  {
    if (tileMapChunk_cache_dict.Count == 0 && transform.childCount > 0)
      BuildTileMapChunkCacheDict();

    Vector2Int chunk_xy = TileMapChunkUtil.GetChunkXY(world_grid_x, world_grid_y);
    TileMapChunk tileMapChunk = null;

    uint key = GetTileMapChunkKey(chunk_xy);
    tileMapChunk_cache_dict.TryGetValue(key, out tileMapChunk);

    if (tileMapChunk == null && is_create_if_doesnt_exist)
    {
      string chunk_name = string.Format("{0}_{1}", chunk_xy.x, chunk_xy.y);
      GameObject chunk_gameObject = new GameObject(chunk_name);
      if (is_undo_enabled)
      {
#if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(chunk_gameObject, TileMapConst.Undo_Operation_Name + name);
#endif
      }
      tileMapChunk = chunk_gameObject.AddComponent<TileMapChunk>();
      chunk_gameObject.transform.parent = transform;
      Vector2Int offset_grid_xy = TileMapChunkUtil.GetOffsetGridXY(chunk_xy);
      tileMapChunk.offset_grid_x = offset_grid_xy.x;
      tileMapChunk.offset_grid_y = offset_grid_xy.y;
      chunk_gameObject.transform.localPosition = new Vector2(tileMapChunk.offset_grid_x * cell_size.x, tileMapChunk.offset_grid_y * cell_size.y);
      chunk_gameObject.transform.localRotation = Quaternion.identity;
      chunk_gameObject.transform.localScale = Vector3.one;
      chunk_gameObject.hideFlags = gameObject.hideFlags | HideFlags.HideInHierarchy;
      if (Application.isPlaying)
        tileMapChunk.Reset();
      tileMapChunk.parent_tileMap = this;
      tileMapChunk.SetDimensions(TileMapConst.TileMapChunk_Size, TileMapConst.TileMapChunk_Size);
      tileMapChunk.SetSharedMaterial(material);
      tileMapChunk.SortingLayerID = sortingLayer;
      tileMapChunk.OrderInLayer = orderInLayer;
      tileMapChunk.UpdateRendererProperties();

      tileMapChunk_cache_dict[key] = tileMapChunk;

    }

    return tileMapChunk;
  }

  private void BuildTileMapChunkCacheDict()
  {
    tileMapChunk_cache_dict.Clear();
    for (int i = 0; i < transform.childCount; ++i)
    {
      TileMapChunk tileMapChunk = transform.GetChild(i).GetComponent<TileMapChunk>();
      if (tileMapChunk)
      {
        Vector2Int chunk_xy = tileMapChunk.GetChunkXY();
        uint key = GetTileMapChunkKey(chunk_xy);
        tileMapChunk_cache_dict[key] = tileMapChunk;
      }
    }
  }


  public uint GetTileMapChunkKey(int chunk_x, int chunk_y)
  {
    return  (uint)((chunk_y << 16) | (chunk_x & 0x0000FFFF));
  }
  public uint GetTileMapChunkKey(Vector2Int chunk_xy)
  {
    return GetTileMapChunkKey(chunk_xy.x,chunk_xy.y);
  }

  public void UpdateTileMapChunkRenderereProperties()
  {
    var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (valueIter.MoveNext())
    {
      var tileMapChunk = valueIter.Current;
      if (tileMapChunk)
        tileMapChunk.UpdateRendererProperties();
    }
  }
}
}
#endif