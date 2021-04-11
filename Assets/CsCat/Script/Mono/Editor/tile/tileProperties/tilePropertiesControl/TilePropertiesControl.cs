#if MicroTileMap
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
[Serializable]
public partial class TilePropertiesControl
{
  [SerializeField]
  private TilePropertiesEditMode edit_mode;

  public TileSet tileSet;
  private Vector2 scroll_pos = Vector2.zero;
  
  

  public void Display()
  {
    if (tileSet == null)
    {
      EditorGUILayout.HelpBox("没有选中tileSet", MessageType.Info);
      return;
    }

    if (tileSet.atlas_texture == null)
    {
      EditorGUILayout.HelpBox("tileSet中没有atlas texture", MessageType.Info);
      return;
    }

    if (tileSet.selected_tileId == TileSetConst.TileId_Empty && tileSet.tileSelection == null && tileSet.selected_tileSetBrushId == TileSetConst.TileSetBrushId_Default)
    {
      EditorGUILayout.HelpBox("没有选中的tile", MessageType.Info);
      return;
    }

    using (new EditorGUILayoutBeginVerticalScope())
    {
      using (new EditorGUILayoutBeginScrollViewScope(ref scroll_pos, GUILayout.Width(EditorGUIUtility.currentViewWidth)))
      {
        string[] edit_mode_names = Enum.GetNames(typeof(TilePropertiesEditMode));
        edit_mode = (TilePropertiesEditMode)GUILayout.Toolbar((int)edit_mode, edit_mode_names);
        switch (edit_mode)
        {
          case TilePropertiesEditMode.Collider: DisplayCollider(); break;
//          case TilePropertiesEditMode.Parameters: DisplayParameters(); break;
          case TilePropertiesEditMode.Prefab: DisplayPrefab(); break;
//          case TilePropertiesEditMode.Auto_Tile: DisplayAutoTile(); break;
        }
      }
    }

    if (GUI.changed)
      EditorUtility.SetDirty(tileSet);
  }
}
}
#endif