using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CsCat
{
  public static partial class TilemapExtension
  {
#if UNITY_EDITOR
    public static Hashtable GetSerializeHashtable(this Tilemap self, Hashtable ref_id_hashtable = null)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["animationFrameRate"] = self.animationFrameRate;
      hashtable["color"] = self.color.ToHtmlStringRGBAOrDefault();
      hashtable["tileAnchor"] = self.tileAnchor.ToStringOrDefault(null, new Vector3(0.5f, 0.5f, 0));
      hashtable["orientation"] = (int)self.orientation;

      Hashtable tile_hashtable = new Hashtable();
      Vector3Int size = self.size;
      Vector3Int origin = self.origin;
      hashtable["size"] = size.ToStringOrDefault();
      hashtable["origin"] = origin.ToStringOrDefault();
      int check_count = size.x * size.y * size.z;
      for (int i = 0; i < check_count; i++)
      {
        int offset_z = i / (size.x * size.y);
        int offset_y = (i - offset_z * (size.x * size.y)) / size.x;
        int offset_x = i - offset_z * (size.x * size.y) - offset_y * size.x;

        Vector3Int current = origin + new Vector3Int(offset_x, offset_y, offset_z);
        if (self.HasTile(current))
        {
          Hashtable tile_detail_hashtable = new Hashtable();

          TileBase tileBase = self.GetTile(current);
          string assetPath = tileBase.GetAssetPath();
          string guid = AssetDatabase.AssetPathToGUID(assetPath);
          long ref_id = AssetPathRefManager.instance.GetRefIdByGuid(guid);
          tile_detail_hashtable["tileBase_ref_id"] = ref_id;
          if (ref_id_hashtable != null)
            ref_id_hashtable[ref_id] = true;

          TileFlags tileFlags = self.GetTileFlags(current);
          tile_detail_hashtable["tileFlags"] = (int)tileFlags;

          tile_detail_hashtable["transformMatrix"] =
            self.GetTransformMatrix(current).ToStringOrDefault(null, Matrix4x4.identity);

          tile_hashtable[current.ToString()] = tile_detail_hashtable;
        }
      }

      hashtable["tile_hashtable"] = tile_hashtable;
      hashtable.Trim();
      return hashtable;
    }
#endif

    public static void LoadSerializeHashtable(this Tilemap self, Hashtable hashtable, ResLoad resLoad)
    {
      self.animationFrameRate = hashtable.Get<float>("animationFrameRate");
      self.color = hashtable.Get<string>("color").ToColorOrDefault();
      self.tileAnchor = hashtable.Get<string>("tileAnchor").ToVector3OrDefault(null, new Vector3(0.5f, 0.5f, 0));
      self.orientation = hashtable.Get<int>("orientation").ToEnum<Tilemap.Orientation>();

      Vector3Int size = hashtable.Get<string>("size").ToVector3IntOrDefault();
      Vector3Int origin = hashtable.Get<string>("origin").ToVector3IntOrDefault();
      self.size = size;
      self.origin = origin;
      Hashtable tile_hashtable = hashtable.Get<Hashtable>("tile_hashtable");


      foreach (string cell_poistion_string in tile_hashtable.Keys)
      {
        Vector3Int cell_pos = cell_poistion_string.ToVector3().ToVector3Int();
        Hashtable tile_detail_hashtable = tile_hashtable.Get<Hashtable>(cell_poistion_string);
        long tileBase_ref_id = tile_detail_hashtable.Get<long>("tileBase_ref_id");
        string assetPath = tileBase_ref_id.GetAssetPathByRefId();
        resLoad.GetOrLoadAsset(assetPath, assetCat =>
        {
          TileBase tileBase = assetCat.Get<TileBase>();
          SetTile(self, cell_pos, tileBase, tile_detail_hashtable);
        }, null, null, self);
      }
    }
  }
}