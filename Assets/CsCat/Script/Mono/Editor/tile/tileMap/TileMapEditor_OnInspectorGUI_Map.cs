#if MicroTileMap
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TileMapEditor 
{
  private void OnInspectorGUI_Map()
  {
    EditorGUILayout.Space();

    if (GUILayout.Button("Refresh Map", GUILayout.MaxWidth(125)))
      tileMap.Refresh(true, true, true, true);
    if (GUILayout.Button("Clear Map", GUILayout.MaxWidth(125)))
    {
      if (EditorUtility.DisplayDialog("Warning!", "Are you sure you want to clear the map?\nThis action will remove all children objects under the tilemap", "Yes", "No"))
      {
        Undo.RegisterFullObjectHierarchyUndo(tileMap.gameObject, "Clear Map " + tileMap.name);
        tileMap.is_undo_enabled = true;
        tileMap.ClearMap();
        tileMap.is_undo_enabled = false;
      }
    }

    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
    {
      using (new EditorGUILayoutBeginHorizontalScope())
      {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cell_size"));
        if (GUILayout.Button("R", GUILayout.Width(20)))
          serializedObject.FindProperty("cell_size").vector2Value = tileMap.tileSet.tile_pixel_size / tileMap.tileSet.pixels_per_unit;
      }
      EditorGUILayout.PropertyField(serializedObject.FindProperty("is_show_grid"), new GUIContent("is_show_grid", "Show the tilemap grid."));
    }
    EditorGUILayout.Space();

    EditorGUILayout.LabelField(string.Format("Map Size ({0},{1})", tileMap.GridWidth , tileMap.GridHeight));

    //Display Map Bounds
    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
    {
      EditorGUILayout.LabelField("Map Bounds (in tiles):", EditorStyles.boldLabel);
      is_toggle_map_bounds_edit = EditorGUIUtil.ToggleIconButton("Edit Map Bounds", is_toggle_map_bounds_edit, EditorGUIUtility.IconContent("EditCollider"));

      float saved_label_width = EditorGUIUtility.labelWidth;
      EditorGUIUtility.labelWidth = 80;
      using (new EditorGUIIndentLevelScope(2))
      {
        using (var check = new EditorGUIBeginChangeCheckScope())
        {
          using (new EditorGUILayoutBeginHorizontalScope())
          {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("min_grid_x"), new GUIContent("Left"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("min_grid_y"), new GUIContent("Bottom"));
          }
          using (new EditorGUILayoutBeginHorizontalScope())
          {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("max_grid_x"), new GUIContent("Right"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("max_grid_y"), new GUIContent("Top"));
          }
          if (check.IsChanged)
          {
            serializedObject.ApplyModifiedProperties();
            tileMap.RecalculateMapBounds();
          }
        }
      }
      EditorGUIUtility.labelWidth = saved_label_width;
    }

    EditorGUILayout.Space();

    tileMap.is_allow_painting_out_of_bounds = EditorGUILayout.ToggleLeft("is_allow_painting_out_of_bounds", tileMap.is_allow_painting_out_of_bounds);

    EditorGUILayout.Space();

    if (GUILayout.Button("Shrink to Visible Area", GUILayout.MaxWidth(150)))
      tileMap.ShrinkMapBoundsToVisibleArea();
    EditorGUILayout.PropertyField(serializedObject.FindProperty("is_auto_shrink"));

    EditorGUILayout.Space();
    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
    {
      EditorGUILayout.LabelField("Advanced Options", EditorStyles.boldLabel);
      using (var check = new EditorGUIBeginChangeCheckScope())
      {
        bool is_tileMapChunks_visible = IsTileMapChunksVisible();
        is_tileMapChunks_visible = EditorGUILayout.Toggle(new GUIContent("is_tileMapChunks_visible", "Show tilemap chunk objects for debugging or other purposes. Hiding will be refreshed after collapsing the tilemap."), is_tileMapChunks_visible);
        if (check.IsChanged)
          SetTileMapChunkHideFlag(HideFlags.HideInHierarchy, !is_tileMapChunks_visible);
      }
      EditorGUILayout.PropertyField(serializedObject.FindProperty("is_enable_undo_while_painting"), new GUIContent("Enable Undo", "Disable Undo when painting on big maps to improve performance."));
    }
  }


  private bool IsTileMapChunksVisible()
  {
    var tileMapChunk = tileMap.GetComponentInChildren<TileMapChunk>();
    return tileMapChunk && (tileMapChunk.gameObject.hideFlags & HideFlags.HideInHierarchy) == 0;
  }


  private void SetTileMapChunkHideFlag(HideFlags flags, bool value)
  {
    TileMapChunk[] tileMapChunks = tileMap.GetComponentsInChildren<TileMapChunk>();
    foreach (TileMapChunk tileMapChunk in tileMapChunks)
    {
      if (value)
        tileMapChunk.gameObject.hideFlags |= flags;
      else
        tileMapChunk.gameObject.hideFlags &= ~flags;
    }
  }

}
}
#endif