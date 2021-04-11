#if MicroTileMap

#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
public  partial class TileMapChunk
{
  public void DrawColliders()
  {
    if (parent_tileMap.tileMapColliderType == TileMapColliderType._3D)
    {
      if (meshCollider != null && meshCollider.sharedMesh != null && meshCollider.sharedMesh.normals.Length > 0f)
      {
        Gizmos.color = TileMapConst.TileMap_Collider_Color;
        Gizmos.DrawWireMesh(meshCollider.sharedMesh, transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = Color.white;
      }
    }
    else if (parent_tileMap.tileMapColliderType == TileMapColliderType._2D)
    {
      Gizmos.color = TileMapConst.TileMap_Collider_Color;
      Gizmos.matrix = gameObject.transform.localToWorldMatrix;
      Collider2D[] edgeColliders = GetComponents<Collider2D>();
      for (int i = 0; i < edgeColliders.Length; ++i)
      {
        Collider2D collider2D = edgeColliders[i];
        if (collider2D.enabled)
        {
          Vector2[] points = collider2D is EdgeCollider2D ? ((EdgeCollider2D)collider2D).points : ((PolygonCollider2D)collider2D).points;
          for (int j = 0; j < (points.Length - 1); ++j)
          {
            Gizmos.DrawLine(points[j], points[j + 1]);
            //Draw normals
            if (parent_tileMap.is_show_collider_normals)
            {
              Vector2 s0 = points[j];
              Vector2 s1 = points[j + 1];
              Vector3 normPos = (s0 + s1) / 2f;
              Gizmos.DrawLine(normPos, normPos + Vector3.Cross(s1 - s0, -Vector3.forward).normalized * parent_tileMap.cell_size.y * 0.05f);
            }
          }
        }
      }
      Gizmos.matrix = Matrix4x4.identity;
      Gizmos.color = Color.white;
    }
  }
}
}
#endif