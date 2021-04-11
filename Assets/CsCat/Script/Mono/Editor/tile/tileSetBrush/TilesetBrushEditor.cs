#if MicroTileMap
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
[CanEditMultipleObjects]
[CustomEditor(typeof(TileSetBrush))]
public class TileSetBrushEditor : Editor
{
  SerializedProperty tileSet;
  SerializedProperty auto_tile_mode;
  SerializedProperty group;
  SerializedProperty is_show_in_palette;

  public virtual void OnEnable()
  {
    tileSet = serializedObject.FindProperty("tileSet");
    auto_tile_mode = serializedObject.FindProperty("auto_tile_mode");
    group = serializedObject.FindProperty("group");
    is_show_in_palette = serializedObject.FindProperty("is_show_in_palette");
  }

  public override void OnInspectorGUI()
  {
    DoInspectorGUI();
  }

  public void DoInspectorGUI()
  {
    serializedObject.Update();
    TileSetBrush tileSetBrush = (TileSetBrush)target;
    if (tileSetBrush.tileSet == null)
    {
      EditorGUILayout.HelpBox("先选择一个tileset", MessageType.Info);
      EditorGUILayout.PropertyField(tileSet);
      serializedObject.ApplyModifiedProperties();
      return;
    }

    EditorGUILayout.PropertyField(tileSet);
    EditorGUILayout.PropertyField(is_show_in_palette);
    group.intValue = TileSetEditor.DoTileSetBrushGroupFieldLayout(tileSetBrush.tileSet, "Group", group.intValue);
    auto_tile_mode.intValue = Convert.ToInt32(EditorGUILayout.EnumMaskField(new GUIContent("AutoTile Mode"), tileSetBrush.auto_tile_mode));
    string auto_tile_mode_tip =
      "auto_tile_mode:\n" +
      "Self: autotile only with tileSetBrushes of same type\n" +
      "Other: autotile with any other not empty tile\n" +
      "Group: autotile with tileSetBrushes of a group that autotile the brush group";
    EditorGUILayout.HelpBox(auto_tile_mode_tip, MessageType.Info);
    if (GUI.changed)
    {
      serializedObject.ApplyModifiedProperties();
      EditorUtility.SetDirty(target);
    }
  }
}
}
#endif