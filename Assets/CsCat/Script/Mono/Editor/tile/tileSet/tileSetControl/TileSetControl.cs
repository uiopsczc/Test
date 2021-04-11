#if MicroTileMap
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
[Serializable]
public partial class TileSetControl
{
  private TileSet _tileSet;
  public TileSet tileSet { get { return _tileSet; } set { if (_tileSet != value) { is_update_scroll_pos = false; _tileSet = value; } } }
  private MouseDoubleClick mouseDoubleClick = new MouseDoubleClick();
  
  private float last_time;
  private float time_delta;
  private SharedTileSetData sharedTileSetData;
  private Dictionary<TileSet, SharedTileSetData> sharedTileSetData_dict = new Dictionary<TileSet, SharedTileSetData>();
  private float visual_tile_padding = 2;//tile_palette中的tile间隔
  private bool is_update_scroll_pos;//是否更新scroll_pos

  private bool is_left_mouse_released;
  private bool is_right_mouse_released;
  private bool is_mouse_inside_tile_palette_scroll_area;


  public void Display()
  {
    AssetPreview.SetPreviewTextureCacheSize(256);
    Event e = Event.current;
    mouseDoubleClick.Update();
    if(e.isMouse || e.type == EventType.ScrollWheel)
      is_update_scroll_pos = true;
    if (e.type == EventType.Layout && selected_tileSetBrush_in_inspector != null)
    {
      Selection.activeObject = selected_tileSetBrush_in_inspector;
      selected_tileSetBrush_in_inspector = null;
    }
    if(last_time == 0f)
      last_time = Time.realtimeSinceStartup;
    time_delta = Time.realtimeSinceStartup - last_time;
    last_time = Time.realtimeSinceStartup;
    if (tileSet == null)
    {
      EditorGUILayout.HelpBox("没有选中tileSet", MessageType.Info);
      return;
    }
    if (tileSet.atlas_texture == null)
    {
      EditorGUILayout.HelpBox("tileSet中没有atlas_texture", MessageType.Info);
      return;
    }
    if(tileSet.tile_list.Count == 0)
    {
      EditorGUILayout.HelpBox("tileSet中没有tiles", MessageType.Info);
      return;
    }
    sharedTileSetData = GetSharedTileSetData(tileSet);
    

    DrawTileView();
    DrawTilePaletteSetting();
    DrawTilePalette();
    DisplayTileSetBrush();
  }

  

  private SharedTileSetData GetSharedTileSetData(TileSet tileSet)
  {
    SharedTileSetData sharedTileSetData = null;
    if (tileSet)
    {
      if (!sharedTileSetData_dict.TryGetValue(tileSet, out sharedTileSetData))
      {
        sharedTileSetData = new SharedTileSetData();
        sharedTileSetData.tileSet = tileSet;
        sharedTileSetData_dict[tileSet] = sharedTileSetData;
      }
    }
    return sharedTileSetData;
  }

  
  private void FocusSceneView()
  {
    if (SceneView.lastActiveSceneView != null)
      SceneView.lastActiveSceneView.Focus();
    else if (SceneView.sceneViews.Count > 0)
      ((SceneView)SceneView.sceneViews[0]).Focus();
  }

}
}
#endif