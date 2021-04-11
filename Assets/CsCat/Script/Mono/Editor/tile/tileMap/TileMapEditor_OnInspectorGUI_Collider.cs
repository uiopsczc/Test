#if MicroTileMap
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public partial class TileMapEditor 
{
  private void OnInspectorGUI_Collider()
  {
    using(var check  = new EditorGUIBeginChangeCheckScope())
    {
      EditorGUILayout.Space();

      using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
      {
        EditorGUILayout.LabelField("tileMapColliderType:", EditorStyles.boldLabel);
        using (new EditorGUIIndentLevelScope(2))
        {
          SerializedProperty tileMapColliderType_property = serializedObject.FindProperty("tileMapColliderType");
          string[] tileMapColliderType_names = new List<string>(Enum.GetNames(typeof(TileMapColliderType)).Select(x => x.Replace('_', ' '))).ToArray();
          tileMapColliderType_property.intValue = GUILayout.Toolbar(tileMapColliderType_property.intValue, tileMapColliderType_names);
        }
        EditorGUILayout.Space();
      }

      EditorGUILayout.Space();
      if (tileMap.tileMapColliderType == TileMapColliderType._3D)
      {
        SerializedProperty collider_depth_property = serializedObject.FindProperty("collider_depth");
        EditorGUILayout.PropertyField(collider_depth_property);
        collider_depth_property.floatValue = Mathf.Clamp(collider_depth_property.floatValue, Vector3.kEpsilon, Mathf.Max(collider_depth_property.floatValue));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicMaterial"));
      }
      else if (tileMap.tileMapColliderType == TileMapColliderType._2D)
      {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileMap2DColliderType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("is_show_collider_normals"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("physicsMaterial2D"));
      }

      EditorGUILayout.PropertyField(serializedObject.FindProperty("is_trigger"));
      EditorGUILayout.PropertyField(serializedObject.FindProperty("tileMapColliderDisplayMode"));

      if (tileMap.is_trigger)
        EditorGUILayout.HelpBox("Activating IsTrigger could generate wrong collider lines if not used properly. Use Show Tilechunks in Map section to display the real collider lines.", MessageType.Warning);
      else if (tileMap.tileMapColliderType == TileMapColliderType._2D && tileMap.tileMap2DColliderType == TileMap2DColliderType.PolygonCollider2D)
        EditorGUILayout.HelpBox("Using Polygon colliders could generate wrong collider lines if not used properly. Use Show Tilechunks in Map section to display the real collider lines.", MessageType.Warning);

      if (check.IsChanged)
      {
        serializedObject.ApplyModifiedProperties();
        tileMap.Refresh(false, true);
      }
    }

    EditorGUILayout.Space();

    if (GUILayout.Button("Update Collider Mesh"))
      tileMap.Refresh(false, true);
  }

}
}
#endif