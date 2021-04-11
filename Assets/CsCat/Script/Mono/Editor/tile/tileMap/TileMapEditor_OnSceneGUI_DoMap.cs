//#if MicroTileMap
//using UnityEditor;
//using UnityEngine;
//
//namespace CsCat
//{
//public partial class TileMapEditor
//{
//  
//
//  
//  public void OnSceneGUI_DoMap()
//  {
//    if (m_toggleMapBoundsEdit)
//    {
//      EditorGUI.BeginChangeCheck();
//      Handles.color = Color.green;
//      Vector3 vMinX = Handles.FreeMoveHandle(m_tilemap.transform.TransformPoint(new Vector2(m_tilemap.MinGridX * m_tilemap.CellSize.x, m_tilemap.MapBounds.center.y)), Quaternion.identity, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Vector3.zero, EditorCompatibilityUtils.CubeCap);
//      Vector3 vMaxX = Handles.FreeMoveHandle(m_tilemap.transform.TransformPoint(new Vector2((m_tilemap.MaxGridX + 1f) * m_tilemap.CellSize.x, m_tilemap.MapBounds.center.y)), Quaternion.identity, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Vector3.zero, EditorCompatibilityUtils.CubeCap);
//      Vector3 vMinY = Handles.FreeMoveHandle(m_tilemap.transform.TransformPoint(new Vector2(m_tilemap.MapBounds.center.x, m_tilemap.MinGridY * m_tilemap.CellSize.y)), Quaternion.identity, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Vector3.zero, EditorCompatibilityUtils.CubeCap);
//      Vector3 vMaxY = Handles.FreeMoveHandle(m_tilemap.transform.TransformPoint(new Vector2(m_tilemap.MapBounds.center.x, (m_tilemap.MaxGridY + 1f) * m_tilemap.CellSize.y)), Quaternion.identity, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Vector3.zero, EditorCompatibilityUtils.CubeCap);
//      Handles.color = Color.white;
//      int minGridX = Mathf.RoundToInt(m_tilemap.transform.InverseTransformPoint(vMinX).x / m_tilemap.CellSize.x);
//      int maxGridX = Mathf.RoundToInt(m_tilemap.transform.InverseTransformPoint(vMaxX).x / m_tilemap.CellSize.x - 1f);
//      int minGridY = Mathf.RoundToInt(m_tilemap.transform.InverseTransformPoint(vMinY).y / m_tilemap.CellSize.y);
//      int maxGridY = Mathf.RoundToInt(m_tilemap.transform.InverseTransformPoint(vMaxY).y / m_tilemap.CellSize.y - 1f);
//      if (EditorGUI.EndChangeCheck())
//      {
//        EditorUtility.SetDirty(target);
//        m_tilemap.SetMapBounds(minGridX, minGridY, maxGridX, maxGridY);
//      }
//    }
//  }
//
//
//}
//}
//#endif