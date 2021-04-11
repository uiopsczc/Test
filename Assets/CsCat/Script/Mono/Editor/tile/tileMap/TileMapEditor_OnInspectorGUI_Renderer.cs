#if MicroTileMap
using UnityEditor;
using UnityEngine;
namespace CsCat
{
public partial class TileMapEditor 
{
  private void OnInspectorGUI_Renderer()
  {
    using (var check = new EditorGUIBeginChangeCheckScope())
    {
      Material pre_material = tileMap.material;
      EditorGUILayout.PropertyField(serializedObject.FindProperty("_material"));
      if (check.IsChanged)
      {
        serializedObject.ApplyModifiedProperties();
        tileMap.Refresh();
        if (tileMap.material != pre_material && !AssetDatabase.Contains(pre_material))
          DestroyImmediate(pre_material);
      }
    }
    EditorGUILayout.PropertyField(serializedObject.FindProperty("_tintColor"));
    EditorGUILayout.PropertyField(serializedObject.FindProperty("parallax_factor"));

    //Pixel Snap
    if (tileMap.material.HasProperty("is_pixel_snap"))
    {
      using (var check = new EditorGUIBeginChangeCheckScope())
      {
        bool is_pixel_snap = EditorGUILayout.Toggle("is_pixel_snap", tileMap.is_pixel_snap);
        if(check.IsChanged)
          tileMap.is_pixel_snap = is_pixel_snap;
      }
    }

    // Sorting Layer and Order in layer            
    using (var check = new EditorGUIBeginChangeCheckScope())
    {
      EditorGUILayout.PropertyField(serializedObject.FindProperty("sortingLayer"));
      EditorGUILayout.PropertyField(serializedObject.FindProperty("orderInLayer"));
      serializedObject.FindProperty("orderInLayer").intValue = (serializedObject.FindProperty("orderInLayer").intValue << 16) >> 16; // convert from int32 to int16 keeping sign
      if (check.IsChanged)
      {
        serializedObject.ApplyModifiedProperties();
        tileMap.RefreshTileMapChunksSortingAttributes();
        SceneView.RepaintAll();
      }
    }         

    EditorGUILayout.PropertyField(serializedObject.FindProperty("inner_padding"), new GUIContent("Inner Padding", "The size, in pixels, the tile UV will be stretched. Use this to fix pixel precision artifacts when tiles have no padding border in the atlas."));

    tileMap.is_visible = EditorGUILayout.Toggle("Is Visible", tileMap.is_visible);
    EditorGUILayout.Space();
    //Render Properties
    using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
    {
      EditorGUILayout.LabelField("Render Properties", EditorStyles.boldLabel);

      SerializedProperty tileMapChunkRendererProperties = serializedObject.FindProperty("tileMapChunkRendererProperties");

      using (var check = new EditorGUIBeginChangeCheckScope())
      {
        EditorGUILayout.PropertyField(tileMapChunkRendererProperties.FindPropertyRelative("shadowCastingMode"));
        EditorGUILayout.PropertyField(tileMapChunkRendererProperties.FindPropertyRelative("is_receive_shadows"));
        EditorGUILayout.PropertyField(tileMapChunkRendererProperties.FindPropertyRelative("lightProbeUsage"));
        EditorGUILayout.PropertyField(tileMapChunkRendererProperties.FindPropertyRelative("reflectionProbeUsage"));
        if (tileMap.tileMapChunkRendererProperties.reflectionProbeUsage != UnityEngine.Rendering.ReflectionProbeUsage.Off)
        {
          using (new EditorGUIIndentLevelScope(2))
          {
            EditorGUILayout.PropertyField(tileMapChunkRendererProperties.FindPropertyRelative("probeAnchor"));
          }
        }

        if (check.IsChanged)
        {
          serializedObject.ApplyModifiedProperties();
          tileMap.UpdateTileMapChunkRenderereProperties();
        }
      }
    }
  }

}
}
#endif