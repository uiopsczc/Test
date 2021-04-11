#if MicroTileMap
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public partial class TileSetControl
{
  private Rect tile_palette_scroll_area_rect;//tile_palette滚动区域
  private Rect tile_scroll_size_rect;
  private List<uint> visible_tile_list = new List<uint>();
  private Vector2 tile_scroll_speed = Vector2.zero;
  private int visible_tile_count = 1;
  private Vector2 last_tile_palette_scroll_mouse_pos;//上次在tile_palette中scroll_mouse_pos

  public void DrawTilePalette()
  {
    Event e = Event.current;
    is_left_mouse_released = e.type == EventType.MouseUp && e.button == 0;//鼠标左键放开
    is_right_mouse_released = e.type == EventType.MouseUp && e.button == 1;//鼠标右键放开
    is_mouse_inside_tile_palette_scroll_area = e.isMouse && tile_palette_scroll_area_rect.Contains(e.mousePosition);//鼠标是否在tile_palette中
    
    
    //header
    string tileId_label = tileSet.selected_tileId != TileSetConst.TileId_Empty
     ? string.Format("(id:{0})", tileSet.selected_tileId)
     : "";
    EditorGUILayout.LabelField(string.Format("Tile Palette {0}", tileId_label), EditorStyles.boldLabel);
    
    
    sharedTileSetData.tileView_column_count_in_tile_palette = Mathf.Max(1, sharedTileSetData.tileView_column_count_in_tile_palette);
    float tile_area_width = sharedTileSetData.tileView_column_count_in_tile_palette * (tileSet.visual_tile_size.x + visual_tile_padding) + 4f;
    float tile_area_height = (tileSet.visual_tile_size.y + visual_tile_padding) * (1 + (visible_tile_count - 1) / sharedTileSetData.tileView_column_count_in_tile_palette) + 4f;
    sharedTileSetData.tileView_column_count_in_tile_palette = tileView != null ? tileView.tileSelection.column_count : tileSet.column_tile_count_in_palette;
    Vector2 tile_palette_scroll_pos = sharedTileSetData.tile_palette_scroll_pos;
    using (new EditorGUILayoutBeginScrollViewScope(ref tile_palette_scroll_pos, GUIStyleConst.Scroll_Style))
    {
      //更新新tile_palette中的scroll_pos
      if (is_update_scroll_pos)
        sharedTileSetData.tile_palette_scroll_pos = tile_palette_scroll_pos;
      if (e.type == EventType.MouseDrag && (e.button == 1 || e.button == 2))//鼠标中键或者右键拖动中
        sharedTileSetData.tile_palette_scroll_pos = sharedTileSetData.tile_palette_scroll_pos - e.delta;
      else
        DoAutoScroll();

      //记录上次在tile_palette中scroll_mouse_pos
      if (e.isMouse)
        last_tile_palette_scroll_mouse_pos = e.mousePosition;
    
      if (tileSet.tile_list != null)
      {
        GUILayoutUtility.GetRect(tile_area_width, tile_area_height);
        visible_tile_count = 0;
        List<uint> visible_tile_list = new List<uint>();
        int tileView_column_count = tileView != null ? tileView.tileSelection.column_count : tileSet.column_tile_count;
        int tileView_row_count = tileView != null ? ((tileView.tileSelection.selection_tileData_list.Count - 1) / tileView.tileSelection.column_count) + 1 : tileSet.row_tile_count;
        //在tile_palette中全部的tile个数（可能比实际的tile_count多，因为可能出现空的tile来填满行或者列的tile）
        int total_tile_count_in_tile_palette = ((((tileView_column_count - 1) / sharedTileSetData.tileView_column_count_in_tile_palette) + 1) * sharedTileSetData.tileView_column_count_in_tile_palette) * tileView_row_count;
        int tileId_offset = 0;
        
        for (int i = 0; i < total_tile_count_in_tile_palette; ++i)
        {
          //i是在tile_palette中的index（在column_tile_count_in_palette下）
          int tileId = GetTileIdFromIndex(i, sharedTileSetData.tileView_column_count_in_tile_palette, tileView_column_count, tileView_row_count) + tileId_offset;
          uint tileData = (uint)tileId;
          if (tileView != null && tileId != TileSetConst.TileId_Empty)
          {
            //如果是tileVeiw则，上面的tileId是在tileView中的index
            tileData = tileView.tileSelection.selection_tileData_list[tileId - tileId_offset];
            tileId = TileSetUtil.GetTileIdFromTileData(tileData);
          }
          Tile tile = tileSet.GetTile(tileId);
          while (tile != null && tile.uv == default(Rect)) // 忽略无效的tiles（tile.uv == default(Rect),普通的tile是有uv的）
          {
            tile = tileSet.GetTile(++tileId);
            tileData = (uint)tileId;
            tileId_offset = tileId;
          }
          visible_tile_list.Add(tileData);
    
          int tile_x = visible_tile_count % sharedTileSetData.tileView_column_count_in_tile_palette;//在tile_palette中的x
          int tile_y = visible_tile_count / sharedTileSetData.tileView_column_count_in_tile_palette;//在tile_palette中的y
          //当前tile的rect
          Rect visual_tile_rect = new Rect(2 + tile_x * (tileSet.visual_tile_size.x + visual_tile_padding), 2 + tile_y * (tileSet.visual_tile_size.y + visual_tile_padding), tileSet.visual_tile_size.x, tileSet.visual_tile_size.y);
          Rect local_visual_tile_rect = visual_tile_rect;
          local_visual_tile_rect.position -= sharedTileSetData.tile_palette_scroll_pos;//相对于当前的scroll_rect区域矩形位置
          if (local_visual_tile_rect.Overlaps(tile_scroll_size_rect))
          {
            // 画tile （Draw Tile）
            if (tile == null)
              DrawUtil.HandlesDrawSolidRectangleWithOutline(visual_tile_rect, new Color(0f, 0f, 0f, 0.2f), new Color(0f, 0f, 0f, 0.2f));
            else
            {
              DrawUtil.HandlesDrawSolidRectangleWithOutline(visual_tile_rect, new Color(0f, 0f, 0f, 0.1f), new Color(0f, 0f, 0f, 0.1f));
              TileSetEditor.DoGUIDrawTileFromTileData(visual_tile_rect, tileData, tileSet);
            }
            Rect tile_rect = new Rect(2 + tile_x * (tileSet.visual_tile_size.x + visual_tile_padding), 2 + tile_y * (tileSet.visual_tile_size.y + visual_tile_padding), (tileSet.visual_tile_size.x + visual_tile_padding), (tileSet.visual_tile_size.y + visual_tile_padding));
            
            if (visual_tile_rect.Contains(e.mousePosition))//当前tile的矩形包含鼠标
            {
              if (e.type == EventType.MouseDrag && e.button == 0)//鼠标左键点击并且拖动，设置sharedTileSetData.pointed_tile_index_rect
                sharedTileSetData.pointed_tile_index_rect = new KeyValuePair<int, Rect>(visible_tile_count, tile_rect);
              else if (e.type == EventType.MouseDown && e.button == 0)//鼠标左键按下，设置sharedTileSetData.start_drag_tile_index_rect,sharedTileSetData.pointed_tile_index_rect, sharedTileSetData.end_drag_tile_index_rect
              {
                sharedTileSetData.start_drag_tile_index_rect = sharedTileSetData.pointed_tile_index_rect =
                  sharedTileSetData.end_drag_tile_index_rect =
                    new KeyValuePair<int, Rect>(visible_tile_count, tile_rect);
              }
              else if (e.type == EventType.MouseUp && e.button == 0)//放开鼠标左键，设置sharedTileSetData.end_drag_tile_index_rect
              {
                sharedTileSetData.end_drag_tile_index_rect = new KeyValuePair<int, Rect>(visible_tile_count, tile_rect);
                DoSetTileSelection();
              }
            }

            if ((is_left_mouse_released || is_right_mouse_released) //放开鼠标左键或者右键
                && is_mouse_inside_tile_palette_scroll_area  && visual_tile_rect.Contains(e.mousePosition) //鼠标位置处在该tile中
                  && (sharedTileSetData.start_drag_tile_index_rect.Key == sharedTileSetData.end_drag_tile_index_rect.Key) // 没有拖动
                  && tile_scroll_size_rect.Contains(e.mousePosition - sharedTileSetData.tile_palette_scroll_pos))// and it's inside the scroll area
            {
              tileSet.selected_tileId = tileId;
    
              //Give focus to SceneView to get key events
              FocusSceneView();
              //放开鼠标右键，打开显示tile的属性面板
              if (is_right_mouse_released)
                TilePropertiesEditor.Show(tileSet);
            }
            else if (tile != null && tileSet.selected_tileId == tileId)//用黄色外边框选中的tile
              DrawUtil.HandlesDrawSolidRectangleWithOutline(tile_rect, new Color(0f, 0f, 0f, 0.1f),
                new Color(1f, 1f, 0f, 1f));
          }
          ++visible_tile_count;
        }
        this.visible_tile_list = visible_tile_list;
        // 画选中的矩形 用于TileSelection
        if (sharedTileSetData.start_drag_tile_index_rect.Key != sharedTileSetData.pointed_tile_index_rect.Key)
        {
          Rect selection_rect = new Rect(sharedTileSetData.start_drag_tile_index_rect.Value.center, sharedTileSetData.pointed_tile_index_rect.Value.center - sharedTileSetData.start_drag_tile_index_rect.Value.center);
          selection_rect.Set(Mathf.Min(selection_rect.xMin, selection_rect.xMax), Mathf.Min(selection_rect.yMin, selection_rect.yMax), Mathf.Abs(selection_rect.width), Mathf.Abs(selection_rect.height));
          selection_rect.xMin -= sharedTileSetData.start_drag_tile_index_rect.Value.width / 2;
          selection_rect.xMax += sharedTileSetData.start_drag_tile_index_rect.Value.width / 2;
          selection_rect.yMin -= sharedTileSetData.start_drag_tile_index_rect.Value.height / 2;
          selection_rect.yMax += sharedTileSetData.start_drag_tile_index_rect.Value.height / 2;
          DrawUtil.HandlesDrawSolidRectangleWithOutline(selection_rect, new Color(0f, 0f, 0f, 0.1f), new Color(1f, 1f, 1f, 1f));
        }
      }
    }
    if (e.type == EventType.Repaint)
    {
      tile_palette_scroll_area_rect = GUILayoutUtility.GetLastRect();
      tile_scroll_size_rect = tile_palette_scroll_area_rect;
      tile_scroll_size_rect.position = Vector2.zero; // reset position to the Contains and Overlaps inside the tile scroll view without repositioning the position of local positions
      if (tile_area_width > tile_scroll_size_rect.width)
        tile_scroll_size_rect.height -= GUI.skin.verticalScrollbar.fixedWidth;
      if (tile_area_height > tile_scroll_size_rect.height)
        tile_scroll_size_rect.width -= GUI.skin.verticalScrollbar.fixedWidth;
    }
  }


  private void DoAutoScroll()
  {
    Event e = Event.current;
    if (tile_scroll_size_rect.Contains(e.mousePosition - sharedTileSetData.tile_palette_scroll_pos))
    {
      if (e.type == EventType.MouseDrag && e.button == 0)
      {
        Vector2 mouseMoveDisp = e.mousePosition - last_tile_palette_scroll_mouse_pos;
        float auto_scroll_distance = 40;
        float auto_scroll_speed = 500;
        Vector2 mouse_position = e.mousePosition - sharedTileSetData.tile_palette_scroll_pos;
        float left_factor_x = mouseMoveDisp.x < 0f ? 1f - Mathf.Pow(Mathf.Clamp01(mouse_position.x / auto_scroll_distance), 2) : 0f;
        float right_factor_x = mouseMoveDisp.x > 0f ? 1f - Mathf.Pow(Mathf.Clamp01((tile_scroll_size_rect.width - mouse_position.x) / auto_scroll_distance), 2) : 0f;
        float top_factor_y = mouseMoveDisp.y < 0f ? 1f - Mathf.Pow(Mathf.Clamp01(mouse_position.y / auto_scroll_distance), 2) : 0f;
        float bottom_factor_y = mouseMoveDisp.y > 0f ? 1f - Mathf.Pow(Mathf.Clamp01((tile_scroll_size_rect.height - mouse_position.y) / auto_scroll_distance), 2) : 0f;
        tile_scroll_speed = auto_scroll_speed * new Vector2((-left_factor_x + right_factor_x), (-top_factor_y + bottom_factor_y));
      }
      else if (e.type == EventType.MouseUp)
        tile_scroll_speed = Vector2.zero;
    }
    else if (e.isMouse)
      tile_scroll_speed = Vector2.zero;

    sharedTileSetData.tile_palette_scroll_pos += time_delta * tile_scroll_speed;
  }

  //设置tileSelection
  private void DoSetTileSelection()
  {
    if (sharedTileSetData.start_drag_tile_index_rect.Key != sharedTileSetData.end_drag_tile_index_rect.Key)
    {
      int tile_x_start = Mathf.Min(sharedTileSetData.start_drag_tile_index_rect.Key % sharedTileSetData.tileView_column_count_in_tile_palette, sharedTileSetData.end_drag_tile_index_rect.Key % sharedTileSetData.tileView_column_count_in_tile_palette);
      int tile_y_start = Mathf.Min(sharedTileSetData.start_drag_tile_index_rect.Key / sharedTileSetData.tileView_column_count_in_tile_palette, sharedTileSetData.end_drag_tile_index_rect.Key / sharedTileSetData.tileView_column_count_in_tile_palette);
      int tile_x_end = Mathf.Max(sharedTileSetData.start_drag_tile_index_rect.Key % sharedTileSetData.tileView_column_count_in_tile_palette, sharedTileSetData.end_drag_tile_index_rect.Key % sharedTileSetData.tileView_column_count_in_tile_palette);
      int tile_y_end = Mathf.Max(sharedTileSetData.start_drag_tile_index_rect.Key / sharedTileSetData.tileView_column_count_in_tile_palette, sharedTileSetData.end_drag_tile_index_rect.Key / sharedTileSetData.tileView_column_count_in_tile_palette);
      List<uint> selection_tileData_list = new List<uint>();
      int tile_index = 0;
      for (int tile_y = tile_y_end; tile_y >= tile_y_start; --tile_y) // NOTE: this goes from bottom to top to fit the tilemap coordinate system
      {
        for (int tile_x = tile_x_start; tile_x <= tile_x_end; ++tile_x, ++tile_index)
        {
          int visible_tile_index = tile_y * sharedTileSetData.tileView_column_count_in_tile_palette + tile_x;
          uint tileData = visible_tile_list[visible_tile_index];
          selection_tileData_list.Add(tileData);
        }
      }
      
      tileSet.tileSelection = new TileSelection(selection_tileData_list, tile_x_end - tile_x_start + 1);
      FocusSceneView(); //Give focus to SceneView to get key events
    }
  }

  //根据index获取tileId
  //index是在tile_palette中的index（在column_tile_count_in_palette下） 
  private int GetTileIdFromIndex(int index, int column_count_in_tile_palette, int column_count, int row_count)
  {
    int element_count_in_area = column_count_in_tile_palette * row_count;//每个区域area的元素个数(column_count_in_tile_palette * row_count)，这种区域会有多个
    int local_index_in_area = index % element_count_in_area;//在区域area（即[column_count_in_tile_palette，row_count]形成的坐标系）中的index
    if (((index / element_count_in_area) * column_count_in_tile_palette) + (index % column_count_in_tile_palette) >= column_count)
      return TileSetConst.TileId_Empty;
    int local_row_index_in_area = local_index_in_area / column_count_in_tile_palette;//在area中的row_index,和在world_row_index（即[column_count，row_count]形成的坐标系）是一样的
    int local_column_index_in_area = index % column_count_in_tile_palette;//在area中的column_index
    int area_index = index / element_count_in_area;//属于第几个area区域
    return local_row_index_in_area * column_count + area_index * column_count_in_tile_palette + local_column_index_in_area;
  }

}
}
#endif