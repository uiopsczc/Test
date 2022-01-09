#if MicroTileMap
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
  public partial class TileSet
  {
    public int column_tile_count_in_palette = 8;
    public Vector2 visual_tile_size = new Vector2(32, 32);
    public Color background_color = new Color32(205, 205, 205, 205);

    public Vector2 CalculateTileTexelSize()
    {
      //atlas_texture.texelSize相当于shader里面的_MainTex_TexelSize,转为像uv那样限制范围[0,1]
      return atlas_texture != null ? Vector2.Scale(atlas_texture.texelSize, tile_pixel_size) : Vector2.zero;
    }

    public void Slice()
    {
      List<Tile> _tile_list = new List<Tile>();
      if (atlas_texture != null)
      {
        Vector2 tile_texsel_size = CalculateTileTexelSize();
        int u_increase = Mathf.RoundToInt(tile_pixel_size.x + slice_padding.x);
        int v_increase = Mathf.RoundToInt(tile_pixel_size.y + slice_padding.y);
        _row_tile_count = 0;
        if (u_increase > 0 && v_increase > 0)
        {
          for (int v = Mathf.RoundToInt(slice_offset.y);
            v + tile_pixel_size.y <= atlas_texture.height;
            v += v_increase, ++_row_tile_count)
          {
            for (int u = Mathf.RoundToInt(slice_offset.x);
              u + tile_pixel_size.x <= atlas_texture.width;
              u += u_increase)
              _tile_list.Add(new Tile()
              {
                uv = new Rect(
                  new Vector2((float) u / atlas_texture.width,
                    (float) (atlas_texture.height - v - tile_pixel_size.y) / atlas_texture.height), tile_texsel_size)
              });
          }

          _column_tile_count = _tile_list.Count / _row_tile_count;
          //Copy data from previous tiles
#if UNITY_EDITOR
          if (!tile_list.IsNullOrEmpty() && EditorUtility.DisplayDialog("保留之前的tile的属性?", "保留之前的tile的属性?", "Yes", "No"))
#endif
          {
            for (int i = 0; i < tile_list.Count && i < _tile_list.Count; ++i)
            {
              _tile_list[i].tileColliderData = tile_list[i].tileColliderData;
              _tile_list[i].argContainer = tile_list[i].argContainer;
              _tile_list[i].tilePrefabData = tile_list[i].tilePrefabData;
            }
          }

          tile_list = _tile_list;
        }
        else
          LogCat.error(string.Format(" Error while slicing. u_increase = {0},u_increase = {1}", u_increase,
            v_increase));
      }
    }



#if UNITY_EDITOR
    public void UpdateTileSetConfig()
    {
      if (atlas_texture != null)
      {
        string asset_path = AssetDatabase.GetAssetPath(atlas_texture);
        if (!asset_path.IsNullOrEmpty())
        {
          TextureImporter textureImporter = AssetImporter.GetAtPath(asset_path) as TextureImporter;
          if (textureImporter != null)
          {
            pixels_per_unit = textureImporter.spritePixelsPerUnit;
            if (textureImporter.textureType == TextureImporterType.Sprite)
            {
              if (textureImporter.spriteImportMode == SpriteImportMode.Multiple)
              {
                List<Tile> _tile_list = new List<Tile>();
                if (textureImporter.spritesheet.Length >= 2) //每个tile都有spritesheet
                {
                  SpriteMetaData left_top_first_tile_spritesheet = textureImporter.spritesheet[0]; //左上角第一个的tile
                  SpriteMetaData left_top_second_tile_spritesheet = textureImporter.spritesheet[1]; //左上角第二个的tile
                  tile_pixel_size = left_top_first_tile_spritesheet.rect.size;
                  slice_offset = left_top_first_tile_spritesheet.rect.position;
                  slice_offset.y = atlas_texture.height - left_top_first_tile_spritesheet.rect.y -
                                   left_top_first_tile_spritesheet.rect.height;
                  slice_padding.x = left_top_second_tile_spritesheet.rect.x - left_top_first_tile_spritesheet.rect.xMax;

                }

                if (textureImporter.spritesheet.Length >= 2)
                {
                  if (tile_list.Count == 0)
                  {
                    _row_tile_count = 0;
                    foreach (SpriteMetaData spriteData in textureImporter.spritesheet)
                    {
                      Rect rect_uv = new Rect(Vector2.Scale(spriteData.rect.position, atlas_texture.texelSize),
                        Vector2.Scale(spriteData.rect.size,
                          atlas_texture
                            .texelSize)); // atlas_texture.texelSize相当于shader里面的_MainTex_TexelSize,转为像uv那样限制范围[0,1]
                      _tile_list.Add(new Tile() {uv = rect_uv});
                      if (_tile_list.Count >= 2)
                      {
                        if (_tile_list[_tile_list.Count - 2].uv.y != _tile_list[_tile_list.Count - 1].uv.y) //不同的一行
                        {
                          if (_row_tile_count == 1)
                          {
                            _column_tile_count = _tile_list.Count - 1; //计算列数
                            slice_padding.y = textureImporter.spritesheet[_tile_list.Count - 2].rect.y -
                                              textureImporter.spritesheet[_tile_list.Count - 1].rect.yMax;
                          }

                          _row_tile_count++; //计算行数
                        }
                      }
                      else
                        _row_tile_count++; //计算行数
                    }
                  }
                }

                if (!tile_list.IsNullOrEmpty())
                {
                  for (int i = 0; i < tile_list.Count && i < _tile_list.Count; i++)
                  {
                    var _tile = _tile_list[i]; //现在的tile
                    var pre_tile = tile_list[i]; //之前的tile
                    _tile.tileColliderData = pre_tile.tileColliderData;
                    _tile.argContainer = pre_tile.argContainer;
                    _tile.tilePrefabData = pre_tile.tilePrefabData;
                    _tile.auto_tile_group = pre_tile.auto_tile_group;
                  }
                }

                tile_list = _tile_list;
              }
            }
          }
        }
      }
    }
#endif
  }
}
#endif