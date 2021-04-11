#if MicroTileMap
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
[CustomEditor(typeof(TileSet))]
public partial class TileSetEditor : Editor
{
  private new TileSet target
  {
    get { return base.target as TileSet; }
  }

  private static TileSetEditorMode mode = TileSetEditorMode.Tiles;
  private TileSetControl tileSetControl = new TileSetControl();

  public override void OnInspectorGUI()
  {
    serializedObject.Update();


    DrawTextureSetting();

    EditorGUILayout.Separator();
    //      if (GUILayout.Button("Import TMX"))
    //        TmxImporter.ImportTmxIntoTheScene(target);
    EditorGUILayout.Separator();
    string[] mode_names = Enum.GetNames(typeof(TileSetEditorMode));
    mode = (TileSetEditorMode) GUILayout.Toolbar((int) mode, mode_names);
    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox,
      GUILayout.MinHeight(mode == TileSetEditorMode.Tiles ? Screen.height * 0.8f : 0f)))
    {
      switch (mode)
      {
        case TileSetEditorMode.Tiles:
          tileSetControl.tileSet = target;
          tileSetControl.Display();
          Repaint();
          break;
//          case TileSetEditorMode.BrushGroups:
//            s_brushGroupsFoldout = EditorGUILayout.Foldout(s_brushGroupsFoldout, "Groups");
//            if (s_brushGroupsFoldout)
//            {
//              m_groupsList.DoLayoutList();
//            }
//            GroupMatrixGUI.DoGUI("Group Autotiling Mask", tileset.BrushGroupNames, ref s_brushAutotilingMaskFoldout, ref s_brushGroupMatrixScrollPos, GetAutotiling, SetAutotiling);
//            EditorGUILayout.HelpBox("Check the group that should autotile between them when Autotiling Mode Group is enabled in a brush", MessageType.Info);
//            break;
      }
    }


    if (GUI.changed)
    {
      serializedObject.ApplyModifiedProperties();
      EditorUtility.SetDirty(target);
    }
  }


  //在指定的dst_rect区域画tile
  public static void DoGUIDrawTileFromTileData(Rect dst_rect, uint tileData, TileSet tileSet,
    Rect custom_uv = default(Rect))
  {
    int tileId = (int) (tileData & TileSetConst.TileDataMask_TileId);
    Tile tile = tileSet.GetTile(tileId);
    if (tileId != TileSetConst.TileId_Empty && tileSet.atlas_texture)
    {
      if (TileSetUtil.IsTileFlagFlipV(tileData))
        GUIUtility.ScaleAroundPivot(new Vector2(1f, -1f), dst_rect.center);
      if (TileSetUtil.IsTileFlagFlipH(tileData))
        GUIUtility.ScaleAroundPivot(new Vector2(-1f, 1f), dst_rect.center);
      if (TileSetUtil.IsTileFlagRot90(tileData))
        GUIUtility.RotateAroundPivot(90f, dst_rect.center);
      if (tile != null && tile.tilePrefabData.prefab && tile.tilePrefabData.is_show_prefab_preview_in_tile_palette)
      {
        Texture2D asset_preview = AssetPreview.GetAssetPreview(tile.tilePrefabData.prefab);
        if (asset_preview)
          GUI.DrawTexture(dst_rect, asset_preview, ScaleMode.ScaleToFit);
        else
          GUI.DrawTextureWithTexCoords(dst_rect, tileSet.atlas_texture,
            custom_uv == default(Rect) && tile != null ? tile.uv : custom_uv, true);
      }
      else
      {
        GUI.DrawTextureWithTexCoords(dst_rect, tileSet.atlas_texture,
          custom_uv == default(Rect) && tile != null ? tile.uv : custom_uv, true);
      }

      GUI.matrix = Matrix4x4.identity;
    }
  }


  public static TileSet GetSelectedTileSet()
  {
    if (Selection.activeObject is TileSet)
    {
      return Selection.activeObject as TileSet;
    }
    else if (Selection.activeObject is TileSetBrush)
    {
      return (Selection.activeObject as TileSetBrush).tileSet;
    }
    else if (Selection.activeObject is GameObject)
    {
      TileMap tileMap = (Selection.activeObject as GameObject).GetComponent<TileMap>();
      if (tileMap == null)
      {
        var tileMapGroup = (Selection.activeObject as GameObject).GetComponent<TileMapGroup>();
        if (tileMapGroup != null)
          tileMap = tileMapGroup.selected_tileMap;
      }

      if (tileMap != null)
        return tileMap.tileSet;
    }

    return null;
  }


  public static int DoTileSetBrushGroupFieldLayout(TileSet tileSet, string label, int group_index)
  {
    string tileSetBrush_group_names = tileSet.tileSetBrush_group_names[group_index];
    string[] tileSetBrushGroup_list = tileSet.tileSetBrush_group_names.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    using (var check = new EditorGUIBeginChangeCheckScope())
    {
      int index = EditorGUILayout.Popup(label,
        ArrayUtility.FindIndex(tileSetBrushGroup_list, x => x == tileSetBrush_group_names), tileSetBrushGroup_list);
      if (check.IsChanged)
        return ArrayUtility.FindIndex(tileSet.tileSetBrush_group_names, x => x == tileSetBrushGroup_list[index]);
    }

    return group_index;
  }



  
}
}
#endif