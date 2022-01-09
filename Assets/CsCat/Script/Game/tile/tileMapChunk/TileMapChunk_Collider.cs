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
  private MeshCollider meshCollider;
  private bool is_need_rebuild_colliders = false;
  static bool is_on_validate = false;
  [SerializeField]
  private bool has_2D_colliders;

  public void InvalidateMeshCollider()
  {
    is_need_rebuild_colliders = true;
  }


  public bool UpdateColliders()
  {
    if (parent_tileMap == null)
    {
      parent_tileMap = transform.parent.GetComponent<TileMap>();
    }
    if (gameObject.layer != parent_tileMap.gameObject.layer)
      gameObject.layer = parent_tileMap.gameObject.layer;
    if (gameObject.tag != parent_tileMap.gameObject.tag)
      gameObject.tag = parent_tileMap.gameObject.tag;

    //+++ Free unused resources
    if (parent_tileMap.tileMapColliderType != TileMapColliderType._3D)
    {
      if (meshCollider != null)
      {
        if (!is_on_validate)
          DestroyImmediate(meshCollider);
        else
          meshCollider.enabled = false;
      }
    }

    //if (ParentTilemap.ColliderType != eColliderType._2D)
    if (is_need_rebuild_colliders)
    {
      if (has_2D_colliders)
      {
        has_2D_colliders = parent_tileMap.tileMapColliderType == TileMapColliderType._2D;
        System.Type oppositeCollider2DType = parent_tileMap.tileMap2DColliderType != TileMap2DColliderType.EdgeCollider2D ? typeof(EdgeCollider2D) : typeof(PolygonCollider2D);
        System.Type collidersToDestroy = parent_tileMap.tileMapColliderType != TileMapColliderType._2D ? typeof(Collider2D) : oppositeCollider2DType;
        var aCollider2D = GetComponents(collidersToDestroy);
        for (int i = 0; i < aCollider2D.Length; ++i)
        {
          if (!is_on_validate)
            DestroyImmediate(aCollider2D[i]);
          else
            ((Collider2D)aCollider2D[i]).enabled = false;
        }
      }
    }
    //---

    if (parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
    {
      if (meshCollider == null)
      {
        meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null && parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
        {
          meshCollider = gameObject.AddComponent<MeshCollider>();
        }
      }

      if (parent_tileMap.is_trigger)
      {
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
      }
      else
      {
        meshCollider.isTrigger = false;
        meshCollider.convex = false;
      }
      meshCollider.sharedMaterial = parent_tileMap.physicMaterial;

      //NOTE: m_meshCollider.sharedMesh is equal to m_meshFilter.sharedMesh when the script is attached or reset
      if (meshCollider != null && (meshCollider.sharedMesh == null || meshCollider.sharedMesh == meshFilter.sharedMesh))
      {
        meshCollider.sharedMesh = new Mesh();
        meshCollider.sharedMesh.hideFlags = HideFlags.DontSave;
        meshCollider.sharedMesh.name = parent_tileMap.name + "_collmesh";
        is_need_rebuild_colliders = true;
      }
    }

    if (is_need_rebuild_colliders)
    {
      is_need_rebuild_colliders = false;
      bool isEmpty = FillColliderMeshData();
      if (parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
      {
        Mesh mesh =meshCollider.sharedMesh;
        mesh.Clear();
#if UNITY_5_0 || UNITY_5_1
                    mesh.vertices = s_meshCollVertices.ToArray();
                    mesh.triangles = s_meshCollTriangles.ToArray();
#else
        mesh.SetVertices(mesh_collider_vertice_list);
        mesh.SetTriangles(mesh_collider_triangle_list, 0);
#endif
        mesh.RecalculateNormals(); // needed by Gizmos.DrawWireMesh
        meshCollider.sharedMesh = null; // for some reason this fix showing the green lines of the collider mesh
        meshCollider.sharedMesh = mesh;
      }
      return isEmpty;
    }
    return true;
  }

  private static List<Vector3> mesh_collider_vertice_list;
  private static List<int> mesh_collider_triangle_list;




  private bool FillColliderMeshData()
  {
    //Debug.Log( "[" + ParentTilemap.name + "] FillColliderMeshData -> " + name);
    if (tileSet == null || parent_tileMap.tileMapColliderType == TileMapColliderType.None)
    {
      return false;
    }

    System.Type collider2DType = parent_tileMap.tileMap2DColliderType == TileMap2DColliderType.EdgeCollider2D ? typeof(EdgeCollider2D) : typeof(PolygonCollider2D);
    Component[] aColliders2D = null;
    if (parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
    {
      int totalTiles = width * height;
      if (mesh_collider_vertice_list == null)
      {
        mesh_collider_vertice_list = new List<Vector3>(totalTiles * 4);
        mesh_collider_triangle_list = new List<int>(totalTiles * 6);
      }
      else
      {
        mesh_collider_vertice_list.Clear();
        mesh_collider_triangle_list.Clear();
      }
    }
    else //if (ParentTilemap.ColliderType == eColliderType._2D)
    {
      has_2D_colliders = true;
      open_edge_list.Clear();
      aColliders2D = GetComponents(collider2DType);
    }
    float halvedCollDepth = parent_tileMap.collider_depth / 2f;
    bool isEmpty = true;
    for (int ty = 0, tileIdx = 0; ty < height; ++ty)
    {
      for (int tx = 0; tx < width; ++tx, ++tileIdx)
      {
        uint tileData = tileData_list[tileIdx];
        if (tileData != TileSetConst.TileData_Empty)
        {
          int tileId = (int)(tileData & TileSetConst.TileDataMask_TileId);
          Tile tile = tileSet.GetTile(tileId);
          if (tile != null)
          {
#if ENABLE_MERGED_SUBTILE_COLLIDERS
                            TilesetBrush brush = ParentTilemap.Tileset.FindBrush(Tileset.GetBrushIdFromTileData(tileData));
                            Vector2[] subTileMergedColliderVertices = brush ? brush.GetMergedSubtileColliderVertices(ParentTilemap, GridPosX + tx, GridPosY + ty, tileData) : null;
#else
            Vector2[] subTileMergedColliderVertices = null;
#endif
            bool hasMergedColliders = subTileMergedColliderVertices != null;

            TileColliderData tileCollData = tile.tileColliderData;
            if (tileCollData.type != TileColliderType.None || hasMergedColliders)
            {
              isEmpty = false;
              int neighborCollFlags = 0; // don't remove, even using neighborTileCollData, neighborTileCollData is not filled if tile is empty
              bool isSurroundedByFullColliders = true;
              for (int i = 0; i < neighbor_segment_min_max.Length; ++i)
              {
                neighbor_segment_min_max[i].x = float.MaxValue;
                neighbor_segment_min_max[i].y = float.MinValue;
              }
              System.Array.Clear(neighbor_tileColliderData, 0, neighbor_tileColliderData.Length);

              if (!hasMergedColliders)
              {
                if ((tileData & (TileSetConst.TileFlag_FlipH | TileSetConst.TileFlag_FlipV | TileSetConst.TileFlag_Rot90)) != 0)
                {
                  tileCollData = tileCollData.Clone();
                  tileCollData.ApplyFlippingFlags(tileData);
                }
                for (int i = 0; i < 4; ++i)
                {
                  uint neighborTileData;
                  bool isTriggerOrPolygon = parent_tileMap.is_trigger ||
                                            parent_tileMap.tileMapColliderType == TileMapColliderType._2D &&
                                            parent_tileMap.tileMap2DColliderType == TileMap2DColliderType.PolygonCollider2D;
                  switch (i)
                  {
                    case 0:  // Up Tile
                      neighborTileData = (tileIdx + width) < tileData_list.Count ?
                        tileData_list[tileIdx + width]
                      :
                      isTriggerOrPolygon ? TileSetConst.TileData_Empty : parent_tileMap.GetTileData(offset_grid_x + tx, offset_grid_y + ty + 1); break;
                    case 1: // Right Tile
                      neighborTileData = (tileIdx + 1) % width != 0 ? //(tileIdx + 1) < m_tileDataList.Count ? 
                      tileData_list[tileIdx + 1]
                      :
                      isTriggerOrPolygon ? TileSetConst.TileData_Empty : parent_tileMap.GetTileData(offset_grid_x + tx + 1, offset_grid_y + ty); break;
                    case 2: // Down Tile
                      neighborTileData = tileIdx >= width ?
                      tileData_list[tileIdx - width]
                      :
                      isTriggerOrPolygon ? TileSetConst.TileData_Empty : parent_tileMap.GetTileData(offset_grid_x + tx, offset_grid_y + ty - 1); break;
                    case 3: // Left Tile
                      neighborTileData = tileIdx % width != 0 ? //neighborTileId = tileIdx >= 1 ? 
                      tileData_list[tileIdx - 1]
                      :
                      isTriggerOrPolygon ? TileSetConst.TileData_Empty : parent_tileMap.GetTileData(offset_grid_x + tx - 1, offset_grid_y + ty); break;
                    default: neighborTileData = TileSetConst.TileData_Empty; break;
                  }

                  int neighborTileId = (int)(neighborTileData & TileSetConst.TileDataMask_TileId);
                  if (neighborTileId != TileSetConst.TileId_Empty)
                  {
                    Vector2 segmentMinMax;
                    TileColliderData neighborTileCollider;
                    neighborTileCollider = tileSet.tile_list[neighborTileId].tileColliderData;
                    if ((neighborTileData & (TileSetConst.TileFlag_FlipH | TileSetConst.TileFlag_FlipV | TileSetConst.TileFlag_Rot90)) != 0)
                    {
                      neighborTileCollider = neighborTileCollider.Clone();
                      if ((neighborTileData & TileSetConst.TileFlag_FlipH) != 0) neighborTileCollider.FlipH();
                      if ((neighborTileData & TileSetConst.TileFlag_FlipV) != 0) neighborTileCollider.FlipV();
                      if ((neighborTileData & TileSetConst.TileFlag_Rot90) != 0) neighborTileCollider.Rot90();
                    }
                    neighbor_tileColliderData[i] = neighborTileCollider;
                    isSurroundedByFullColliders &= (neighborTileCollider.type == TileColliderType.Full);

                    if (neighborTileCollider.type == TileColliderType.None)
                    {
                      segmentMinMax = new Vector2(float.MaxValue, float.MinValue); //NOTE: x will be min, y will be max
                    }
                    else if (neighborTileCollider.type == TileColliderType.Full)
                    {
                      segmentMinMax = new Vector2(0f, 1f); //NOTE: x will be min, y will be max
                      neighborCollFlags |= (1 << i);
                    }
                    else
                    {
                      segmentMinMax = new Vector2(float.MaxValue, float.MinValue); //NOTE: x will be min, y will be max
                      neighborCollFlags |= (1 << i);
                      for (int j = 0; j < neighborTileCollider.vertices.Length; ++j)
                      {
                        Vector2 v = neighborTileCollider.vertices[j];
                        {
                          if (i == 0 && v.y == 0 || i == 2 && v.y == 1) //Top || Bottom
                          {
                            if (v.x < segmentMinMax.x) segmentMinMax.x = v.x;
                            if (v.x > segmentMinMax.y) segmentMinMax.y = v.x;
                          }
                          else if (i == 1 && v.x == 0 || i == 3 && v.x == 1) //Right || Left
                          {
                            if (v.y < segmentMinMax.x) segmentMinMax.x = v.y;
                            if (v.y > segmentMinMax.y) segmentMinMax.y = v.y;
                          }
                        }
                      }
                    }
                    neighbor_segment_min_max[i] = segmentMinMax;
                  }
                  else
                  {
                    isSurroundedByFullColliders = false;
                  }
                }
              }
              // Create Mesh Colliders
              if (isSurroundedByFullColliders && !hasMergedColliders)
              {
                //Debug.Log(" Surrounded! " + tileIdx);
              }
              else
              {
                float px0 = tx * cell_size.x;
                float py0 = ty * cell_size.y;
                Vector2[] collVertices = subTileMergedColliderVertices;
                if (!hasMergedColliders)
                  collVertices = tileCollData.type == TileColliderType.Full ?TileConst.Full_Collider_Tile_Vertices : tileCollData.vertices;

                for (int i = 0; i < collVertices.Length; ++i)
                {
                  Vector2 s0 = collVertices[i];
                  Vector2 s1 = collVertices[i == (collVertices.Length - 1) ? 0 : i + 1];
                  if (hasMergedColliders) ++i; // add ++i; in this case to go 2 by 2 because the collVertices for merged colliders will have the segments in pairs

                  // full collider optimization
                  if ((tileCollData.type == TileColliderType.Full) &&
                      (
                      (i == 0 && neighbor_tileColliderData[3].type == TileColliderType.Full) || // left tile has collider
                      (i == 1 && neighbor_tileColliderData[0].type == TileColliderType.Full) || // top tile has collider
                      (i == 2 && neighbor_tileColliderData[1].type == TileColliderType.Full) || // right tile has collider
                      (i == 3 && neighbor_tileColliderData[2].type == TileColliderType.Full)  // bottom tile has collider
                      )
                  )
                  {
                    continue;
                  }
                  // polygon collider optimization
                  else // if( tileCollData.type == eTileCollider.Polygon ) Or Full colliders if neighbor is not Full as well
                  {
                    Vector2 n, m;
                    if (s0.y == 1f && s1.y == 1f) // top side
                    {
                      if ((neighborCollFlags & 0x1) != 0) // top tile has collider
                      {
                        n = neighbor_segment_min_max[0];
                        if (n.x < n.y && n.x <= s0.x && n.y >= s1.x)
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.x == 1f && s1.x == 1f) // right side
                    {
                      if ((neighborCollFlags & 0x2) != 0) // right tile has collider
                      {
                        n = neighbor_segment_min_max[1];
                        if (n.x < n.y && n.x <= s1.y && n.y >= s0.y)
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.y == 0f && s1.y == 0f) // bottom side
                    {
                      if ((neighborCollFlags & 0x4) != 0) // bottom tile has collider
                      {
                        n = neighbor_segment_min_max[2];
                        if (n.x < n.y && n.x <= s1.x && n.y >= s0.x)
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.x == 0f && s1.x == 0f) // left side
                    {
                      if ((neighborCollFlags & 0x8) != 0) // left tile has collider
                      {
                        n = neighbor_segment_min_max[3];
                        if (n.x < n.y && n.x <= s0.y && n.y >= s1.y)
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.y == 1f && s1.x == 1f) // top - right diagonal
                    {
                      if ((neighborCollFlags & 0x1) != 0 && (neighborCollFlags & 0x2) != 0)
                      {
                        n = neighbor_segment_min_max[0];
                        m = neighbor_segment_min_max[1];
                        if ((n.x < n.y && n.x <= s0.x && n.y == 1f) && (m.x < m.y && m.x <= s1.y && m.y == 1f))
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.x == 1f && s1.y == 0f) // right - bottom diagonal
                    {
                      if ((neighborCollFlags & 0x2) != 0 && (neighborCollFlags & 0x4) != 0)
                      {
                        n = neighbor_segment_min_max[1];
                        m = neighbor_segment_min_max[2];
                        if ((n.x < n.y && n.x == 0f && n.y >= s0.y) && (m.x < m.y && m.x <= s1.x && m.y == 1f))
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.y == 0f && s1.x == 0f) // bottom - left diagonal
                    {
                      if ((neighborCollFlags & 0x4) != 0 && (neighborCollFlags & 0x8) != 0)
                      {
                        n = neighbor_segment_min_max[2];
                        m = neighbor_segment_min_max[3];
                        if ((n.x < n.y && n.x == 0f && n.y >= s0.x) && (m.x < m.y && m.x == 0f && m.y >= s1.y))
                        {
                          continue;
                        }
                      }
                    }
                    else if (s0.x == 0f && s1.y == 1f) // left - top diagonal
                    {
                      if ((neighborCollFlags & 0x8) != 0 && (neighborCollFlags & 0x1) != 0)
                      {
                        n = neighbor_segment_min_max[3];
                        m = neighbor_segment_min_max[0];
                        if ((n.x < n.y && n.x <= s0.y && n.y == 1f) && (m.x < m.y && m.x == 0f && m.y >= s1.x))
                        {
                          continue;
                        }
                      }
                    }
                  }
                  // Update s0 and s1 to world positions
                  s0.x = px0 + cell_size.x * s0.x; s0.y = py0 + cell_size.y * s0.y;
                  s1.x = px0 + cell_size.x * s1.x; s1.y = py0 + cell_size.y * s1.y;
                  if (parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
                  {
                    int collVertexIdx = mesh_collider_vertice_list.Count;
                    mesh_collider_vertice_list.Add(new Vector3(s0.x, s0.y, -halvedCollDepth));
                    mesh_collider_vertice_list.Add(new Vector3(s0.x, s0.y, halvedCollDepth));
                    mesh_collider_vertice_list.Add(new Vector3(s1.x, s1.y, halvedCollDepth));
                    mesh_collider_vertice_list.Add(new Vector3(s1.x, s1.y, -halvedCollDepth));

                    mesh_collider_triangle_list.Add(collVertexIdx + 0);
                    mesh_collider_triangle_list.Add(collVertexIdx + 1);
                    mesh_collider_triangle_list.Add(collVertexIdx + 2);
                    mesh_collider_triangle_list.Add(collVertexIdx + 2);
                    mesh_collider_triangle_list.Add(collVertexIdx + 3);
                    mesh_collider_triangle_list.Add(collVertexIdx + 0);
                  }
                  else //if( ParentTilemap.ColliderType == eColliderType._2D )
                  {
                    int linkedSegments = 0;
                    int segmentIdxToMerge = -1;
                    for (int edgeIdx = open_edge_list.Count - 1; edgeIdx >= 0 && linkedSegments < 2; --edgeIdx)
                    {
                      LinkedList<Vector2> edgeSegments = open_edge_list[edgeIdx];
                      if (edgeSegments.First.Value == edgeSegments.Last.Value)
                        continue; //skip closed edges
                      if (edgeSegments.Last.Value == s0)
                      {
                        if (segmentIdxToMerge >= 0)
                        {
                          LinkedList<Vector2> segmentToMerge = open_edge_list[segmentIdxToMerge];
                          if (s0 == segmentToMerge.First.Value)
                          {
                            for (LinkedListNode<Vector2> node = segmentToMerge.First.Next; node != null; node = node.Next)
                              edgeSegments.AddLast(node.Value);
                            open_edge_list.RemoveAt(segmentIdxToMerge);
                          }
                          /* Cannot join head with head or tail with tail, it will change the segment normal
                          else
                              for (LinkedListNode<Vector2> node = segmentToMerge.Last.Previous; node != null; node = node.Previous)
                                  edgeSegments.AddLast(node.Value);*/
                        }
                        else
                        {
                          segmentIdxToMerge = edgeIdx;
                          edgeSegments.AddLast(s1);
                        }
                        ++linkedSegments;
                      }
                      /* Cannot join head with head or tail with tail, it will change the segment normal
                      else if( edgeSegments.Last.Value == s1 )                                                
                      else if (edgeSegments.First.Value == s0)*/
                      else if (edgeSegments.First.Value == s1)
                      {
                        if (segmentIdxToMerge >= 0)
                        {
                          LinkedList<Vector2> segmentToMerge = open_edge_list[segmentIdxToMerge];
                          if (s1 == segmentToMerge.Last.Value)
                          {
                            for (LinkedListNode<Vector2> node = edgeSegments.First.Next; node != null; node = node.Next)
                              segmentToMerge.AddLast(node.Value);
                            open_edge_list.RemoveAt(edgeIdx);
                          }
                          /* Cannot join head with head or tail with tail, it will change the segment normal
                          else
                              for (LinkedListNode<Vector2> node = edgeSegments.First.Next; node != null; node = node.Next)
                                  segmentToMerge.AddFirst(node.Value);*/
                        }
                        else
                        {
                          segmentIdxToMerge = edgeIdx;
                          edgeSegments.AddFirst(s0);
                        }
                        ++linkedSegments;
                      }
                    }
                    if (linkedSegments == 0)
                    {
                      LinkedList<Vector2> newEdge = new LinkedList<Vector2>();
                      newEdge.AddFirst(s0);
                      newEdge.AddLast(s1);
                      open_edge_list.Add(newEdge);
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    if (parent_tileMap.tileMapColliderType == TileMapColliderType._2D)
    {
      //+++ Process Edges
      //(NOTE: this was added to fix issues related with lighting, otherwise leave this commented)                
      {
        // Remove vertex inside a line
        RemoveRedundantVertices(open_edge_list);

        // Split segments (NOTE: This is not working with polygon colliders)
        /*/ commented unless necessary for performance reasons
        if (ParentTilemap.Collider2DType == e2DColliderType.EdgeCollider2D)
        {
            openEdges = SplitSegments(openEdges);
        }
        //*/
      }
      //---

      //Create Edges
      for (int i = 0; i < open_edge_list.Count; ++i)
      {
        LinkedList<Vector2> edgeSegments = open_edge_list[i];
        bool reuseCollider = i < aColliders2D.Length;
        Collider2D collider2D = reuseCollider ? (Collider2D)aColliders2D[i] : (Collider2D)gameObject.AddComponent(collider2DType);
        collider2D.enabled = true;
        collider2D.isTrigger = parent_tileMap.is_trigger;
        collider2D.sharedMaterial = parent_tileMap.physicMaterial2D;
        if (parent_tileMap.tileMap2DColliderType == TileMap2DColliderType.EdgeCollider2D)
        {
          ((EdgeCollider2D)collider2D).points = edgeSegments.ToArray();
        }
        else
        {
          ((PolygonCollider2D)collider2D).SetPath(0, edgeSegments.ToArray());
        }
      }

      //Destroy unused edge colliders
      for (int i = open_edge_list.Count; i < aColliders2D.Length; ++i)
      {
        if (!is_on_validate)
          DestroyImmediate(aColliders2D[i]);
        else
          ((Collider2D)aColliders2D[i]).enabled = false;
      }
    }

    return !isEmpty;
  }

  void RemoveRedundantVertices(List<LinkedList<Vector2>> edgeList)
  {
    for (int i = 0; i < edgeList.Count; ++i)
    {
      RemoveRedundantVertices(open_edge_list[i]);
    }
  }

  void RemoveRedundantVertices(LinkedList<Vector2> edgeVertices)
  {
    LinkedListNode<Vector2> iter = edgeVertices.First;
    while (iter != edgeVertices.Last)
    {
      float perpDot;
      //Special case for first node if this is a closed edge
      if (iter == edgeVertices.First)
      {
        if (iter.Value == edgeVertices.Last.Value)
        {
          perpDot = PerpDot(iter.Value, edgeVertices.Last.Previous.Value, iter.Next.Value);
          if (Mathf.Abs(perpDot) <= Vector2.kEpsilon)
          {
            edgeVertices.RemoveFirst();
            edgeVertices.Last.Value = edgeVertices.First.Value;
            iter = edgeVertices.First;
          }
          else
          {
            iter = iter.Next;
          }
        }
        else
        {
          iter = iter.Next;
        }
      }
      else
      {
        perpDot = PerpDot(iter.Value, iter.Previous.Value, iter.Next.Value);
        iter = iter.Next;
        if (Mathf.Abs(perpDot) <= Vector2.kEpsilon)
        {
          edgeVertices.Remove(iter.Previous);
        }
      }
    }
  }


  float PerpDot(Vector2 p, Vector2 a, Vector2 b)
  {
    Vector2 v0 = a - p;
    Vector2 v1 = b - p;
    return PerpDot(v0, v1);
  }


  float PerpDot(Vector2 v0, Vector2 v1)
  {
    return v0.x * v1.y - v0.y * v1.x;
  }



  private static List<LinkedList<Vector2>> open_edge_list = new List<LinkedList<Vector2>>(50);
  private static Vector2[] neighbor_segment_min_max = new Vector2[4];
  private static TileColliderData[] neighbor_tileColliderData = new TileColliderData[4];




}
}
#endif