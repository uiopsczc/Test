#if MicroTileMap
using System;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
[AddComponentMenu("Tile/TileMap", 10)]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public partial class TileMap : MonoBehaviour
{
  static List<TileMapChunk> tileMapChunk_list = new List<TileMapChunk>(50);
  Dictionary<uint, TileMapChunk> tileMapChunk_cache_dict = new Dictionary<uint, TileMapChunk>();
  public bool is_undo_enabled = false;

  public Action<TileMap, int, int,uint> OnTileChanged;

  private bool is_mark_for_update_mesh = false;
#if UNITY_EDITOR
  private bool is_mark_scene_dirty_on_next_update_mesh = false;
#endif

  public int GridWidth { get { return max_grid_x - min_grid_x + 1; } }
  public int GridHeight { get { return max_grid_y - min_grid_y + 1; } }

  public static bool Is_Disable_Tile_Prefab_Creation = false;

  public TileMapColliderType tileMapColliderType = TileMapColliderType.None;
  public TileMap2DColliderType tileMap2DColliderType = TileMap2DColliderType.EdgeCollider2D;
  public float collider_depth = 0.1f;
  public float inner_padding = 0f;
  public TileSet tileSet
  {
    get { return _tileSet; }
    set
    {
      bool has_changed = _tileSet != value;
      _tileSet = value;
      if (has_changed)
      {
        if (_tileSet != null && cell_size == default(Vector2))
          cell_size = _tileSet.tile_pixel_size / _tileSet.pixels_per_unit;
      }
    }
  }
  public bool is_visible
  {
    get
    {
      return _is_visible;
    }
    set
    {
      bool pre_value = _is_visible;
      _is_visible = value;
      if (_is_visible != pre_value)
        UpdateMesh();
    }
  }

  public void RefreshTileMapChunksSortingAttributes()
  {
    var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (valueIter.MoveNext())
    {
      var tileMapChunk = valueIter.Current;
      if (tileMapChunk)
      {
        tileMapChunk.SortingLayerID = sortingLayer;
        tileMapChunk.OrderInLayer = orderInLayer;
      }
    }
  }


  public void Refresh(bool is_refresh_mesh = true, bool is_refresh_mesh_collider = true, bool is_refresh_tile_objects = false, bool is_invalidate_tileSetBrushes = false)
  {
    BuildTileMapChunkCacheDict();
    var value_iter = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (value_iter.MoveNext())
    {
      var tileMapChunk = value_iter.Current;
      if (tileMapChunk)
      {
        if (is_refresh_mesh)
          tileMapChunk.InvalidateMesh();
        if (is_refresh_mesh_collider)
          tileMapChunk.InvalidateMeshCollider();
        if (is_refresh_tile_objects)
          tileMapChunk.RefreshTileObjects();
        if (is_invalidate_tileSetBrushes)
          tileMapChunk.InvalidateTileSetBrushes();
      }
    }
    UpdateMesh();
  }

  public void UpdateMesh()
  {
    is_mark_for_update_mesh = true;
  }

  public uint GetTileData(Vector2 localPosition)
  {
    int grid_x = TileSetBrushUtil.GetGridX(localPosition, cell_size);
    int grid_y = TileSetBrushUtil.GetGridY(localPosition, cell_size);
    return GetTileData(grid_x, grid_y);
  }

  public uint GetTileData(int world_grid_x, int world_grid_y)
  {
    TileMapChunk tileMapChunk = GetOrCreateTileMapChunk(world_grid_x, world_grid_y);
    if (tileMapChunk == null)
      return TileSetConst.TileData_Empty;
    else
    {
      Vector2Int local_grid_xy = TileMapChunkUtil.GetLocalGridXYOfChunk(world_grid_x, world_grid_y);
      int local_grid_x = local_grid_xy.x;
      int local_grid_y = local_grid_xy.y;
      if (world_grid_x < 0)
        local_grid_x = TileMapConst.TileMapChunk_Size - 1 - local_grid_x;
      if (world_grid_y < 0)
        local_grid_y = TileMapConst.TileMapChunk_Size - 1 - local_grid_y;
      return tileMapChunk.GetTileData(local_grid_x, local_grid_y);
    }
  }


  public void ClearMap()
  {
    tileMapBounds = new Bounds();
    max_grid_x = max_grid_y = min_grid_x = min_grid_y = 0;
    while (transform.childCount > 0)
    {
#if UNITY_EDITOR
      if (is_undo_enabled)
        Undo.DestroyObjectImmediate(transform.GetChild(0).gameObject);
      else
        DestroyImmediate(transform.GetChild(0).gameObject);
#else
        DestroyImmediate(transform.GetChild(0).gameObject);
#endif
    }
  }


  public void RecalculateMapBounds()
  {
    // Fix grid limits if necessary
    min_grid_x = Mathf.Min(min_grid_x, 0);
    min_grid_y = Mathf.Min(min_grid_y, 0);
    max_grid_x = Mathf.Max(max_grid_x, 0);
    max_grid_y = Mathf.Max(max_grid_y, 0);

    Vector2 min_tile_pos = Vector2.Scale(new Vector2(min_grid_x, min_grid_y), cell_size);
    Vector2 max_tile_pos = Vector2.Scale(new Vector2(max_grid_x, max_grid_y), cell_size);
    Vector3 saved_size = tileMapBounds.size;
    tileMapBounds.min = tileMapBounds.max = Vector2.zero;
    tileMapBounds.Encapsulate(min_tile_pos);
    tileMapBounds.Encapsulate(min_tile_pos + cell_size);
    tileMapBounds.Encapsulate(max_tile_pos);
    tileMapBounds.Encapsulate(max_tile_pos + cell_size);
    if (saved_size != tileMapBounds.size)
    {
      var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
      while (valueIter.MoveNext())
      {
        var tileMapChunk = valueIter.Current;
        if (tileMapChunk)
          tileMapChunk.InvalidateTileSetBrushes();
      }
    }
  }



  public void ShrinkMapBoundsToVisibleArea()
  {
    Bounds tileMap_bounds = new Bounds();
    Vector2 half_cell_size = cell_size / 2f; // used to avoid precission errors
    max_grid_x = max_grid_y = min_grid_x = min_grid_y = 0;
    var value_iterator = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (value_iterator.MoveNext())
    {
      var tileMapChunk = value_iterator.Current;
      if (tileMapChunk)
      {
        Bounds tileMapChunk_bounds = tileMapChunk.GetBounds();
        Vector2 min = transform.InverseTransformPoint(tileMapChunk.transform.TransformPoint(tileMapChunk_bounds.min));
        Vector2 max = transform.InverseTransformPoint(tileMapChunk.transform.TransformPoint(tileMapChunk_bounds.max));
        tileMap_bounds.Encapsulate(min + half_cell_size);
        tileMap_bounds.Encapsulate(max - half_cell_size);
      }
    }
    min_grid_x = TileSetBrushUtil.GetGridX(tileMap_bounds.min, cell_size);
    min_grid_y = TileSetBrushUtil.GetGridY(tileMap_bounds.min, cell_size);
    max_grid_x = TileSetBrushUtil.GetGridX(tileMap_bounds.max, cell_size);
    max_grid_y = TileSetBrushUtil.GetGridY(tileMap_bounds.max, cell_size);
    RecalculateMapBounds();
  }


  public void FlipV(bool changeFlags)
  {
    List<uint> flipped_list = new List<uint>(GridWidth * GridHeight);
    for (int grid_y = min_grid_y; grid_y <= max_grid_y; ++grid_y)
    {
      for (int grid_x = min_grid_x; grid_x <= max_grid_x; ++grid_x)
      {
        int flippedGy = GridHeight - 1 - grid_y;
        flipped_list.Add(GetTileData(grid_x, flippedGy));
      }
    }

    int idx = 0;
    for (int grid_y = min_grid_y; grid_y <= max_grid_y; ++grid_y)
    {
      for (int grid_x = min_grid_x; grid_x <= max_grid_x; ++grid_x, ++idx)
      {
        uint flippedTileData = flipped_list[idx];
        if (
          changeFlags
          && (flippedTileData != TileSetConst.TileData_Empty)
          && (flippedTileData & TileSetConst.TileDataMask_TileSetBrushId) == 0 // don't activate flip flags on brushes
        )
        {
          flippedTileData = TileSetBrush.ApplyAndMergeTileFlags(flippedTileData, TileSetConst.TileFlag_FlipV);
        }
        SetTileData(grid_x, grid_y, flippedTileData);
      }
    }
  }

  public void FlipH(bool changeFlags)
  {
    List<uint> flippedList = new List<uint>(GridWidth * GridHeight);
    for (int gx = min_grid_x; gx <= max_grid_x; ++gx)
    {
      for (int gy = min_grid_y; gy <= max_grid_y; ++gy)
      {
        int flippedGx = GridWidth - 1 - gx;
        flippedList.Add(GetTileData(flippedGx, gy));
      }
    }

    int idx = 0;
    for (int gx = min_grid_x; gx <= max_grid_x; ++gx)
    {
      for (int gy = min_grid_y; gy <= max_grid_y; ++gy, ++idx)
      {
        uint flippedTileData = flippedList[idx];
        if (
          changeFlags
          && (flippedTileData != TileSetConst.TileData_Empty)
          && (flippedTileData & TileSetConst.TileDataMask_TileSetBrushId) == 0 // don't activate flip flags on brushes
        )
        {
          flippedTileData = TileSetBrush.ApplyAndMergeTileFlags(flippedTileData, TileSetConst.TileFlag_FlipH);
        }
        SetTileData(gx, gy, flippedTileData);
      }
    }
  }


  public void Rot90(bool changeFlags)
  {
    List<uint> flippedList = new List<uint>(GridWidth * GridHeight);
    for (int gy = min_grid_y; gy <= max_grid_y; ++gy)
    {
      for (int gx = min_grid_x; gx <= max_grid_x; ++gx)
      {
        flippedList.Add(GetTileData(gx, gy));
      }
    }

    int minGridX = min_grid_x;
    int minGridY = min_grid_y;
    int maxGridX = max_grid_x;
    int maxGridY = max_grid_x;
    ClearMap();

    int idx = 0;
    for (int gx = minGridX; gx <= maxGridX; ++gx)
    {
      for (int gy = maxGridY; gy >= minGridY; --gy, ++idx)
      {
        uint flippedTileData = flippedList[idx];
        if (
          changeFlags
          && (flippedTileData != TileSetConst.TileData_Empty)
          && (flippedTileData & TileSetConst.TileDataMask_TileSetBrushId) == 0 // don't activate flip flags on brushes
        )
        {
          flippedTileData = TileSetBrush.ApplyAndMergeTileFlags(flippedTileData, TileSetConst.TileFlag_Rot90);
        }
        SetTileData(gx, gy, flippedTileData);
      }
    }
  }

  public void SetTileData(Vector2 grid, uint tileData)
  {
    SetTileData((int)grid.x, (int)grid.y, tileData);
  }

  public void SetTileData(int grid_x, int grid_y, uint tileData)
  {
    var tileMapChunk = GetOrCreateTileMapChunk(grid_x, grid_y, true);
    int tileMapChunk_grid_x = (grid_x < 0 ? -grid_x - 1 : grid_x) % TileMapConst.TileMapChunk_Size;
    int tileMapChunk_grid_y = (grid_y < 0 ? -grid_y - 1 : grid_y) % TileMapConst.TileMapChunk_Size;
    if (grid_x < 0)
      tileMapChunk_grid_x = TileMapConst.TileMapChunk_Size - 1 - tileMapChunk_grid_x;
    if (grid_y < 0)
      tileMapChunk_grid_y = TileMapConst.TileMapChunk_Size - 1 - tileMapChunk_grid_y;
    if (is_allow_painting_out_of_bounds || (grid_x >= min_grid_x && grid_x <= max_grid_x && grid_y >= min_grid_y && grid_y <= max_grid_y))
    {
      tileMapChunk.SetTileData(tileMapChunk_grid_x, tileMapChunk_grid_y, tileData);
      if (OnTileChanged != null)
        OnTileChanged(this, grid_x, grid_y, tileData);
#if UNITY_EDITOR
      is_mark_scene_dirty_on_next_update_mesh = true;
#endif
      {
       min_grid_x = Mathf.Min(min_grid_x, grid_x);
       max_grid_x = Mathf.Max(max_grid_x, grid_x);
       min_grid_y = Mathf.Min(min_grid_y, grid_y);
       max_grid_y = Mathf.Max(max_grid_y, grid_y);
      }
    }
  }


  public bool InvalidateChunkAt(int grid_x, int grid_y, bool is_invalidate_mesh = true, bool is_invalidate_mesh_collider = true)
  {
    TileMapChunk tileMapChunk = GetOrCreateTileMapChunk(grid_x, grid_y);
    if (tileMapChunk != null)
    {
      tileMapChunk.InvalidateMesh();
      tileMapChunk.InvalidateMeshCollider();
      return true;
    }
    return false;
  }

  public Action<TileMap> OnMeshUpdated;
  public void UpdateMeshImmediate()
  {
#if UNITY_EDITOR
    if (!Application.isPlaying && is_mark_scene_dirty_on_next_update_mesh)
    {
      is_mark_scene_dirty_on_next_update_mesh = false;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
      UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
#else
                EditorApplication.MarkSceneDirty();
#endif
    }
#endif

    RecalculateMapBounds();

    tileMapChunk_list.Clear();
    var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (valueIter.MoveNext())
    {
      var chunk = valueIter.Current;
      if (chunk)
      {
        if (!chunk.UpdateMesh())
        {
#if UNITY_EDITOR
          if (is_undo_enabled)
          {
            Undo.DestroyObjectImmediate(chunk.gameObject);
          }
          else
          {
            DestroyImmediate(chunk.gameObject);
          }
#else
                        DestroyImmediate(chunk.gameObject);
#endif
        }
        else
        {
          //chunk.UpdateColliderMesh();
          tileMapChunk_list.Add(chunk);
        }
      }
    }

    if (is_auto_shrink)
      ShrinkMapBoundsToVisibleArea();

    // UpdateColliderMesh is called after calling UpdateMesh of all tilechunks, because UpdateColliderMesh needs the tileId to be updated 
    // ( remember a brush sets neighbours tile id to empty, so UpdateColliderMesh won't be able to know the collider type )
    for (int i = 0; i < tileMapChunk_list.Count; ++i)
    {
      tileMapChunk_list[i].UpdateColliders();
    }

    if (OnMeshUpdated != null)
      OnMeshUpdated(this);
  }


  void Update()
  {
    if (Application.isPlaying && is_apply_contacts_empty_fix)
    {
      is_apply_contacts_empty_fix = false;
      var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
      while (valueIter.MoveNext())
      {
        var chunk = valueIter.Current;
        if (chunk)
        {
          chunk.ApplyContactsEmptyFix();
        }
      }
    }

    if (is_mark_for_update_mesh)
    {
      is_mark_for_update_mesh = false;
      is_apply_contacts_empty_fix = tileMapColliderType == TileMapColliderType._3D;
      UpdateMeshImmediate();
    }
  }
  private bool is_apply_contacts_empty_fix = false;
}
}
#endif