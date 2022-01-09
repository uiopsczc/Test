
#if MicroTileMap
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public  partial class TileMapChunk :MonoBehaviour
{
  [SerializeField, HideInInspector]
  private int width = -1;
  [SerializeField, HideInInspector]
  private int height = -1;
  [SerializeField, HideInInspector]
  private List<uint> tileData_list = new List<uint>();
  

  public TileMap parent_tileMap;
  public int offset_grid_x;//当前的chunk相对于（0，0）x方向偏移了多少个格子
  public int offset_grid_y;//当前的chunk相对于（0，0）y方向偏移了多少个格子
  private bool is_invalidate_tileSetBrushes = false;
  static TileMapChunk current_updated_tileMapChunk;

  public Vector2 cell_size { get { return parent_tileMap.cell_size; } }

  public TileSet tileSet { get { return parent_tileMap.tileSet; } }

  public void Reset()
  {
    SetDimensions(width, height);

#if UNITY_EDITOR
    if (meshRenderer != null)
      EditorUtility.SetSelectedRenderState(meshRenderer, EditorSelectedRenderState.Hidden);
#endif
    is_need_rebuild_mesh = true;
    is_need_rebuild_colliders = true;
  }

  public void SetDimensions(int width, int height)
  {
    int size = width * height;
    if (size > 0 && size * 4 < 65000) //NOTE: 65000 is the current maximum vertex allowed per mesh and each tile has 4 vertex
    {
      this.width = width;
      this.height = height;
      tileData_list = Enumerable.Repeat(TileSetConst.TileData_Empty, size).ToList();
    }
    else
      LogCat.warn("Invalid parameters!");
  }

  public uint GetTileData(Vector2 localPosition)
  {
    return GetTileData((int)(localPosition.x / cell_size.x), (int)(localPosition.y / cell_size.y));
  }

  public uint GetTileData(int local_grid_x, int local_grid_y)
  {
    if (local_grid_x >= 0 && local_grid_x < width && local_grid_y >= 0 && local_grid_y < height)
    {
      int tile_index = local_grid_y * width + local_grid_x;
      return tileData_list[tile_index];
    }
    else
      return TileSetConst.TileData_Empty;
  }

  //在chunk数组中的[][]坐标中x，chunck数组与grid格子数组类似，(只不过是grid_x/TileMapConst.TileMapChunk_Size,所以里面的元素个数相对于grid格子数组里面的个数少)
  public Vector2Int GetChunkXY()
  {
    return TileMapChunkUtil.GetChunkXY(offset_grid_x, offset_grid_y);
  }


  public Bounds GetBounds()
  {
    Bounds bounds = meshFilter.sharedMesh ? meshFilter.sharedMesh.bounds : default(Bounds);
    if (bounds == default(Bounds))
    {
      Vector3 vMinMax = Vector2.Scale(new Vector2(offset_grid_x < 0 ? width : 0f, offset_grid_y < 0 ? height : 0f), cell_size);
      bounds.SetMinMax(vMinMax, vMinMax);
    }
    for (int i = 0; i < tileObjData_list.Count; ++i)
    {
      int local_grid_x = tileObjData_list[i].index % width;
      if (offset_grid_x >= 0)
        local_grid_x++;
      int local_grid_y = tileObjData_list[i].index / width;
      if (offset_grid_x >= 0)
        local_grid_y++;
      Vector2 grid_pos = Vector2.Scale(new Vector2(local_grid_x, local_grid_y), cell_size);
      bounds.Encapsulate(grid_pos);
    }
    return bounds;
  }

  public void SetTileData(Vector2 local_position, uint tileData)
  {
    SetTileData((int)(local_position.x / cell_size.x), (int)(local_position.y / cell_size.y), tileData);
  }

  public void SetTileData(int local_grid_x, int local_grid_y, uint tileData)
  {
    if (local_grid_x >= 0 && local_grid_x < width && local_grid_y >= 0 && local_grid_y < height)
    {
      int tile_index = local_grid_y * width + local_grid_x;

      int tileId = (int)(tileData & TileSetConst.TileDataMask_TileId);
      Tile tile = tileSet.GetTile(tileId);

      int prev_tileId = (int)(tileData_list[tile_index] & TileSetConst.TileDataMask_TileId);
      Tile prev_tile = tileSet.GetTile(prev_tileId);

      int brushId = TileSetUtil.GetTileSetBrushIdFromTileData(tileData);
      int prev_brushId = TileSetUtil.GetTileSetBrushIdFromTileData(tileData_list[tile_index]);

      if (brushId != prev_brushId)
      {
        if (!current_updated_tileMapChunk) // avoid this is chunks is being Updated from FillMeshData
        {
          // Refresh Neighbors ( and itself if needed )
          for (int y_offset = -1; y_offset <= 1; ++y_offset)
          {
            for (int x_offset = -1; x_offset <= 1; ++x_offset)
            {
              if ((x_offset | y_offset) == 0)
              {
                if (brushId > 0)
                {
                  // Refresh itself
                  tileData = (tileData & ~TileSetConst.TileFlag_Updated);
                }
              }
              else
              {
                int grid_x = (local_grid_x + x_offset);
                int grid_y = (local_grid_y + y_offset);
                int index = grid_y * width + grid_x;
                bool is_inside_chunk = (grid_x >= 0 && grid_x < width && grid_y >= 0 && grid_y < height);
                uint neighbor_tileData = is_inside_chunk ? tileData_list[index] : parent_tileMap.GetTileData(offset_grid_x + local_grid_x + x_offset, offset_grid_y + local_grid_y + y_offset);
                int neighbor_brushId = (int)((neighbor_tileData & TileSetConst.TileDataMask_TileSetBrushId) >> 16);
                TileSetBrush neighbor_tileSetBrush = parent_tileMap.tileSet.FindTileSetBrush(neighbor_brushId);
                //if (brush != null && brush.AutotileWith(brushId, neighborBrushId) || prevBrush != null && prevBrush.AutotileWith(prevBrushId, neighborBrushId))
                if (neighbor_tileSetBrush != null &&
                    (neighbor_tileSetBrush.AutoTileWith(neighbor_brushId, brushId) || neighbor_tileSetBrush.AutoTileWith(neighbor_brushId, prev_brushId)))
                {
                  neighbor_tileData = (neighbor_tileData & ~TileSetConst.TileFlag_Updated); // force a refresh
                  if (is_inside_chunk)
                  {
                    tileData_list[index] = neighbor_tileData;
                  }
                  else
                  {
                    parent_tileMap.SetTileData(offset_grid_x + grid_x, offset_grid_y + grid_y, neighbor_tileData);
                  }
                }
              }
            }
          }
        }
      }
      else if (brushId > 0)
      {
        // Refresh itself
        tileData = (tileData & ~TileSetConst.TileFlag_Updated);
      }

      is_need_rebuild_mesh |= (tileData_list[tile_index] != tileData) || (tileData & TileSetConst.TileDataMask_TileId) == TileSetConst.TileId_Empty;
      is_need_rebuild_colliders |= is_need_rebuild_mesh &&
      (
          (prev_brushId > 0) || (brushId > 0) // there is a brush (a brush could change the collider data later)
          || (tile != null && tile.tileColliderData.type != TileColliderType.None) || (prev_tile != null && prev_tile.tileColliderData.type != TileColliderType.None) // prev. or new tile has colliders
      );

      if (parent_tileMap.tileMapColliderType != TileMapColliderType.None && is_need_rebuild_colliders)
      {
        // Refresh Neighbors tilechunk colliders, to make the collider autotiling
        // Only if neighbor is outside this tilechunk
        for (int y_offset = -1; y_offset <= 1; ++y_offset)
        {
          for (int x_offset = -1; x_offset <= 1; ++x_offset)
          {
            if ((x_offset | y_offset) != 0) // skip this tile position xf = yf = 0
            {
              int grid_x = (local_grid_x + x_offset);
              int grid_y = (local_grid_y + y_offset);
              bool is_inside_chunk = (grid_x >= 0 && grid_x < width && grid_y >= 0 && grid_y < height);
              if (!is_inside_chunk)
              {
                parent_tileMap.InvalidateChunkAt(offset_grid_x + grid_x, offset_grid_y + grid_y, false, true);
              }
            }
          }
        }
      }

      // Update tile data
      tileData_list[tile_index] = tileData;

      if (!TileMap.Is_Disable_Tile_Prefab_Creation)
      {
        // Create tile Objects
        if (tile != null && tile.tilePrefabData.prefab != null)
          CreateTileObject(tile_index, tile.tilePrefabData);
        else
          DestroyTileObject(tile_index);
      }

      TileSetBrush brush = parent_tileMap.tileSet.FindTileSetBrush(brushId);
      if (brushId != prev_brushId)
      {
        TileSetBrush prevBrush = parent_tileMap.tileSet.FindTileSetBrush(prev_brushId);
        if (prevBrush != null)
        {
          prevBrush.OnErase(this, local_grid_x, local_grid_y, tileData, prev_brushId);
        }
      }
      if (brush != null)
      {
        tileData = brush.OnPaint(this, local_grid_x, local_grid_y, tileData);
      }
    }
  }

  private MaterialPropertyBlock materialPropertyBlock;
  void UpdateMaterialPropertyBlock()
  {
    if (materialPropertyBlock == null)
      materialPropertyBlock = new MaterialPropertyBlock();
    meshRenderer.GetPropertyBlock(materialPropertyBlock);
#if UNITY_EDITOR
    // Apply UnselectedColorMultiplier
    TileMapGroup selected_tileMapGroup;
    if (
        !Application.isPlaying &&
        // Check if there is a parent tilemap group and this tilemap is not the selected tilemap in the tilemap group
        parent_tileMap.parent_tileMapGroup &&
        parent_tileMap.parent_tileMapGroup.selected_tileMap != parent_tileMap &&
        // Check if the tilemap group or any of its children is selected
        Selection.activeGameObject &&
        (Selection.activeGameObject == parent_tileMap.parent_tileMapGroup.gameObject ||
        ((selected_tileMapGroup = Selection.activeGameObject.GetComponentInParent<TileMapGroup>()) && selected_tileMapGroup == parent_tileMap.parent_tileMapGroup) &&
        // Exception: the selected object is parent of the tilemap but it's not a tilemap group (ex: grouping tilemaps under a dummy gameobject)
        !(parent_tileMap.transform.IsChildOf(Selection.activeGameObject.transform) && !Selection.activeGameObject.GetComponent<TileMapGroup>())
        )
    )
    {
      materialPropertyBlock.SetColor("_Color", parent_tileMap.tintColor * parent_tileMap.parent_tileMapGroup.unselected_color_multiplier);
    }
    else
#endif
    {
      materialPropertyBlock.SetColor("_Color", parent_tileMap.tintColor);
    }
    if (tileSet && tileSet.atlas_texture != null)
      materialPropertyBlock.SetTexture("_MainTex", tileSet.atlas_texture);
    //else //TODO: find a way to set a null texture or pink texture
    //  m_matPropBlock.SetTexture("_MainTex", default(Texture));
    meshRenderer.SetPropertyBlock(materialPropertyBlock);
  }

  static Dictionary<Material, Material> material_copy_with_pixel_snap_dict = new Dictionary<Material, Material>();
  void OnWillRenderObject()
  {
    if (!parent_tileMap.tileSet)
      return;

    if (parent_tileMap.is_pixel_snap && parent_tileMap.material.HasProperty("PixelSnap"))
    {
      Material material_copy_with_pixel_snap;
      if (!material_copy_with_pixel_snap_dict.TryGetValue(parent_tileMap.material, out material_copy_with_pixel_snap))
      {
        material_copy_with_pixel_snap = new Material(parent_tileMap.material);
        material_copy_with_pixel_snap.name += "_pixelSnapCopy";
        material_copy_with_pixel_snap.hideFlags = HideFlags.DontSave;
        material_copy_with_pixel_snap.EnableKeyword("PIXELSNAP_ON");
        material_copy_with_pixel_snap.SetFloat("PixelSnap", 1f);
        material_copy_with_pixel_snap_dict[parent_tileMap.material] = material_copy_with_pixel_snap;
      }
      meshRenderer.sharedMaterial = material_copy_with_pixel_snap;
    }
    else
    {
      meshRenderer.sharedMaterial = parent_tileMap.material;
    }

    UpdateMaterialPropertyBlock();

    if (animated_tile_list.Count > 0) //TODO: add fps attribute to update animated tiles when necessary
    {
      for (int i = 0; i < animated_tile_list.Count; ++i)
      {
        AnimTileData animTileData = animated_tile_list[i];
        Vector2[] uvs = animTileData.tileSetBrush.GetAnimUVWithFlags(inner_padding);
        if (animTileData.sub_tile_index >= 0)
        {
          for (int j = 0; j < 4; ++j)
          {
            if (j == animTileData.sub_tile_index)
              uv_list[animTileData.vertex_index + j] = uvs[j];
            else
              uv_list[animTileData.vertex_index + j] = (uvs[j] + uvs[animTileData.sub_tile_index]) / 2f;
          }
        }
        else
        {
          uv_list[animTileData.vertex_index + 0] = uvs[0];
          uv_list[animTileData.vertex_index + 1] = uvs[1];
          uv_list[animTileData.vertex_index + 2] = uvs[2];
          uv_list[animTileData.vertex_index + 3] = uvs[3];
        }
      }
      if (meshFilter.sharedMesh)
        meshFilter.sharedMesh.SetUVs(0, uv_list);
    }
  }

  void OnEnable()
  {
    if (parent_tileMap == null)
    {
      parent_tileMap = GetComponentInParent<TileMap>();
    }

#if UNITY_EDITOR
    if (meshRenderer != null)
    {
      EditorUtility.SetSelectedRenderState(meshRenderer, EditorSelectedRenderState.Hidden);
    }
#endif
   meshRenderer = GetComponent<MeshRenderer>();
   meshFilter = GetComponent<MeshFilter>();
   meshCollider = GetComponent<MeshCollider>();

    if (tileData_list == null || tileData_list.Count != width * height)
    {
      SetDimensions(width, height);
    }

    // if not playing, this will be done later by OnValidate
    if (Application.isPlaying && IsInitialized()) //NOTE: && IsInitialized was added to avoid calling UpdateMesh when adding this component and GridPos was set
    {
      // Refresh only if Mesh is null (this happens if hideFlags == DontSave)
      is_need_rebuild_mesh = meshFilter.sharedMesh == null;
      is_need_rebuild_colliders = parent_tileMap.tileMapColliderType == TileMapColliderType._3D && (meshCollider == null || meshCollider.sharedMesh == null);
      UpdateMesh();
      UpdateColliders();
    }
  }


  public bool IsInitialized()
  {
    return width > 0 && height > 0;
  }


  public void ApplyContactsEmptyFix()
  {
    if (meshCollider)
      meshCollider.convex = meshCollider.convex;
  }
}
}
#endif