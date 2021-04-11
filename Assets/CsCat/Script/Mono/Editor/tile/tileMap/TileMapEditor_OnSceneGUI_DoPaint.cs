#if MicroTileMap
using System;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
public partial class TileMapEditor
{
  Vector2 start_dragging_pos;
  Vector2 end_dragging_pos;
  Vector2 local_tileSetBrushBehaviour_pos;
  EventModifiers m_prevEventModifiers;
  uint flood_fill_restored_tileData = TileSetConst.TileData_Empty;
  private bool is_dragging;
  

  public void OnSceneGUI_DoPaint()
  {
    Event e = Event.current;
    DrawToolbar();
    if (DragAndDrop.objectReferences.Length > 0 // hide brush when user is dragging a prefab into the scene
        || EditorWindow.mouseOverWindow != SceneView.currentDrawingSceneView // hide brush when it's not over the scene view
        || (Tools.current != Tool.Rect && Tools.current != Tool.None))
    {
      is_tileSetBrush_visible = false;
      SceneView.RepaintAll();
      return;
    }

    int control_id = GUIUtility.GetControlID(FocusType.Passive);
    HandleUtility.AddDefaultControl(control_id);
    EventType current_event_type = Event.current.GetTypeForControl(control_id);
    bool is_skip = false;
    int save_control = GUIUtility.hotControl;

    try
    {
      if (current_event_type == EventType.Layout)
        is_skip = true;
      else if (current_event_type == EventType.ScrollWheel)
        is_skip = true;

      if (tileMap.tileSet == null)
        return;

      if (!is_skip)
      {
        if (e.type == EventType.KeyDown)
        {
          switch (e.keyCode)
          {
            case TileToolbarConst.Key_FlipH:
              TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap).FlipH(!e.shift);
              e.Use(); // Use key event
              break;
            case TileToolbarConst.Key_FlipV:
              TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap).FlipV(!e.shift);
              e.Use(); // Use key event
              break;
            case TileToolbarConst.Key_Rot90:
              TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap).Rot90(!e.shift);
              e.Use(); // Use key event
              break;
            case TileToolbarConst.Key_Rot90Back:
              TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap).Rot90Back(!e.shift);
              e.Use(); // Use key event
              break;
            case TileToolbarConst.Key_PencilTool:
            case TileToolbarConst.Key_LineTool:
            case TileToolbarConst.Key_RectTool:
            case TileToolbarConst.Key_EllipseTool:
              switch (e.keyCode)
              {
                case TileToolbarConst.Key_PencilTool:
                  TileSetBrushBehaviour.instance.paint_mode = TileSetBrushPaintMode.Pencil;
                  break;
                case TileToolbarConst.Key_LineTool:
                  TileSetBrushBehaviour.instance.paint_mode = TileSetBrushPaintMode.Line;
                  break;
                case TileToolbarConst.Key_RectTool:
                  TileSetBrushBehaviour.instance.paint_mode =
                    TileSetBrushBehaviour.instance.paint_mode == TileSetBrushPaintMode.Rect
                      ? TileSetBrushPaintMode.FilledRect
                      : TileSetBrushPaintMode.Rect;//在filledRect和Rect之间切换
                  break;
                case TileToolbarConst.Key_EllipseTool:
                  TileSetBrushBehaviour.instance.paint_mode =
                    TileSetBrushBehaviour.instance.paint_mode == TileSetBrushPaintMode.Ellipse
                      ? TileSetBrushPaintMode.FilledEllipse
                      : TileSetBrushPaintMode.Ellipse;//在FilledEllipse和Ellipse之间切换
                  break;
              }

              tileSetBrush_mode = TileMapEditorBrushMode.Paint;
              TileToolbar.instance.tileSetBrushToolbar.TriggerButton((int) tileSetBrush_mode);
              TileToolbar.instance.tileSetBrushPaintToolbar.TriggerButton((int) TileSetBrushBehaviour.instance
                .paint_mode);
              e.Use();
              break;
          }
        }

        EditorGUIUtility.AddCursorRect(new Rect(0f, 0f, (float) Screen.width, (float) Screen.height),
          MouseCursor.Arrow);
        GUIUtility.hotControl = control_id;
        {
          Plane tileMapChunk_plane = new Plane(tileMap.transform.forward, tileMap.transform.position);
          Vector2 mouse_pos = Event.current.mousePosition;
          mouse_pos.y = Screen.height - mouse_pos.y;
          Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
          float distance;
          if (tileMapChunk_plane.Raycast(ray, out distance))
          {
            Rect tile_rect = new Rect(0, 0, tileMap.cell_size.x, tileMap.cell_size.y);
            tile_rect.position = tileMap.transform.InverseTransformPoint(ray.GetPoint(distance));

            Vector2 tile_pos = tile_rect.position;
            if (tile_pos.x < 0)
              tile_pos.x -= tileMap.cell_size.x;//因为负数是从-1开始的，正数是从0开始的
            if (tile_pos.y < 0)
              tile_pos.y -= tileMap.cell_size.y;//因为负数是从-1开始的，正数是从0开始的
            tile_pos.x -= tile_pos.x % tileMap.cell_size.x;//取整
            tile_pos.y -= tile_pos.y % tileMap.cell_size.y;//取整
            tile_rect.position = tile_pos;


            Vector2 start_pos = new Vector2(Mathf.Min(start_dragging_pos.x, end_dragging_pos.x),
              Mathf.Min(start_dragging_pos.y, end_dragging_pos.y));
            Vector2 end_pos = new Vector2(Mathf.Max(start_dragging_pos.x, end_dragging_pos.x),
              Mathf.Max(start_dragging_pos.y, end_dragging_pos.y));
            Vector2 selection_snapped_pos = TileSetBrushUtil.GetSnappedPosition(start_pos,tileMap.cell_size);
            Vector2 selection_size = TileSetBrushUtil.GetSnappedPosition(end_pos,tileMap.cell_size) - selection_snapped_pos + tileMap.cell_size;

            TileSetBrushBehaviour tileSetBrushBehaviour = TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap);
            // Update brush transform
            local_tileSetBrushBehaviour_pos = (Vector2) tileMap.transform.InverseTransformPoint(ray.GetPoint(distance));//设置tileSetBrushBehaviour的position
            Vector2 tileSetBrushBehaviour_snapped_pos = TileSetBrushUtil.GetSnappedPosition((tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos),tileMap.cell_size);
            tileSetBrushBehaviour.transform.rotation = tileMap.transform.rotation;
            tileSetBrushBehaviour.transform.localScale = tileMap.transform.lossyScale;
            if (!TileSetBrushBehaviour.instance.is_dragging)//不是拖动的时候
            {
              tileSetBrushBehaviour.transform.position =
                tileMap.transform.TransformPoint(new Vector3(tileSetBrushBehaviour_snapped_pos.x, tileSetBrushBehaviour_snapped_pos.y, -0.01f));
            }

            int pre_mouse_grid_x = mouse_grid_x;
            int pre_mouse_grid_y = mouse_grid_y;
            if (e.isMouse)
            {
              mouse_grid_x = TileSetBrushUtil.GetGridX(local_tileSetBrushBehaviour_pos, tileMap.cell_size);
              mouse_grid_y = TileSetBrushUtil.GetGridY(local_tileSetBrushBehaviour_pos, tileMap.cell_size);
            }

            bool is_mouse_grid_changed = pre_mouse_grid_x != mouse_grid_x || pre_mouse_grid_y != mouse_grid_y;
            //Update Fill Preview
            if (GetTileSetBrushMode() == TileMapEditorBrushMode.Fill && is_mouse_grid_changed)
            {
              fill_preview_tile_pos_list.Clear();
              TileMapDrawingUtil.FloodFillPreview(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos,
                tileSetBrushBehaviour.tileMap.GetTileData(0, 0), fill_preview_tile_pos_list);
            }

            bool is_modifiers_changed = false;
            if (e.isKey)
            {
              EventModifiers filtered_modifiers = e.modifiers & (EventModifiers.Control | EventModifiers.Alt);
              is_modifiers_changed = filtered_modifiers != m_prevEventModifiers;
              m_prevEventModifiers = filtered_modifiers;
            }

            if (
              (EditorWindow.focusedWindow ==
               EditorWindow.mouseOverWindow
              ) && // fix painting tiles when closing another window popup over the SceneView like GameObject Selection window
              (e.type == EventType.MouseDown || e.type == EventType.MouseDrag && is_mouse_grid_changed ||
               e.type == EventType.MouseUp || is_modifiers_changed)
            )
            {
              if (e.button == 0)
              {
                if (mouseDoubleClick.IsDoubleClick && tileSetBrushBehaviour.tileMap.GridWidth == 1 &&
                    tileSetBrushBehaviour.tileMap.GridHeight == 1)
                {
                  // Restore previous tiledata modified by Paint, because before the double click, a single click is done before
                  tileMap.SetTileData(tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos, flood_fill_restored_tileData);
                  tileSetBrushBehaviour.FloodFill(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos);
                }
                // Do a brush paint action
                else
                {
                  switch (GetTileSetBrushMode())
                  {
                    
                    case TileMapEditorBrushMode.Paint:
                      if (e.type == EventType.MouseDown)
                        flood_fill_restored_tileData = tileMap.GetTileData(mouse_grid_x, mouse_grid_y);

                      if (e.type == EventType.MouseDown)
                        tileSetBrushBehaviour.DoPaintPressed(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos, e.modifiers);
                      else if (e.type == EventType.MouseDrag || is_modifiers_changed &&
                               TileSetBrushBehaviour.instance.paint_mode != TileSetBrushPaintMode.Pencil)
                        tileSetBrushBehaviour.DoPaintDragged(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos, e.modifiers);
                      else if (e.type == EventType.MouseUp)
                        tileSetBrushBehaviour.DoPaintReleased(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos, e.modifiers);
                      break;
                    case TileMapEditorBrushMode.Erase:
                      tileSetBrushBehaviour.Erase(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos);
                      break;
                    case TileMapEditorBrushMode.Fill:
                      tileSetBrushBehaviour.FloodFill(tileMap, tileSetBrushBehaviour.offset + local_tileSetBrushBehaviour_pos);
                      break;
                  }
                }
              }
              else if (e.button == 1)
              {
                if (e.type == EventType.MouseDown)
                {
                  is_dragging = true;
                  tileSetBrushBehaviour.tileMap.ClearMap();
                  start_dragging_pos = end_dragging_pos = local_tileSetBrushBehaviour_pos;
                }
                else
                {
                  end_dragging_pos = local_tileSetBrushBehaviour_pos;
                }
              }
            }

            if (e.type == EventType.MouseUp)
            {
              if (e.button == 1) // right mouse button
              {
                is_dragging = false;
                ResetTileSetBrushMode();
                // Copy one tile
                if (selection_size.x <= tileMap.cell_size.x && selection_size.y <= tileMap.cell_size.y)
                {
                  uint tileData = tileMap.GetTileData(local_tileSetBrushBehaviour_pos);
                  //Select the first tile not null if any and select the tilemap
                  if (e.control && tileMap.parent_tileMapGroup)
                  {
                    for (int i = tileMap.parent_tileMapGroup.tileMap_list.Count - 1; i >= 0; --i)
                    {
                      var tileMap = this.tileMap.parent_tileMapGroup.tileMap_list[i];
                      tileData = tileMap.GetTileData(local_tileSetBrushBehaviour_pos);
                      if (tileData != TileSetConst.TileData_Empty)
                      {
                        tileMap.parent_tileMapGroup.selected_tileMap = tileMap;
                        if (Selection.activeGameObject == this.tileMap.gameObject)
                          Selection.activeGameObject = tileMap.gameObject;
                        break;
                      }
                    }
                  }

                  if (tileData == TileSetConst.TileData_Empty)
                  {
                    tileMap.tileSet.selected_tileId = TileSetConst.TileId_Empty;
                    tileSetBrushBehaviour.tileMap.SetTileData(0, 0, TileSetConst.TileData_Empty);
                  }
                  else
                  {
                    int brushId = TileSetUtil.GetTileSetBrushIdFromTileData(tileData);
                    int tileId = TileSetUtil.GetTileIdFromTileData(tileData);

                    // Select the copied tile in the tileset
                    if (brushId > 0 && !e.alt) //NOTE: if Alt is held, the tile is selected instead
                    {
                      tileMap.tileSet.selected_tileSetBrushId = brushId;
                    }
                    else
                    {
                      tileMap.tileSet.selected_tileId = tileId;
                      tileSetBrushBehaviour.tileMap.SetTileData(0, 0,
                        tileData & ~TileSetConst.TileDataMask_TileSetBrushId); // keep tile flags
                    }
                  }

                  // Cut tile if key shift is pressed
                  if (e.shift)
                  {
                    int startGridX = TileSetBrushUtil.GetGridX(start_pos, tileMap.cell_size);
                    int startGridY = TileSetBrushUtil.GetGridY(start_pos, tileMap.cell_size);
                    tileSetBrushBehaviour.CutRect(tileMap, startGridX, startGridY, startGridX, startGridY);
                  }

                  tileSetBrushBehaviour.tileMap.UpdateMesh();
                  tileSetBrushBehaviour.offset = Vector2.zero;
                }
                // copy a rect of tiles
                else
                {
                  int startGridX = TileSetBrushUtil.GetGridX(start_pos, tileMap.cell_size);
                  int startGridY = TileSetBrushUtil.GetGridY(start_pos, tileMap.cell_size);
                  int endGridX = TileSetBrushUtil.GetGridX(end_pos, tileMap.cell_size);
                  int endGridY = TileSetBrushUtil.GetGridY(end_pos, tileMap.cell_size);

                  // Cut tile if key shift is pressed
                  if (e.shift)
                  {
                    tileSetBrushBehaviour.CutRect(tileMap, startGridX, startGridY, endGridX, endGridY);
                  }
                  else
                  {
                    tileSetBrushBehaviour.CopyRect(tileMap, startGridX, startGridY, endGridX, endGridY);
                  }

                  tileSetBrushBehaviour.offset.x = end_dragging_pos.x > start_dragging_pos.x
                    ? -(endGridX - startGridX) * tileMap.cell_size.x
                    : 0f;
                  tileSetBrushBehaviour.offset.y = end_dragging_pos.y > start_dragging_pos.y
                    ? -(endGridY - startGridY) * tileMap.cell_size.y
                    : 0f;
                }
              }
            }

            if (is_dragging)
            {
              Rect rGizmo = new Rect(selection_snapped_pos, selection_size);
              DrawUtil.HandlesDrawSolidRectangleWithOutline(rGizmo, new Color(), Color.white, tileMap.transform);
            }
            else // Draw brush border
            {
              Rect rBound = new Rect(tileSetBrushBehaviour.tileMap.tileMapBounds.min,
                tileSetBrushBehaviour.tileMap.tileMapBounds.size);
              Color fillColor;
              switch (GetTileSetBrushMode())
              {
                case TileMapEditorBrushMode.Paint:
                  fillColor = new Color(0, 0, 0, 0);
                  break;
                case TileMapEditorBrushMode.Erase:
                  fillColor = new Color(1f, 0f, 0f, 0.1f);
                  break;
                case TileMapEditorBrushMode.Fill:
                  fillColor = new Color(1f, 1f, 0f, 0.2f);
                  break;
                default:
                  fillColor = new Color(0, 0, 0, 0);
                  break;
              }

              DrawUtil.HandlesDrawSolidRectangleWithOutline(rBound, fillColor, new Color(1, 1, 1, 0.2f),
                tileSetBrushBehaviour.transform);
            }
          }
        }

        if (current_event_type == EventType.MouseDrag && Event.current.button < 2) // 2 is for central mouse button
        {
          // avoid dragging the map
          Event.current.Use();
        }
      }
    }
    catch (Exception exception)
    {
      Debug.LogException(exception);
    }

    SceneView.RepaintAll();
    GUIUtility.hotControl = save_control;
  }

  
}
}
#endif