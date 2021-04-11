#if MicroTileMap
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TilePropertiesControl
{
  private void DisplayPrefab()
  {
    if (tileSet.selected_tileSetBrushId != TileSetConst.TileSetBrushId_Default)
      EditorGUILayout.LabelField("TileSetBrush tile不能被编辑", EditorStyles.boldLabel);
    else
    {
      bool is_multi_selection = tileSet.tileSelection != null;
      Tile selected_tile = is_multi_selection
        ? tileSet.tile_list[TileSetUtil.GetTileIdFromTileData(tileSet.tileSelection.selection_tileData_list[0])]
        : tileSet.selected_tile;
      GUILayoutUtility.GetRect(1, 1, GUILayout.Width(tileSet.visual_tile_size.x),
        GUILayout.Height(tileSet.visual_tile_size.y));//Pos1
      Rect tile_uv = selected_tile.uv;
      GUI.color = tileSet.background_color;
      GUI.DrawTextureWithTexCoords(GUILayoutUtility.GetLastRect(), EditorGUIUtility.whiteTexture, tile_uv, true);//即在Pos1处画图片
      GUI.color = Color.white;
      GUI.DrawTextureWithTexCoords(GUILayoutUtility.GetLastRect(), tileSet.atlas_texture, tile_uv, true);//即在Pos1处画图片

      if (is_multi_selection)
        EditorGUILayout.LabelField("* Multi-selection Edition", EditorStyles.boldLabel);

      using (var check1 = new EditorGUIBeginChangeCheckScope())
      {
        TilePrefabData tilePrefabData = selected_tile.tilePrefabData;
        float saved_label_width = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80;
        //offset
        tilePrefabData.offset = EditorGUILayout.Vector3Field("offset", tilePrefabData.offset);
        tilePrefabData.offsetMode =(TilePrefabDataOffsetMode) EditorGUILayout.EnumPopup("offset mode", tilePrefabData.offsetMode);
        //prefab
        bool is_prefab_changed = false;
        using (var check2 = new EditorGUIBeginChangeCheckScope())
        {
          tilePrefabData.prefab =
            (GameObject) EditorGUILayout.ObjectField("Prefab", tilePrefabData.prefab, typeof(GameObject), false);
          if (check2.IsChanged)
            is_prefab_changed = true;
        }

        //prefab preview
        Texture2D prefab_preview;
        using (new GUILayoutBeginHorizontalScope())
        {
          prefab_preview = AssetPreview.GetAssetPreview(selected_tile.tilePrefabData.prefab);
          GUILayout.Box(prefab_preview, prefab_preview != null ? (GUIStyle) "Box" : GUIStyle.none);
        }

        if (tilePrefabData.prefab)
        {
          EditorGUIUtility.labelWidth = 260;
          if (prefab_preview)
            tilePrefabData.is_show_prefab_preview_in_tile_palette = EditorGUILayout.Toggle("在tile palette中显示预设",
              tilePrefabData.is_show_prefab_preview_in_tile_palette);
          EditorGUIUtility.labelWidth = saved_label_width;
          tilePrefabData.is_show_tile_with_prefab =
            EditorGUILayout.Toggle("Show Tile With Prefab", tilePrefabData.is_show_tile_with_prefab);
        }

        if (check1.IsChanged)
        {
          Undo.RecordObject(tileSet, "Tile Prefab Data Changed");
          if (is_multi_selection)
          {
            for (int i = 0; i < tileSet.tileSelection.selection_tileData_list.Count; ++i)
            {
              Tile tile = tileSet.tile_list[TileSetUtil.GetTileIdFromTileData(tileSet.tileSelection.selection_tileData_list[i])];
              GameObject saved_prefab = tile.tilePrefabData.prefab;
              tile.tilePrefabData = tilePrefabData;
              if (!is_prefab_changed)
                tile.tilePrefabData.prefab = saved_prefab;
            }
          }
          else
            selected_tile.tilePrefabData = tilePrefabData;

          EditorUtility.SetDirty(tileSet);
        }
      }
    }
  }
}
}
#endif