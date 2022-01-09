#if MicroTileMap
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
public partial class TileMap
{
  public bool is_show_collider_normals = true;
  public bool is_show_grid = true;
  public TileMapColliderDisplayMode tileMapColliderDisplayMode = TileMapColliderDisplayMode.Selected;
#if UNITY_EDITOR
  public void OnDrawGizmosSelected()
  {
    if (tileMapColliderDisplayMode == TileMapColliderDisplayMode.Selected)
    {
      if (Selection.activeGameObject == this.gameObject)
        DoDrawGizmos();
    }
  }

  public void OnDrawGizmos()
  {
    if (tileMapColliderDisplayMode == TileMapColliderDisplayMode.Always ||
        (tileMapColliderDisplayMode == TileMapColliderDisplayMode.ParentSelected && Selection.activeGameObject && this.gameObject.transform.IsChildOf(Selection.activeGameObject.transform)))
      DoDrawGizmos();
  }

  void DoDrawGizmos()
  {
    Vector3 saved_pos = transform.position;
    transform.position += (Vector3)(Vector2.Scale(Camera.current.transform.position, (Vector2.one - parallax_factor))); //apply parallax
    Rect bound_rect = new Rect(tileMapBounds.min, tileMapBounds.size);
    DrawUtil.HandlesDrawSolidRectangleWithOutline( bound_rect, new Color(0, 0, 0, 0), new Color(1, 1, 1, 0.5f), transform);

    if (is_show_grid)
    {
      Plane tileMap_plane = new Plane(this.transform.forward, this.transform.position);
      float distance_camera_to_tileMap = 0f;
      Ray ray_camera_to_plane = new Ray(Camera.current.transform.position, Camera.current.transform.forward);
      tileMap_plane.Raycast(ray_camera_to_plane, out distance_camera_to_tileMap);
      if (HandleUtility.GetHandleSize(ray_camera_to_plane.GetPoint(distance_camera_to_tileMap)) <= 3f)
      {

        // draw tile cells
        Gizmos.color = TileMapConst.TileMap_Grid_Color;

        // Horizontal lines
        for (float i = 1; i < GridWidth; i++)
        {
          Gizmos.DrawLine(
              this.transform.TransformPoint(new Vector3(tileMapBounds.min.x + i * cell_size.x, tileMapBounds.min.y)),
              this.transform.TransformPoint(new Vector3(tileMapBounds.min.x + i * cell_size.x, tileMapBounds.max.y))
              );
        }

        // Vertical lines
        for (float i = 1; i < GridHeight; i++)
        {
          Gizmos.DrawLine(
              this.transform.TransformPoint(new Vector3(tileMapBounds.min.x, tileMapBounds.min.y + i * cell_size.y, 0)),
              this.transform.TransformPoint(new Vector3(tileMapBounds.max.x, tileMapBounds.min.y + i * cell_size.y, 0))
              );
        }
      }
      Gizmos.color = Color.white;
    }

    //Draw Chunk Colliders
    var valueIter = tileMapChunk_cache_dict.Values.GetEnumerator();
    while (valueIter.MoveNext())
    {
      var chunk = valueIter.Current;
      if (chunk)
      {
        string[] asChunkCoords = chunk.name.Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
        int chunkX = int.Parse(asChunkCoords[0]);
        int chunkY = int.Parse(asChunkCoords[1]);
        bound_rect = new Rect(chunkX * TileMapConst.TileMapChunk_Size * cell_size.x, chunkY * TileMapConst.TileMapChunk_Size * cell_size.y, TileMapConst.TileMapChunk_Size * cell_size.x, TileMapConst.TileMapChunk_Size * cell_size.y);
        DrawUtil.HandlesDrawSolidRectangleWithOutline( bound_rect, new Color(0, 0, 0, 0), new Color(1, 0, 0, 0.2f), transform);
        chunk.DrawColliders();
      }
    }
    //
    transform.position = saved_pos; // restore position
  }
#endif
}
}
#endif