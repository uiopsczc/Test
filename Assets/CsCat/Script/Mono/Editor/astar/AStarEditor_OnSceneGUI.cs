using Boo.Lang;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
  public partial class AStarEditor
  {
    private int mouse_grid_x;
    private int mouse_grid_y;
    private bool is_mouse_grid_changed;
    private Vector2 local_brush_position;
    private int selected_obstacleType_index = 0;
    private bool is_see_obstacleType = true;
    private int selected_terrainType_index = 0;
    private bool is_see_terrainType = true;
    private Event e;

    private AStarObstacleType selected_obstacleType
    {
      get { return AStarEditorConst.AStarObstacleType_List[selected_obstacleType_index]; }
    }

    private AStarTerrainType selected_terrainType
    {
      get { return AStarEditorConst.AStarTerrainType_List[selected_terrainType_index]; }
    }


    void OnSceneGUI()
    {
      int control_id = GUIUtility.GetControlID(FocusType.Passive);
      HandleUtility.AddDefaultControl(control_id);
      UpdateLocalBrushPosition();
      UpdateMouseGrid();

      DrawBounds();
      DrawDataDict();
      brush.DrawBrush(mouse_grid_x, mouse_grid_y, is_see_obstacleType, selected_obstacleType.value, is_see_terrainType,
        selected_terrainType.value);
      DrawInfo();
      DrawMainToolbar();
      HandleEvent();

      SceneView.RepaintAll();
    }

    void UpdateLocalBrushPosition()
    {
      Plane plane = new Plane(target.transform.forward, target.transform.position);
      Vector2 mouse_pos = Event.current.mousePosition;
      mouse_pos.y = Screen.height - mouse_pos.y;
      Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
      float distance;
      if (plane.Raycast(ray, out distance))
      {
        Rect cell_rect = new Rect(0, 0, target.cell_size.x, target.cell_size.y);
        cell_rect.position = target.transform.InverseTransformPoint(ray.GetPoint(distance));

        Vector2 cell_pos = cell_rect.position;
        if (cell_pos.x < 0)
          cell_pos.x -= target.cell_size.x; //因为负数是从-1开始的，正数是从0开始的
        if (cell_pos.y < 0)
          cell_pos.y -= target.cell_size.y; //因为负数是从-1开始的，正数是从0开始的
        cell_pos.x -= cell_pos.x % target.cell_size.x; //取整
        cell_pos.y -= cell_pos.y % target.cell_size.y; //取整
        cell_rect.position = cell_pos;
        local_brush_position = (Vector2) target.transform.InverseTransformPoint(ray.GetPoint(distance));
      }
    }

    void DrawBounds()
    {
      for (int grid_y = target.min_grid_y; grid_y <= target.max_grid_y; grid_y++)
      {
        for (int grid_x = target.min_grid_x; grid_x <= target.max_grid_x; grid_x++)
        {
          Rect cell_rect = new Rect(0, 0, target.cell_size.x, target.cell_size.y);
          cell_rect.position = target.GetPosition(grid_x, grid_y);
          DrawUtil.HandlesDrawSolidRectangleWithOutline(cell_rect, default(Color), Color.white, target.transform);
        }
      }
    }

    void DrawDataDict()
    {
      for (int grid_y = target.min_grid_y; grid_y <= target.max_grid_y; grid_y++)
      {
        for (int grid_x = target.min_grid_x; grid_x <= target.max_grid_x; grid_x++)
        {
          int value = target.GetDataValue(grid_x, grid_y);
          if (value == AStarMonoBehaviourConst.Default_Data_Value)
            continue;
          int obstacleType = AStarUtil.GetObstacleType(value);
          int terrainType = AStarUtil.GetTerrainType(value);
          //draw obstacleType
          if (this.is_see_obstacleType)
            AStarEditorUtil.DrawdObstacleTypeRect(target, grid_x, grid_y, obstacleType);
          // draw terrainType
          if (this.is_see_terrainType)
            AStarEditorUtil.DrawdTerrainTypeRect(target, grid_x, grid_y, terrainType);
        }
      }
    }


    void UpdateMouseGrid()
    {
      e = Event.current;
      int pre_mouse_grid_x = mouse_grid_x;
      int pre_mouse_grid_y = mouse_grid_y;
      if (e.isMouse)
      {
        mouse_grid_x = target.GetGridX(local_brush_position);
        mouse_grid_y = target.GetGridY(local_brush_position);
      }

      is_mouse_grid_changed = pre_mouse_grid_x != mouse_grid_x || pre_mouse_grid_y != mouse_grid_y;
    }


    void DrawInfo()
    {
      string info = "<b>Grid:(" + mouse_grid_x + "," + mouse_grid_y + ") </b>";
      GUIContent info_guiContent = info.ToGUIContent();
      Rect info_rect = new Rect(new Vector2(4f, 4f), GUIStyleConst.Toolbar_Box_Style.CalcSize(info_guiContent));
      using (new HandlesBeginGUIScope())
      {
        using (new GUILayoutBeginAreaScope(info_rect))
        {
          DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, info_rect.size),
            new Color(0, 0, 1, 0.2f), Color.black);
          GUILayout.Label(info, GUIStyleConst.Toolbar_Box_Style);
        }
      }
    }


    void DrawMainToolbar()
    {
      using (new HandlesBeginGUIScope())
      {
        using (new GUILayoutBeginAreaScope(new Rect(120, 4, Screen.width - 40, 80)))
        {
          using (new GUILayoutBeginHorizontalScope())
          {
            DrawObstacleTypeTool();
            GUILayout.Space(20);
            DrawTerrainTypeTool();
          }
        }
      }
    }


    void DrawObstacleTypeTool()
    {
      List<GUIContent> obstacleType_guiContent_list = new List<GUIContent>();
      foreach (var astarObstacleType in AStarEditorConst.AStarObstacleType_List)
      {
        GUIContent guiContent = new GUIContent();
        guiContent.text = astarObstacleType.name;
        guiContent.image = astarObstacleType.GetColorImage();
        obstacleType_guiContent_list.Add(guiContent);
      }

      is_see_obstacleType = EditorGUILayout.Toggle(is_see_obstacleType, new GUIStyle("Toggle"), GUILayout.Width(10));
      selected_obstacleType_index = EditorGUILayout.Popup(selected_obstacleType_index,
        obstacleType_guiContent_list.ToArray(), new GUIStyle("Popup"), GUILayout.Width(80));
    }

    void DrawTerrainTypeTool()
    {
      List<GUIContent> terrainType_guiContent_list = new List<GUIContent>();
      foreach (var astarTerrainType in AStarEditorConst.AStarTerrainType_List)
      {
        GUIContent guiContent = new GUIContent();
        guiContent.text = string.Format("{0}:{1}", astarTerrainType.value, astarTerrainType.name);
        terrainType_guiContent_list.Add(guiContent);
      }

      is_see_terrainType = EditorGUILayout.Toggle(is_see_terrainType, new GUIStyle("Toggle"), GUILayout.Width(10));
      selected_terrainType_index = EditorGUILayout.Popup(selected_terrainType_index,
        terrainType_guiContent_list.ToArray(), new GUIStyle("Popup"), GUILayout.Width(80));
    }




    void HandleEvent()
    {
      if (!e.alt && (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0) //press
      {
        if (!target.IsInRange(mouse_grid_x, mouse_grid_y) && !target.is_enable_edit_outside_bounds)
          return;
        int org_value = target.GetDataValue(mouse_grid_x, mouse_grid_y);
        int obstacleType = AStarUtil.GetObstacleType(org_value);
        int terrainType = AStarUtil.GetTerrainType(org_value);
        if (is_see_obstacleType)
          obstacleType = this.selected_obstacleType.value;
        if (is_see_terrainType)
          terrainType = this.selected_terrainType.value;
        int value = AStarUtil.ToGridType(0, terrainType, obstacleType);
        brush.DoPaintPressed(mouse_grid_x, mouse_grid_y, value);
      }

    }

  }
}