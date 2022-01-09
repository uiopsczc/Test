#if MicroTileMap
#if UNITY_EDITOR
using System.Collections.Generic;
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMapChunk
{
  [SerializeField, HideInInspector]
  private MeshRenderer meshRenderer;
  [SerializeField, HideInInspector]
  private MeshFilter meshFilter;
  [SerializeField, HideInInspector]
  private List<TileColor32> tileColor_list = null;
  private bool is_need_rebuild_mesh = false;


  private static List<Vector3> vertice_list;
  private List<Vector2> uv_list; //NOTE: this is the only one not static because it's needed to update the animated tiles
  private static List<int> triangle_list;
  private static List<Color32> colors32_list = null;
  public float inner_padding { get { return parent_tileMap.inner_padding; } }

  public int SortingLayerID
  {
    get { return meshRenderer.sortingLayerID; }
    set { meshRenderer.sortingLayerID = value; }
  }

  public string SortingLayerName
  {
    get { return meshRenderer.sortingLayerName; }
    set { meshRenderer.sortingLayerName = value; }
  }

  public int OrderInLayer
  {
    get { return meshRenderer.sortingOrder; }
    set { meshRenderer.sortingOrder = value; }
  }

  public void SetSharedMaterial(Material material)
  {
    meshRenderer.sharedMaterial = material;
    is_need_rebuild_mesh = true;
  }

  public void InvalidateMesh()
  {
    is_need_rebuild_mesh = true;
  }

  public void InvalidateTileSetBrushes()
  {
    is_invalidate_tileSetBrushes = true;
  }

  public void UpdateRendererProperties()
  {
    meshRenderer.shadowCastingMode = parent_tileMap.tileMapChunkRendererProperties.shadowCastingMode;
    meshRenderer.receiveShadows = parent_tileMap.tileMapChunkRendererProperties.is_receive_shadows;
    meshRenderer.lightProbeUsage = parent_tileMap.tileMapChunkRendererProperties.lightProbeUsage;
    meshRenderer.reflectionProbeUsage = parent_tileMap.tileMapChunkRendererProperties.reflectionProbeUsage;
    meshRenderer.probeAnchor = parent_tileMap.tileMapChunkRendererProperties.probeAnchor;
  }


  public bool UpdateMesh()
  {
    if (parent_tileMap == null)
    {
      if (transform.parent == null)
        gameObject.hideFlags = HideFlags.None; //Unhide orphan tilechunks. This shouldn't happen
      parent_tileMap = transform.parent.GetComponent<TileMap>();
    }
    if (gameObject.layer != parent_tileMap.gameObject.layer)
      gameObject.layer = parent_tileMap.gameObject.layer;
    if (gameObject.tag != parent_tileMap.gameObject.tag)
      gameObject.tag = parent_tileMap.gameObject.tag;
    transform.localPosition = new Vector2(offset_grid_x * cell_size.x, offset_grid_y * cell_size.y);

    if (meshFilter.sharedMesh == null)
    {
      //Debug.Log("Creating new mesh for " + name);
      meshFilter.sharedMesh = new Mesh();
      meshFilter.sharedMesh.hideFlags = HideFlags.DontSave;
      meshFilter.sharedMesh.name = parent_tileMap.name + "_mesh";
      is_need_rebuild_mesh = true;
    }
#if UNITY_EDITOR
    // fix prefab preview, not compatible with MaterialPropertyBlock. I need to create a new material and change the main texture and color directly.
    if (UnityEditor.PrefabUtility.GetPrefabType(gameObject) == UnityEditor.PrefabType.Prefab)
    {
      gameObject.hideFlags |= HideFlags.HideInHierarchy;
      if (meshRenderer.sharedMaterial == null || meshRenderer.sharedMaterial == parent_tileMap.material)
      {
        meshRenderer.sharedMaterial = new Material(parent_tileMap.material);
        meshRenderer.sharedMaterial.name += "_copy";
        meshRenderer.sharedMaterial.hideFlags = HideFlags.DontSave;
        meshRenderer.sharedMaterial.color = parent_tileMap.tintColor;
        meshRenderer.sharedMaterial.mainTexture = parent_tileMap.tileSet ? parent_tileMap.tileSet.atlas_texture : null;
      }
    }
    else
#endif
    //NOTE: else above
    {
      meshRenderer.sharedMaterial = parent_tileMap.material;
    }
    meshRenderer.enabled = parent_tileMap.is_visible;
    if (is_need_rebuild_mesh)
    {
      is_need_rebuild_mesh = false;
      if (FillMeshData())
      {
        is_invalidate_tileSetBrushes = false;
        Mesh mesh = meshFilter.sharedMesh;
        mesh.Clear();
#if UNITY_5_0 || UNITY_5_1
                    mesh.vertices = s_vertices.ToArray();
                    mesh.triangles = s_triangles.ToArray();
                    mesh.uv = m_uv.ToArray();
                    if (s_colors32 != null && s_colors32.Count != 0)
                        mesh.colors32 = s_colors32.ToArray();
                    else
                        mesh.colors32 = null;
#else
        mesh.SetVertices(vertice_list);
        mesh.SetTriangles(triangle_list, 0);
        mesh.SetUVs(0, uv_list);
        if (colors32_list != null && colors32_list.Count != 0)
          mesh.SetColors(colors32_list);
        else
          mesh.SetColors((List<Color32>)null);
#endif
        mesh.RecalculateNormals(); //NOTE: allow directional lights to work properly
        TangentSolver(mesh); //NOTE: allow bumped shaders to work with directional lights
      }
      else
      {
        return false;
      }
    }
    return true;
  }



  private bool FillMeshData()
  {
    //Debug.Log( "[" + ParentTilemap.name + "] FillData -> " + name);
    if (!tileSet || !tileSet.atlas_texture)
    {
      return false;
    }
    current_updated_tileMapChunk = this;

    int total_tile_count = width * height;
    if (vertice_list == null)
      vertice_list = new List<Vector3>(total_tile_count * 4);
    else
      vertice_list.Clear();
    if (triangle_list == null)
      triangle_list = new List<int>(total_tile_count * 6);
    else triangle_list.Clear();
    if (colors32_list == null)
      colors32_list = new List<Color32>(total_tile_count * 4);
    else
      colors32_list.Clear();
    if (uv_list == null)
      uv_list = new List<Vector2>(total_tile_count * 4);
    else
      uv_list.Clear();

    Vector2[] sub_tile_offset = new Vector2[]
    {
                new Vector2( 0f, 0f ),
                new Vector2( cell_size.x / 2f, 0f ),
                new Vector2( 0f, cell_size.y / 2f ),
                new Vector2( cell_size.x / 2f, cell_size.y / 2f ),
    };
    Vector2 sub_tile_size = cell_size / 2f;
    animated_tile_list.Clear();
    bool is_empty = true;
    for (int tile_y = 0, tile_index = 0; tile_y < height; ++tile_y)
    {
      for (int tile_x = 0; tile_x < width; ++tile_x, ++tile_index)
      {
        uint tileData = tileData_list[tile_index];
        if (tileData != TileSetConst.TileData_Empty)
        {
          int brushId = (int)((tileData & TileSetConst.TileDataMask_TileSetBrushId) >> 16);
          int tileId = (int)(tileData & TileSetConst.TileDataMask_TileId);
          Tile tile = tileSet.GetTile(tileId);
          TileSetBrush tileBrush = null;
          if (brushId > 0)
          {
            tileBrush = tileSet.FindTileSetBrush(brushId);
            if (tileBrush == null)
            {
              Debug.LogWarning(parent_tileMap.name + "\\" + name + ": BrushId " + brushId + " not found! GridPos(" + tile_x + "," + tile_y + ") tilaData 0x" + tileData.ToString("X"));
              tileData_list[tile_index] = tileData & ~TileSetConst.TileDataMask_TileSetBrushId;//找不到对应的brush则设置该tileData的brushid为空
            }
            if (tileBrush != null && (is_invalidate_tileSetBrushes || (tileData & TileSetConst.TileFlag_Updated) == 0))
            {
              tileData = tileBrush.Refresh(parent_tileMap, offset_grid_x + tile_x, offset_grid_y + tile_y, tileData);
              //+++NOTE: this code add support for animated brushes inside a random brush
              // Collateral effects of supporting changing the brush id in Refresh:
              // - When the random brush select a tile data with another brush id, this tile won't be a random tile any more
              // - If the tilemap is refreshed several times, and at least a tile data contains another brush id, then all tiles will loose the brush id of the random brush
              if (TileSetBrushBehaviour.instance.tileMap == parent_tileMap) // avoid changing brushId when updating the BrushTilemap
              {
                tileData &= ~TileSetConst.TileDataMask_TileSetBrushId;
                tileData |= (uint)(brushId << 16);
              }
              int new_brushId = (int)((tileData & TileSetConst.TileDataMask_TileSetBrushId) >> 16);
              if (brushId != new_brushId)
              {
                brushId = new_brushId;
                tileBrush = tileSet.FindTileSetBrush(brushId);
              }
              //---
              tileData |= TileSetConst.TileFlag_Updated;// set updated flag
              tileData_list[tile_index] = tileData; // update tileData                                
              tileId = (int)(tileData & TileSetConst.TileDataMask_TileId);
              tile = tileSet.GetTile(tileId);
              // update created objects
              if (tile != null && tile.tilePrefabData.prefab != null)
                CreateTileObject(tile_index, tile.tilePrefabData);
              else
                DestroyTileObject(tile_index);
            }
          }

          is_empty = false;

          if (tileBrush != null && tileBrush.IsAnimated())
          {
            animated_tile_list.Add(new AnimTileData() { vertex_index = vertice_list.Count, tileSetBrush = tileBrush, sub_tile_index = -1 });
          }

          current_uv_vertex = vertice_list.Count;
          Rect tile_uv;
          uint[] subtileData = tileBrush != null ? tileBrush.GetSubTiles(parent_tileMap, offset_grid_x + tile_x, offset_grid_y + tile_y, tileData) : null;
          if (subtileData == null)
          {
            if (tile != null)
            {
              if (tile.tilePrefabData.prefab == null || tile.tilePrefabData.is_show_tile_with_prefab //hide the tiles with prefabs ( unless showTileWithPrefab is true )
                  || tileBrush && tileBrush.IsAnimated()) // ( skip if it's an animated brush )
              {
                tile_uv = tile.uv;
                _AddTileToMesh(tile_uv, tile_x, tile_y, tileData, Vector2.zero, cell_size);
                if (tile_color_list != null && tile_color_list.Count > tile_index)
                {
                  TileColor32 tileColor32 = tile_color_list[tile_index];
                  colors32_list.Add(tileColor32.color0);
                  colors32_list.Add(tileColor32.color1);
                  colors32_list.Add(tileColor32.color2);
                  colors32_list.Add(tileColor32.color3);
                }
              }
            }
          }
          else
          {
            for (int i = 0; i < subtileData.Length; ++i)
            {
              uint subTileData = subtileData[i];
              int subTileId = (int)(subTileData & TileSetConst.TileDataMask_TileId);
              Tile subTile = tileSet.GetTile(subTileId);
              tile_uv = subTile != null ? subTile.uv : default(Rect);
              //if (tileUV != default(Rect)) //NOTE: if this is uncommented, there won't be coherence with geometry ( 16 vertices per tiles with subtiles ). But it means also, the tile shouldn't be null.
              {
                _AddTileToMesh(tile_uv, tile_x, tile_y, subTileData, sub_tile_offset[i], sub_tile_size, i);
                if (tile_color_list != null && tile_color_list.Count > tile_index)
                {
                  TileColor32 tileColor32 = tile_color_list[tile_index];
                  Color32 middleColor = new Color32(
                      System.Convert.ToByte((tileColor32.color0.r + tileColor32.color1.r + tileColor32.color2.r + tileColor32.color3.r) >> 2),
                      System.Convert.ToByte((tileColor32.color0.g + tileColor32.color1.g + tileColor32.color2.g + tileColor32.color3.g) >> 2),
                      System.Convert.ToByte((tileColor32.color0.b + tileColor32.color1.b + tileColor32.color2.b + tileColor32.color3.b) >> 2),
                      System.Convert.ToByte((tileColor32.color0.a + tileColor32.color1.a + tileColor32.color2.a + tileColor32.color3.a) >> 2)
                      );
                  switch (i)
                  {
                    case 0:
                     colors32_list.Add(tileColor32.color0);
                     colors32_list.Add(Color32.Lerp(tileColor32.color1, tileColor32.color0, .5f));
                     colors32_list.Add(Color32.Lerp(tileColor32.color2, tileColor32.color0, .5f));
                     colors32_list.Add(middleColor);
                      break;
                    case 1:
                     colors32_list.Add(Color32.Lerp(tileColor32.color0, tileColor32.color1, .5f));
                     colors32_list.Add(tileColor32.color1);
                     colors32_list.Add(middleColor);
                     colors32_list.Add(Color32.Lerp(tileColor32.color3, tileColor32.color1, .5f));
                      break;
                    case 2:
                      colors32_list.Add(Color32.Lerp(tileColor32.color0, tileColor32.color2, .5f));
                      colors32_list.Add(middleColor);
                      colors32_list.Add(tileColor32.color2);
                      colors32_list.Add(Color32.Lerp(tileColor32.color3, tileColor32.color2, .5f));
                      break;
                    case 3:
                      colors32_list.Add(middleColor);
                      colors32_list.Add(Color32.Lerp(tileColor32.color1, tileColor32.color3, .5f));
                      colors32_list.Add(Color32.Lerp(tileColor32.color2, tileColor32.color3, .5f));
                      colors32_list.Add(tileColor32.color3);
                      break;
                  }
                }
              }
            }
          }
        }
      }
    }
    //NOTE: the destruction of tileobjects needs to be done here to avoid a Undo/Redo bug. Check inside DestroyTileObject for more information.
    for (int i = 0; i < tile_gameObject_to_be_removed_list.Count; ++i)
    {
      DestroyTileObject(tile_gameObject_to_be_removed_list[i]);
    }
    tile_gameObject_to_be_removed_list.Clear();
    current_updated_tileMapChunk = null;
    return !is_empty;
  }


  private void _AddTileToMesh(Rect tileUV, int tx, int ty, uint tileData, Vector2 subtileOffset, Vector2 subtileCellSize, int subTileIdx = -1)
  {
    
    float px0 = tx * cell_size.x + subtileOffset.x;
    float py0 = ty * cell_size.y + subtileOffset.y;
    //NOTE: px0 and py0 values are not used to avoid float errors and line artifacts. Don't forget Pixel Snap has to be disabled as well.
    float px1 = tx * cell_size.x + subtileOffset.x + subtileCellSize.x;
    float py1 = ty * cell_size.y + subtileOffset.y + subtileCellSize.y;


    int vertexIdx = vertice_list.Count;
    vertice_list.Add(new Vector3(px0, py0, 0));
    vertice_list.Add(new Vector3(px1, py0, 0));
    vertice_list.Add(new Vector3(px0, py1, 0));
    vertice_list.Add(new Vector3(px1, py1, 0));

    triangle_list.Add(vertexIdx + 3);
    triangle_list.Add(vertexIdx + 0);
    triangle_list.Add(vertexIdx + 2);
    triangle_list.Add(vertexIdx + 0);
    triangle_list.Add(vertexIdx + 3);
    triangle_list.Add(vertexIdx + 1);

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
      tileUV_list[0] = new Vector2(u1, v0);
      tileUV_list[1] = new Vector2(u1, v1);
      tileUV_list[2] = new Vector2(u0, v0);
      tileUV_list[3] = new Vector2(u0, v1);
    }
    else
    {
      tileUV_list[0] = new Vector2(u0, v0);
      tileUV_list[1] = new Vector2(u1, v0);
      tileUV_list[2] = new Vector2(u0, v1);
      tileUV_list[3] = new Vector2(u1, v1);
    }
    
    if (subTileIdx >= 0)
    {
      for (int i = 0; i < 4; ++i)
      {
        if (i == subTileIdx) continue;
        tileUV_list[i] = (tileUV_list[i] + tileUV_list[subTileIdx]) / 2f;
        
      }
    }
    for (int i = 0; i < 4; ++i)
    {
      uv_list.Add(tileUV_list[i]);
    }
  }


  //ref: https://github.com/danielbuechele/SumoVizUnity/blob/master/Assets/Helper/TangentSolver.cs
  // This script has been simplified to be used with tiles were the tangent is always (1, 0, 0, -1)
  private void TangentSolver(Mesh mesh)
  {
    int vertexCount = mesh.vertexCount;
    Vector4[] tangents = new Vector4[vertexCount];
    //ref: https://github.com/danielbuechele/SumoVizUnity/blob/master/Assets/Helper/TangentSolver.cs
    //NOTE: fix issues when using a bumped shader
    for (int i = 0; i < (vertexCount); i++)
    {
      tangents[i].x = 1f;
      //tangents[i].y = 0f;
      //tangents[i].z = 0f;
      tangents[i].w = -1f;
    }
    mesh.tangents = tangents;
  }


  static Vector2[] tileUV_list = new Vector2[4];
  [SerializeField, HideInInspector]
  private List<TileColor32> tile_color_list = null;
  static int current_uv_vertex;
  private List<AnimTileData> animated_tile_list = new List<AnimTileData>();


  struct AnimTileData
  {
    public int vertex_index;
    public int sub_tile_index;
    public ITileSetBrush tileSetBrush;
  }
}
}
#endif