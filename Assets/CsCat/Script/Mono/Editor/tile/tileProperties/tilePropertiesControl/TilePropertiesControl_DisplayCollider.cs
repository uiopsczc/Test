#if MicroTileMap
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TilePropertiesControl
{
  
  private Vector2[] saved_vertexs;
  private bool is_dragging = false;
  private Color pre_background_color;
  private Vector2 mouse_pos;
  private Rect tile_rect;
  private int active_vertex_index = -1;
  private static TileColliderData copied_colliderData;

  private void DisplayCollider()
  {
    Event e = Event.current;
    if (tileSet.selected_tileSetBrushId != TileSetConst.TileSetBrushId_Default)
    {
      EditorGUILayout.LabelField("tileSetBrush不能被编辑", EditorStyles.boldLabel);
      return;
    }

    bool is_multi_selection = tileSet.tileSelection != null;
    bool is_save_changes = false;
    Tile selected_tile = is_multi_selection
      ? tileSet.tile_list[TileSetUtil.GetTileIdFromTileData(tileSet.tileSelection.selection_tileData_list[0])]
      : tileSet.selected_tile;

    if (e.type == EventType.MouseDown)
    {
      is_dragging = true;
      if (selected_tile.tileColliderData.vertices != null)
      {
        saved_vertexs = new Vector2[selected_tile.tileColliderData.vertices.Length];
        selected_tile.tileColliderData.vertices.CopyTo(saved_vertexs, 0);
      }
    }
    else if (e.type == EventType.MouseUp)
      is_dragging = false;

    //background_color
    tileSet.background_color = EditorGUILayout.ColorField(tileSet.background_color);
    if (pre_background_color != tileSet.background_color || GUIStyleConst.Scroll_Style.normal.background == null)
    {
      pre_background_color = tileSet.background_color;
      if (GUIStyleConst.Scroll_Style.normal.background == null)
        GUIStyleConst.Scroll_Style.normal.background = new Texture2D(1, 1) { hideFlags = HideFlags.DontSave };
      GUIStyleConst.Scroll_Style.normal.background.SetPixel(0, 0, tileSet.background_color);
      GUIStyleConst.Scroll_Style.normal.background.Apply();
    }

    float aspect_ratio = tileSet.tile_pixel_size.x / tileSet.tile_pixel_size.y;//比例
    float padding = 2; // pixel size of the border around the tile

    //画tile图
    //包围tile_rect的rect
    Rect tile_parent_rect = GUILayoutUtility.GetRect(1, 1, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
    using (new GUIBeginGroupScope(tile_parent_rect, GUIStyleConst.Scroll_Style))
    {
      if (e.type == EventType.Repaint)
      {
        float tile_pixel_size = tile_parent_rect.width / (tileSet.tile_pixel_size.x + 2 * padding);//2*padding是因为padding有左边和右边
        mouse_pos = e.mousePosition;
        tile_rect = new Rect(padding * tile_pixel_size, padding * tile_pixel_size,
          tile_parent_rect.width -  2* padding * tile_pixel_size,
          (tile_parent_rect.width / aspect_ratio) - 2 * padding * tile_pixel_size);
        tile_rect.height = Mathf.Min(tile_rect.height, tile_parent_rect.height - 2 * padding * tile_pixel_size);
        tile_rect.width = (tile_rect.height * aspect_ratio);
      }
      using (new GUIColorScope(new Color(1f, 1f, 1f, 0.1f)))
      {
        GUI.DrawTexture(tile_rect, EditorGUIUtility.whiteTexture);
      }
      if (is_multi_selection)
      {
        foreach (uint tileData in tileSet.tileSelection.selection_tileData_list)
        {
          int tileId = TileSetUtil.GetTileIdFromTileData(tileData);
          Tile tile = tileSet.GetTile(tileId);
          if (tile != null)
          {
            GUI.color = new Color(1f, 1f, 1f, 1f / tileSet.tileSelection.selection_tileData_list.Count);
            GUI.DrawTextureWithTexCoords(tile_rect, tileSet.atlas_texture, tile.uv);
          }
        }
        GUI.color = Color.white;
      }
      else
        GUI.DrawTextureWithTexCoords(tile_rect, tileSet.atlas_texture, selected_tile.uv);

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      Color saved_handle_color = Handles.color;
      if (selected_tile.tileColliderData.type != TileColliderType.None)
      {
        Vector2[] collider_vertices = selected_tile.tileColliderData.type == TileColliderType.Full ? TileConst.Full_Collider_Tile_Vertices : selected_tile.tileColliderData.vertices;
        if (collider_vertices == null || collider_vertices.Length == 0)
        {
          collider_vertices = selected_tile.tileColliderData.vertices = new Vector2[TileConst.Full_Collider_Tile_Vertices.Length];
          Array.Copy(TileConst.Full_Collider_Tile_Vertices, collider_vertices, TileConst.Full_Collider_Tile_Vertices.Length);
          EditorUtility.SetDirty(tileSet);
        }

        // 将vertice的xy限制在[0,1]之间
        for (int i = 0; i < collider_vertices.Length; ++i)
        {
          Vector2 collider_vertice = collider_vertices[i];
          collider_vertice = collider_vertice.Snap2(tileSet.tile_pixel_size).ConvertElement(v=>Mathf.Clamp01(v));
          collider_vertices[i] = collider_vertice;
        }

        // 画边 draw edges
        Vector3[] poly_edges = new Vector3[collider_vertices.Length + 1];
        for (int i = 0; i < collider_vertices.Length; ++i)
        {
          Vector2 collider_vertice = collider_vertices[i];
          collider_vertice.x = tile_rect.x + tile_rect.width * collider_vertice.x;
          collider_vertice.y = tile_rect.yMax - tile_rect.height * collider_vertice.y;

          Vector2 collider_vertice_next = collider_vertices[(i + 1) % collider_vertices.Length];
          collider_vertice_next.x = tile_rect.x + tile_rect.width * collider_vertice_next.x;
          collider_vertice_next.y = tile_rect.yMax - tile_rect.height * collider_vertice_next.y;

          poly_edges[i] = collider_vertice;
          poly_edges[i + 1] = collider_vertice_next;

          //画边
          Handles.color = Color.green;
          Handles.DrawLine(collider_vertice, collider_vertice_next);
          //画边上的法线 Draw normals
          Handles.color = Color.white;
          Vector3 normal_pos = (collider_vertice + collider_vertice_next) / 2f;
          Handles.DrawLine(normal_pos, normal_pos + Vector3.Cross(collider_vertice_next - collider_vertice, Vector3.forward).normalized * tile_rect.yMin);

          Handles.color = saved_handle_color;
        }

        float pixel_size = tile_rect.width / tileSet.tile_pixel_size.x;
        if (selected_tile.tileColliderData.type == TileColliderType.Polygon)
        {
          bool is_adding_vertex_on = !is_dragging && e.shift && active_vertex_index == -1;
          bool is_removing_vertex_on = !is_dragging && ((Application.platform == RuntimePlatform.OSXEditor) ? e.command : e.control) && collider_vertices.Length > 3;
          //删除顶点
          if (is_removing_vertex_on && active_vertex_index != -1 && e.type == EventType.MouseUp)
          {
            selected_tile.tileColliderData.vertices = new Vector2[collider_vertices.Length - 1];
            for (int i = 0, j = 0; i < collider_vertices.Length; ++i)
            {
              if (i == active_vertex_index)
                continue;
              selected_tile.tileColliderData.vertices[j] = collider_vertices[i];
              ++j;
            }
            collider_vertices = selected_tile.tileColliderData.vertices;
            active_vertex_index = -1;
          }

          float min_distance = float.MaxValue;
          if (!is_dragging)
            active_vertex_index = -1;
          for (int i = 0; i < collider_vertices.Length; ++i)
          {
            Vector2 collider_vertice = collider_vertices[i];
            collider_vertice.x = tile_rect.x + tile_rect.width * collider_vertice.x;
            collider_vertice.y = tile_rect.yMax - tile_rect.height * collider_vertice.y;

            if (is_dragging)
            {
              if (i == active_vertex_index)
              {
                collider_vertice = mouse_pos;
                collider_vertice -= tile_rect.position;
                collider_vertice.x = Mathf.Clamp(Mathf.Round(collider_vertice.x / pixel_size) * pixel_size, 0, tile_rect.width);
                collider_vertice.y = Mathf.Clamp(Mathf.Round(collider_vertice.y / pixel_size) * pixel_size, 0, tile_rect.height);
                collider_vertice += tile_rect.position;
                collider_vertices[i].x = Mathf.Clamp01((collider_vertice.x - tile_rect.x) / tile_rect.width);
                collider_vertices[i].y = Mathf.Clamp01((collider_vertice.y - tile_rect.yMax) / -tile_rect.height);
              }
            }
            else
            {
              float distance = Vector2.Distance(mouse_pos, collider_vertice);
              //distance < GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.width是为了限定少于多少距离（该顶点与mouse_pos的距离）的时候才选中该顶点
              if (distance <= min_distance && distance < GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.width)
              {
                min_distance = distance;
                active_vertex_index = i;
              }
            }
            //画顶点
            if (e.type == EventType.Repaint)
            {
              //画左上角的坐标
              if (i == active_vertex_index)
              {
                GUIStyleConst.VertexCoord_Style.fontSize = (int)(Mathf.Min(12f, tile_rect.yMin / 2f));
                GUI.Label(new Rect(0, 0, tile_rect.width, tile_rect.yMin), Vector2.Scale(collider_vertices[i], tileSet.tile_pixel_size).ToString(), GUIStyleConst.VertexCoord_Style);
              }
              GUI.color = active_vertex_index == i ? (is_removing_vertex_on ? Color.red : Color.cyan) : new Color(0.7f, 0.7f, 0.7f, 0.8f);
              GUIStyleConst.Collider_Vertex_Handle_Style.Draw(new Rect(collider_vertice.x - GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.width / 2, collider_vertice.y - GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.height / 2, 1, 1), i.ToString(), false, false, false, false);
              GUI.color = Color.white;
            }
          }

          //添加顶点
          if (is_adding_vertex_on)
          {
            //线段index
            int segment_index;
            Vector2 new_vertex_pos = ClosestPointToPolyLine(poly_edges, out segment_index);

            if (e.type == EventType.MouseUp)
            {
              selected_tile.tileColliderData.vertices = new Vector2[collider_vertices.Length + 1];
              segment_index = (segment_index + 1) % selected_tile.tileColliderData.vertices.Length;
              for (int i = 0, j = 0; i < selected_tile.tileColliderData.vertices.Length; ++i)
              {
                if (segment_index == i)
                {
                  new_vertex_pos.x = Mathf.Clamp(Mathf.Round(new_vertex_pos.x / pixel_size) * pixel_size, tile_rect.x, tile_rect.xMax);
                  new_vertex_pos.y = Mathf.Clamp(Mathf.Round(new_vertex_pos.y / pixel_size) * pixel_size, tile_rect.y, tile_rect.yMax);
                  selected_tile.tileColliderData.vertices[i].x = Mathf.Clamp01((new_vertex_pos.x - tile_rect.x) / tile_rect.width);
                  selected_tile.tileColliderData.vertices[i].y = Mathf.Clamp01((new_vertex_pos.y - tile_rect.yMax) / -tile_rect.height);
                }
                else
                {
                  selected_tile.tileColliderData.vertices[i] = collider_vertices[j];
                  ++j;
                }
              }
              collider_vertices = selected_tile.tileColliderData.vertices;
              active_vertex_index = -1;
            }
            else if (e.type == EventType.Repaint)
            {
              //添加顶点中移动鼠标，透明动态显示当前将要新增顶点的位置
              GUI.color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
              GUIStyleConst.Collider_Vertex_Handle_Style.Draw(new Rect(new_vertex_pos.x - GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.width / 2, new_vertex_pos.y - GUIStyleConst.Collider_Vertex_Handle_Style.normal.background.height / 2, 1, 1), segment_index.ToString(), false, false, false, false);
              GUI.color = Color.white;
            }
          }
        }

        if (e.type == EventType.MouseUp)
        {
          is_save_changes = true;
          //remove duplicated vertex
          selected_tile.tileColliderData.vertices = selected_tile.tileColliderData.vertices.Distinct().ToArray();
          if (selected_tile.tileColliderData.vertices.Length <= 2)
            selected_tile.tileColliderData.vertices = saved_vertexs;
          //snap vertex positions
          selected_tile.tileColliderData.SnapVertices(tileSet);
        }
      }
    }
    
    

    if (GUILayout.Button("反转法线"))
      selected_tile.tileColliderData.vertices.Reverse();

    EditorGUILayout.Space();

    string helpInfo =
      "  - 移动:点击图中的顶点并移动它的位置" + "\n" +
      "  - 添加:按住Shift并点击鼠标进行添加顶点" + "\n" +
      "  - 删除:按住Ctrl并且点击顶点进行删除顶点 (should be more than 3)";
    EditorGUILayout.HelpBox(helpInfo, MessageType.Info);

    // Collider Settings
    float saved_label_width = EditorGUIUtility.labelWidth;
    EditorGUIUtility.labelWidth = 40;
    if (is_multi_selection)
      EditorGUILayout.LabelField("* Multi-selection Edition", EditorStyles.boldLabel);
    using (new EditorGUILayoutBeginHorizontalScope(GUILayout.MinWidth(140)))
    {
      EditorGUILayout.LabelField("Collider Data", EditorStyles.boldLabel);
      if (GUILayout.Button("Copy", GUILayout.Width(50)))
        copied_colliderData = selected_tile.tileColliderData.Clone();
      if (GUILayout.Button("Paste", GUILayout.Width(50)))
      {
        selected_tile.tileColliderData = copied_colliderData.Clone();
        is_save_changes = true;
      }
    }
    EditorGUILayout.Space();
    
    using (var check = new EditorGUIBeginChangeCheckScope())
    {
      EditorGUIUtility.labelWidth = 100;
      EditorGUILayout.LabelField("Collider Type:", EditorStyles.boldLabel);
      using (new EditorGUIIndentLevelScope(2))
      {
        string[] tile_collider_names = Enum.GetNames(typeof(TileColliderType));
        selected_tile.tileColliderData.type = (TileColliderType)GUILayout.Toolbar((int)selected_tile.tileColliderData.type, tile_collider_names);
      }
      EditorGUIUtility.labelWidth = saved_label_width;
      is_save_changes |= check.IsChanged;
    }

    //Save changes
    if (is_save_changes)
    {
      if (is_multi_selection)
      {
        for (int i = 0; i < tileSet.tileSelection.selection_tileData_list.Count; ++i)
          tileSet.tile_list[TileSetUtil.GetTileIdFromTileData(tileSet.tileSelection.selection_tileData_list[i])].tileColliderData = selected_tile.tileColliderData.Clone();
      }
      EditorUtility.SetDirty(tileSet);
      //Refresh selected tileMap
      TileMap selected_tileMap = Selection.activeGameObject ? Selection.activeGameObject.GetComponent<TileMap>() : null;
      if (selected_tileMap)
        selected_tileMap.Refresh(false, true);
    }
  }

  //获取polyline（vertices组成的线段）线段上的离当前mouse poistion最近的点
  //Get the point on a polyline (in 3D space) which is closest to the current mouse position
  Vector3 ClosestPointToPolyLine(Vector3[] vertices, out int closest_segment_index)
  {
    float min_distance = float.MaxValue;
    closest_segment_index = 0;
    for (int i = 0; i < vertices.Length - 1; ++i)
    {
      float distance = HandleUtility.DistanceToLine(vertices[i], vertices[i + 1]);
      if (distance < min_distance)
      {
        min_distance = distance;
        closest_segment_index = i;
      }
    }
    return HandleUtility.ClosestPointToPolyLine(vertices);
  }
}
}
#endif