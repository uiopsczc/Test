#if MicroTileMap
using UnityEditor;
using UnityEngine;

namespace CsCat
{
public partial class TileSetEditor
{
  public void DrawTextureSetting()
  {
    using (var change_scope = new EditorGUIBeginChangeCheckScope())
    {
      EditorGUILayout.PropertyField(serializedObject.FindProperty("atlas_texture"));
      if (change_scope.IsChanged)
      {
        serializedObject.ApplyModifiedProperties();
        target.UpdateTileSetConfig();
      }
    }

    if (target.atlas_texture == null)
      EditorGUILayout.HelpBox("为tileset选择一张texture", MessageType.Info);
    else
    {
      EditorGUILayout.PropertyField(serializedObject.FindProperty("tile_pixel_size"));
      if (GUILayout.Button(new GUIContent("优化TextureImport Settings")))
        OptimizeTextureImportSettings(target.atlas_texture);
      using (new EditorGUILayoutBeginVerticalScope(EditorStyles.helpBox))
      {
        target.tile_pixel_size =
          _GetPositiveIntVector2(EditorGUILayout.Vector2Field("Tile Size (pixels)", target.tile_pixel_size),
            Vector2.one);
        target.slice_offset =
          _GetPositiveIntVector2(EditorGUILayout.Vector2Field(new GUIContent("Offset"), target.slice_offset));
        target.slice_padding =
          _GetPositiveIntVector2(EditorGUILayout.Vector2Field(new GUIContent("Padding"), target.slice_padding));
        if (GUILayout.Button("Slice Atlas"))
          target.Slice();
      }
    }
  }

  private Vector2 _GetPositiveIntVector2(Vector2 v, Vector2 min_value = default(Vector2))
  {
    return new Vector2(Mathf.Max(min_value.x, (int)v.x), Mathf.Max(min_value.y, (int)v.y));
  }

  public static void OptimizeTextureImportSettings(Texture2D texture2D)
  {
    if (texture2D != null)
    {
      string asset_path = AssetDatabase.GetAssetPath(texture2D);
      if (!string.IsNullOrEmpty(asset_path))
      {
        TextureImporter textureImporter = AssetImporter.GetAtPath(asset_path) as TextureImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        if (textureImporter.spriteImportMode == SpriteImportMode.None)
          textureImporter.spriteImportMode = SpriteImportMode.Single;
        textureImporter.mipmapEnabled = false;
        textureImporter.filterMode = FilterMode.Point;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.FixTextureSize(texture2D);
        AssetDatabase.ImportAsset(asset_path);
      }
    }
  }
}
}
#endif