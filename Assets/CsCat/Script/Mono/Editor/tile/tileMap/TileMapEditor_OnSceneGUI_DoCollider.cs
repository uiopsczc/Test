//#if MicroTileMap
//using System.Collections.Generic;
//using System.Linq;
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
//  public void OnSceneGUI_DoCollider()
//  {
//    Event e = Event.current;
//    Vector2 vLocMousePos;
//    //if (!m_editColliders) return;            
//    if (EditorWindow.mouseOverWindow == SceneView.currentDrawingSceneView)
//    {
//      if (EditorGUIUtility.hotControl == 0 && // don't select another tile while dragging a handle
//          GetMousePosOverTilemap(m_tilemap, out vLocMousePos))
//      {
//        //NOTE: Threshold should be a value between [0, 1). This is a percent of the size of the tile area to be taken into account to consider the mouse over the tile. 
//        /// Ex: for a value of 0.5, the mouse should be inside the tile at a distance of half of the size
//        float threshold = 0.7f;
//        Vector2 vTileCenter = TileSetBrushUtil.GetSnappedPosition(vLocMousePos, m_tilemap.CellSize) + m_tilemap.CellSize / 2f;
//        float distX = Mathf.Abs(vLocMousePos.x - vTileCenter.x);
//        float distY = Mathf.Abs(vLocMousePos.y - vTileCenter.y);
//        if (distX <= m_tilemap.CellSize.x * threshold / 2f && distY <= m_tilemap.CellSize.y * threshold / 2f)
//          m_colMouseLocPos = vLocMousePos;
//      }
//      // Draw selected tile handles     
//      uint tileData = m_tilemap.GetTileData(m_colMouseLocPos);
//      Tile tile = m_tilemap.GetTile(m_colMouseLocPos);
//      Vector2 tileLocPos = BrushUtil.GetSnappedPosition(m_colMouseLocPos, m_tilemap.CellSize);
//      Vector2 tileCenter = tileLocPos + m_tilemap.CellSize / 2f;
//      if (tile != null)
//      {
//        // Prevent from selecting another gameobject when clicking the scene
//        int controlID = GUIUtility.GetControlID(FocusType.Passive);
//        HandleUtility.AddDefaultControl(controlID);
//
//        HandlesEx.DrawRectWithOutline(m_tilemap.transform, new Rect(BrushUtil.GetSnappedPosition(m_colMouseLocPos, m_tilemap.CellSize), m_tilemap.CellSize), new Color(0f, 0f, 0f, 0.1f), new Color(0f, 0f, 0f, 0f));
//        bool updateColliders = false;
//        TileColliderData tileColliderData = tile.collData.Clone();
//        tileColliderData.ApplyFlippingFlags(tileData);
//        List<Vector2> handlesPos = new List<Vector2>(tile.collData.vertices != null && tile.collData.vertices.Length > 0 ? tileColliderData.vertices : s_fullCollTileVertices);
//        for (int i = 0; i < handlesPos.Count; ++i)
//          handlesPos[i] = m_tilemap.transform.TransformPoint(tileLocPos + Vector2.Scale(handlesPos[i], m_tilemap.CellSize));
//        HandlesEx.DrawDottedPolyLine(handlesPos.Select(x => (Vector3)x).ToArray(), 10, Color.white * 0.6f);
//        if (e.isMouse && e.button == 1)
//          m_tilemap.Tileset.SelectedTileId = Tileset.GetTileIdFromTileData(tileData);
//        if (e.alt)
//        {
//          bool hasColliders = tile.collData.type != eTileCollider.None;
//          Handles.color = hasColliders ? Color.white : Color.black;
//          Vector2 centerPos = m_tilemap.transform.TransformPoint(tileCenter);
//          float size = 0.1f * m_tilemap.CellSize.x;
//          Handles.DrawSolidDisc(centerPos, -m_tilemap.transform.forward, size);
//          if (Handles.Button(centerPos, Quaternion.identity, size, m_tilemap.CellSize.x, EditorCompatibilityUtils.CircleCap))
//          {
//            Undo.RecordObject(m_tilemap.Tileset, "Update Tile Collider");
//            tile.collData.type = tile.collData.type == eTileCollider.None ? eTileCollider.Polygon : eTileCollider.None;
//            updateColliders = true;
//          }
//          Handles.color = Color.white;
//        }
//
//        int closestVertIdx = -1;
//        if (e.shift && m_lastControl == 0)
//        {
//          List<Vector3> polyLine = handlesPos.Select(x => (Vector3)x).ToList();
//          polyLine.Add(polyLine[0]); // close poly
//          Vector3 newVertexHandlePos = ClosestPointToPolyLine(polyLine.ToArray(), out closestVertIdx);
//          if (HandleUtility.DistanceToCircle(newVertexHandlePos, 0f) < 10f)
//          {
//            handlesPos.Insert(closestVertIdx + 1, newVertexHandlePos);
//          }
//        }
//        int selectedIdx = -1;
//        for (int i = 0; i < handlesPos.Count; ++i)
//        {
//          int idx = i; // real index having into account flip and rotation flags
//          if ((tileData & Tileset.k_TileFlag_FlipH) != 0 ^ (tileData & Tileset.k_TileFlag_FlipV) != 0)
//            idx = handlesPos.Count - i - 1;
//          Vector2 handlePos = handlesPos[i];
//          Vector2 vLocVertex = (Vector2)m_tilemap.transform.InverseTransformPoint(handlePos) - tileLocPos;
//          Vector2 oldVertexValue = PointToSnappedVertex(vLocVertex, m_tilemap);
//          if (tile.collData.type != eTileCollider.None)
//          {
//            handlePos = Handles.FreeMoveHandle(handlePos, Quaternion.identity, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Vector3.zero, EditorCompatibilityUtils.CubeCap);
//            if (closestVertIdx >= 0 && i == closestVertIdx + 1)
//              HandlesEx.DrawDotOutline(handlePos, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Color.green, Color.green);
//            else if (e.control && HandleUtility.DistanceToCircle(handlePos, 0f) < 10f)
//            {
//              selectedIdx = idx;
//              HandlesEx.DrawDotOutline(handlePos, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Color.red, Color.red);
//            }
//            vLocVertex = (Vector2)m_tilemap.transform.InverseTransformPoint(handlePos) - tileLocPos;
//            Vector2 newVertexValue = PointToSnappedVertex(vLocVertex, m_tilemap);
//            updateColliders |= (oldVertexValue != newVertexValue) || m_lastControl == 0 && EditorGUIUtility.hotControl != 0 && HandleUtility.DistanceToCircle(handlePos, 0f) < 10f;
//            if (updateColliders)
//            {
//              Undo.RecordObject(m_tilemap.Tileset, "Update Tile Collider");
//              handlesPos[i] = handlePos;
//              tile.collData.type = eTileCollider.Polygon;
//              tile.collData.vertices = handlesPos.Select(x => PointToSnappedVertex((Vector2)m_tilemap.transform.InverseTransformPoint(x) - tileLocPos, m_tilemap)).ToArray();
//              tile.collData.RemoveFlippingFlags(tileData);
//            }
//          }
//          else
//          {
//            HandlesEx.DrawDotOutline(handlePos, 0.1f * HandleUtility.GetHandleSize(m_tilemap.transform.position), Color.gray * 0.2f, Color.gray * 0.2f);
//          }
//          Handles.Label(handlePos, idx.ToString());
//        }
//        if (e.control && HandleUtility.nearestControl == EditorGUIUtility.hotControl && tile.collData.type != eTileCollider.None)
//        {
//          EditorGUIUtility.hotControl = 0;
//          if (selectedIdx >= 0 && tile.collData.vertices.Length > 3)
//          {
//            updateColliders = true;
//            Undo.RecordObject(m_tilemap.Tileset, "Update Tile Collider");
//            ArrayUtility.RemoveAt(ref tile.collData.vertices, selectedIdx);
//          }
//        }
//        // optimize polygon collider if the vertices are they same as full collider
//        if (tile.collData.type == eTileCollider.Polygon && Enumerable.SequenceEqual(tile.collData.vertices, s_fullCollTileVertices))
//          tile.collData.type = eTileCollider.Full;
//        if (updateColliders)
//        {
//          m_tilemap.Refresh(false, true); // This is less optimized but updates all the tilemap
//          EditorUtility.SetDirty(m_tilemap.Tileset);
//        }
//        SceneView.RepaintAll();
//        Handles.color = Color.white;
//      }
//    }
//    Rect rHelpInfoArea = new Rect(0, Screen.height - 100f, 350f, 50f);
//    GUILayout.BeginArea(rHelpInfoArea);
//    string helpInfo =
//        "<b>" +
//        "<color=#FFD500FF>  - Hold " + ((Application.platform == RuntimePlatform.OSXEditor) ? "Option" : "Alt") + " + Click to enable/disable tile colliders" + "</color>\n" +
//        "<color=orange>" +
//        "  - Hold Shift + Click to add a new vertex" + "\n" +
//        "  - Hold " + ((Application.platform == RuntimePlatform.OSXEditor) ? "Command" : "Ctrl") + " + Click to remove a vertex. (should be more than 3)" + "\n" +
//        "  - Click and drag over a vertex to move it" + "\n" +
//        "</color>" +
//        "</b>";
//    GUI.color = new Color(.2f, .2f, .2f, 1f);
//    EditorGUI.TextArea(new Rect(Vector2.zero, rHelpInfoArea.size), helpInfo, Styles.Instance.richHelpBoxStyle);
//    GUI.color = Color.white;
//    GUILayout.EndArea();
//    m_lastControl = EditorGUIUtility.hotControl;
//  }
//
//
//}
//}
//#endif