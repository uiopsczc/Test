#if MicroTileMap
using System;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public partial class TileMapEditor 
{
  public override void OnInspectorGUI()
  {
    Event e = Event.current;
    if (e.type == EventType.ValidateCommand && e.commandName == "UndoRedoPerformed")
      tileMap.Refresh();
    serializedObject.Update();

    TileSet prev_tileSet = tileMap.tileSet;
    using (new GUIBackGroundColorScope(Color.yellow))
    {
      using (new EditorGUILayoutBeginVerticalScope(GUIStyleConst.Box_Style))
      {
        tileMap.tileSet = (TileSet)EditorGUILayout.ObjectField("TileSet", tileMap.tileSet, typeof(TileSet), false);
      }
    }

    if (prev_tileSet != tileMap.tileSet)
    {
//      UnregisterTileSetEvents(prev_tileSet);
//      RegisterTileSetEvents(tileMap.tileSet);
      tileMap_tileSet = tileMap.tileSet;
    }

    if (tileMap.tileSet == null)
    {
      EditorGUILayout.HelpBox("ÇëÑ¡ÔñÒ»¸ötileSet", MessageType.Info);
      return;
    }
    string[] edit_mode_names = Enum.GetNames(typeof(TileMapEditorEditMode));
    edit_mode = (TileMapEditorEditMode)GUILayout.Toolbar((int)edit_mode, edit_mode_names);
    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox,
      GUILayout.MinHeight(edit_mode == TileMapEditorEditMode.Paint ? Screen.height * 0.8f : 0f)))
    {
      if (edit_mode == TileMapEditorEditMode.Renderer)
        OnInspectorGUI_Renderer();
      else if (edit_mode == TileMapEditorEditMode.Map)
        OnInspectorGUI_Map();
      else if (edit_mode == TileMapEditorEditMode.Collider)
        OnInspectorGUI_Collider();
      else if (edit_mode == TileMapEditorEditMode.Paint)
      {
        if (tileMap.tileSet != null)
        {
          if (tileSetControl == null)
            tileSetControl = new TileSetControl();
          tileSetControl.tileSet = tileMap.tileSet;
          tileSetControl.Display();
        }
      }

      Repaint();
      serializedObject.ApplyModifiedProperties();
      if (GUI.changed)
        EditorUtility.SetDirty(target);
    }
  }
}
}
#endif