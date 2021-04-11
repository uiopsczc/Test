#if MicroTileMap
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TileToolbar : ISingleton
{
  public static TileToolbar instance
  {
    get { return SingletonFactory.instance.Get<TileToolbar>(); }
  }

  public GUIToolbar tileSetBrushToolbar;
  public GUIToolbar tileSetBrushPaintToolbar;

  public TileToolbar()
  {
    List<GUIContent> gui_content_list = new List<GUIContent>()
    {
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.pencil_icon_bits), "Paint"),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.erase_icon_bits), "Erase (Hold Shift)"),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.fill_icon_bits), "Fill (Double click)"),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.flip_h_icon_bits),
        string.Format("Flip Horizontal ({0})", TileToolbarConst.Key_FlipH)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.flip_v_icon_bits),
        string.Format("Flip Vertical ({0})", TileToolbarConst.Key_FlipV)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.rotate_90_icon_bits),
        string.Format("Rotate 90 clockwise ({0}); anticlockwise ({1})", TileToolbarConst.Key_Rot90,
          TileToolbarConst.Key_Rot90Back)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.info_icon_bits), " Display Help (F1)"),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.refresh_icon_bits), " Refresh Tilemap (F5)"),
    };
    tileSetBrushToolbar = new GUIToolbar(gui_content_list);
    tileSetBrushToolbar.OnToolSelected += OnToolSelected_tileSetBrushToolbar;

    gui_content_list = new List<GUIContent>()
    {
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.pencil_icon_bits),
        string.Format("Paint ({0})", TileToolbarConst.Key_PencilTool)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.line_icon_bits),
        string.Format("Line ({0})", TileToolbarConst.Key_LineTool)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.rect_icon_bits),
        string.Format("Rect ({0})", TileToolbarConst.Key_RectTool)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.filled_rect_icon_bits),
        string.Format("Filled Rect ({0})", TileToolbarConst.Key_RectTool)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.ellipse_icon_bits),
        string.Format("Ellipse ({0})", TileToolbarConst.Key_EllipseTool)),
      new GUIContent(GUIToolbarUtil.GetToolbarIconTexture(GUIToolbarConst.filled_ellipse_icon_bits),
        string.Format("Filled Ellipse ({0})", TileToolbarConst.Key_EllipseTool)),
    };
    tileSetBrushPaintToolbar = new GUIToolbar(gui_content_list);
    tileSetBrushPaintToolbar.OnToolSelected += OnToolSelected_tileSetBrushPaintToolbar;
  }

  private void OnToolSelected_tileSetBrushPaintToolbar(GUIToolbar toolbar, int selected_tool_index,
    int prev_selected_tool_index)
  {
    TileSetBrushBehaviour.instance.paint_mode = (TileSetBrushPaintMode) selected_tool_index;
  }


  private void OnToolSelected_tileSetBrushToolbar(GUIToolbar toolbar, int selected_tool_index,
    int prev_selected_tool_index)
  {
    var edit_mode = (TileToolbarEditMode) selected_tool_index;
    switch (edit_mode)
    {
      case TileToolbarEditMode.Pencil:
        TileMapEditor.tileMapEditorBrushMode = TileMapEditorBrushMode.Paint;
        Tools.current = Tool.None;
        break;
      case TileToolbarEditMode.Erase:
        TileMapEditor.tileMapEditorBrushMode = TileMapEditorBrushMode.Erase;
        toolbar.TriggerButton(0);
        Tools.current = Tool.None;
        break;
      case TileToolbarEditMode.Fill:
        TileMapEditor.tileMapEditorBrushMode = TileMapEditorBrushMode.Fill;
        toolbar.TriggerButton(0);
        Tools.current = Tool.None;
        break;
      case TileToolbarEditMode.FlipV:
        TileSetBrushBehaviour.instance.FlipV();
        Tools.current = Tool.None;
        toolbar.selected_index = prev_selected_tool_index;
        break;
      case TileToolbarEditMode.FlipH:
        TileSetBrushBehaviour.instance.FlipH();
        Tools.current = Tool.None;
        toolbar.selected_index = prev_selected_tool_index;
        break;
      case TileToolbarEditMode.Rot90:
        TileSetBrushBehaviour.instance.Rot90();
        Tools.current = Tool.None;
        toolbar.selected_index = prev_selected_tool_index;
        break;
      case TileToolbarEditMode.Info:
        TileMapEditor.is_display_help_box = !TileMapEditor.is_display_help_box;
        Tools.current = Tool.None;
        toolbar.selected_index = prev_selected_tool_index;
        toolbar.SetHighlight(selected_tool_index, TileMapEditor.is_display_help_box);
        break;
      case TileToolbarEditMode.Refresh:
        TileMapGroup tileMapGroup = Selection.activeGameObject.GetComponent<TileMapGroup>();
        if (tileMapGroup)
        {
          foreach (var tileMap in tileMapGroup.tileMap_list)
            tileMap.Refresh(true, true, true, true);
        }
        else
        {
          var tileMap = Selection.activeGameObject.GetComponent<TileMap>();
          if (tileMap)
            tileMap.Refresh(true, true, true, true);
        }

        Tools.current = Tool.None;
        toolbar.selected_index = prev_selected_tool_index;
        break;
    }
  }
}
}
#endif