#if MicroTileMap
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public partial class TileMapEditor
{
  static Color toolbar_box_background_color = new Color(0f, 0f, .4f, 0.4f);
  static Color toolbar_box_outline_color = new Color(.25f, .25f, 1f, 0.70f);
  void DrawToolbar()
  {
    GUIContent tileSetBrush_coords_guiContent = new GUIContent(string.Format("<b> Brush Pos: ({0},{1})</b>", mouse_grid_x, mouse_grid_y));
    GUIContent selected_tileId_or_brushId_guiContent = null;
    if (tileMap.tileSet.selected_tileId != TileSetConst.TileId_Empty)
      selected_tileId_or_brushId_guiContent =
        new GUIContent(string.Format("<b> Selected Tile Id: {0}</b>", tileMap.tileSet.selected_tileId));
    else if (tileMap.tileSet.selected_tileSetBrushId != TileSetConst.TileSetBrushId_Default)
      selected_tileId_or_brushId_guiContent =
        new GUIContent(string.Format("<b> Selected Brush Id: {0}</b>", tileMap.tileSet.selected_tileSetBrushId));
    else
      selected_tileId_or_brushId_guiContent = new GUIContent("<b> Empty tile selected</b>");

    Rect toolbar_rect = new Rect(4f, 4f,
      Mathf.Max(
        Mathf.Max(GUIStyleConst.Toolbar_Box_Style.CalcSize(tileSetBrush_coords_guiContent).x,
          GUIStyleConst.Toolbar_Box_Style.CalcSize(selected_tileId_or_brushId_guiContent).x) + 4f, 180f), 54f);
    using (new HandlesBeginGUIScope())
    {
      using (new GUILayoutBeginAreaScope(toolbar_rect))
      {
        DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, toolbar_rect.size), toolbar_box_background_color,
          toolbar_box_outline_color);

        GUILayout.Space(2f);
        GUILayout.Label(tileSetBrush_coords_guiContent, GUIStyleConst.Toolbar_Box_Style);
        if (selected_tileId_or_brushId_guiContent != null)
          GUILayout.Label(selected_tileId_or_brushId_guiContent, GUIStyleConst.Toolbar_Box_Style);
        GUILayout.Label("<b> F1 - 显示帮助</b>", GUIStyleConst.Toolbar_Box_Style);
        GUILayout.Label("<b> F5 - 刷新tileMap</b>", GUIStyleConst.Toolbar_Box_Style);
      }

      // 显示Toolbar           
      Vector2 toolbar_pos = new Vector2(toolbar_rect.xMax + 4f, toolbar_rect.y);
      Vector2 toolbar_button_size = new Vector2(32f, 32f);
      TileToolbar.instance.tileSetBrushToolbar.SetHighlight((int)TileToolbarEditMode.Erase,
        GetTileSetBrushMode() == TileMapEditorBrushMode.Erase);
      TileToolbar.instance.tileSetBrushToolbar.DrawGUI(toolbar_pos, toolbar_button_size, toolbar_box_background_color,
        toolbar_box_outline_color);

      if (Tools.current != Tool.None && Tools.current != Tool.Rect)
      {
        Rect warn_rect = new Rect(toolbar_pos.x, toolbar_pos.y + toolbar_button_size.y, 370f, 22f);
        using (new GUILayoutBeginAreaScope(warn_rect))
        {
          EditorGUI.HelpBox(new Rect(Vector2.zero, warn_rect.size),
            "选择一个tool_button开始绘画", MessageType.Warning);
        }
      }
      else if (tileMapEditorBrushMode == TileMapEditorBrushMode.Paint)
      {
        toolbar_pos.y += toolbar_button_size.y;
        TileToolbar.instance.tileSetBrushPaintToolbar.DrawGUI(toolbar_pos, toolbar_button_size,
          toolbar_box_background_color * 1.4f, toolbar_box_outline_color * 1.4f);
      }
    }


    if (is_display_help_box)
      DisplayHelpBox();

    if (Event.current.type == EventType.KeyDown)
    {
      if (Event.current.keyCode == KeyCode.F1)
        is_display_help_box = !is_display_help_box;
      else if (Event.current.keyCode == KeyCode.F5)
        tileMap.Refresh(true, true, true, true);
    }
  }


  void DisplayHelpBox()
  {
    string help_content =
      "\n" +
      " - <b>拖动:</b> 鼠标中键\n" +
      " - <b>绘画:</b> 鼠标左键\n" +
      " - <b>删除:</b> Shift + 鼠标左键\n" +
      " - <b>填充:</b> 双击\n\n" +
      " - <b>复制:</b> 右键拖动鼠标\n" +
      " - <b>剪切:</b> 复制的时候按住shift\n" +
      " - <b>选择:</b> 右键点击.\n" +
      "   *按住alt,右键点击,则选中的tile为替换为当前的brush\n" +
      " - <b>旋转和翻转:</b>\n" +
      "   * <b>旋转</b> ±90º 用键盘的 <b>c','</b> 和 <b>'.'</b>\n" +
      "   * <b>垂直翻转</b>点击X键\n" +
      "   * <b>水平翻转</b>点击Y键\n" +
      "   * <i>按住shift则只是旋转或者翻转tile positions</i>\n" +
      "\n - <b>Ctrl-Z/Ctrl-Y</b>Undo/Redo\n";
    GUIContent help_guiContent = new GUIContent(help_content);
    using (new HandlesBeginGUIScope())
    {
      Rect helpBox_rect = new Rect(new Vector2(2f, 74f), GUIStyleConst.Toolbar_Box_Style.CalcSize(help_guiContent));
      using (new GUILayoutBeginAreaScope(helpBox_rect))
      {
        DrawUtil.HandlesDrawSolidRectangleWithOutline(new Rect(Vector2.zero, helpBox_rect.size),
          toolbar_box_background_color, toolbar_box_outline_color);
        GUILayout.Label(help_content, GUIStyleConst.Toolbar_Box_Style);
      }
    }
  }


  private void OnTileSelected(TileSet tileSet, int pre_tileId, int new_tileId)
  {
    Tools.current = Tool.Rect;
    ResetTileSetBrushMode();
    TileSetBrushBehaviour tileSetBrushBehaviour = TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap);
    tileSetBrushBehaviour.tileMap.ClearMap();
    tileSetBrushBehaviour.tileMap.SetTileData(0, 0, (uint)new_tileId);
    tileSetBrushBehaviour.tileMap.UpdateMesh();
    tileSetBrushBehaviour.offset = Vector2.zero;
  }


  private void OnTileSetBrushSelected(TileSet tileSet, int prevBrushId, int newBrushId)
  {
    Tools.current = Tool.Rect;
    ResetTileSetBrushMode();
    TileSetBrushBehaviour tileSetBrushBehaviour = TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap);
    tileSetBrushBehaviour.tileMap.ClearMap();
    tileSetBrushBehaviour.tileMap.SetTileData(0, 0, (uint)(newBrushId << 16) | TileSetConst.TileDataMask_TileId);
    tileSetBrushBehaviour.tileMap.UpdateMesh();
    tileSetBrushBehaviour.offset = Vector2.zero;
  }

  private void OnTileSelectionChanged(TileSet tileSet)
  {
    Tools.current = Tool.Rect;
    ResetTileSetBrushMode();
    TileSetBrushBehaviour tileSetBrushBehaviour = TileSetBrushBehaviour.instance.GetOrCreateTileSetBrushBehaviour(tileMap);
    tileSetBrushBehaviour.tileMap.ClearMap();

    if (tileSet.tileSelection != null)
    {
      for (int i = 0; i < tileSet.tileSelection.selection_tileData_list.Count; ++i)
      {
        int gx = i % tileSet.tileSelection.column_count;
        int gy = i / tileSet.tileSelection.column_count;
        tileSetBrushBehaviour.tileMap.SetTileData(gx, gy, (uint)tileSet.tileSelection.selection_tileData_list[i]);
      }
    }
    tileSetBrushBehaviour.tileMap.UpdateMesh();
    tileSetBrushBehaviour.offset = Vector2.zero;
  }


  void RegisterTilesetEvents(TileSet tileSet)
  {
    if (tileSet != null)
    {
      UnregisterTilesetEvents(tileSet);
      tileSet.OnTileSelected += OnTileSelected;
      tileSet.OnTileSetBrushSelected += OnTileSetBrushSelected;
      tileSet.OnTileSelectionChanged += OnTileSelectionChanged;
    }
  }

  void UnregisterTilesetEvents(TileSet tileSet)
  {
    if (tileSet != null)
    {
      tileSet.OnTileSelected -= OnTileSelected;
      tileSet.OnTileSetBrushSelected -= OnTileSetBrushSelected;
      tileSet.OnTileSelectionChanged -= OnTileSelectionChanged;
    }
  }
}
}
#endif