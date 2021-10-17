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
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_animationFrameRate] = self.animationFrameRate,
                [StringConst.String_color] = self.color.ToHtmlStringRGBAOrDefault(),
                [StringConst.String_tileAnchor] = self.tileAnchor.ToStringOrDefault(null, new Vector3(0.5f, 0.5f, 0)),
                [StringConst.String_orientation] = (int) self.orientation
            };

            Hashtable tileHashtable = new Hashtable();
            Vector3Int size = self.size;
            Vector3Int origin = self.origin;
            hashtable[StringConst.String_size] = size.ToStringOrDefault();
            hashtable[StringConst.String_origin] = origin.ToStringOrDefault();
            int checkCount = size.x * size.y * size.z;
            for (int i = 0; i < checkCount; i++)
            {
                int offsetZ = i / (size.x * size.y);
                int offsetY = (i - offsetZ * (size.x * size.y)) / size.x;
                int offsetX = i - offsetZ * (size.x * size.y) - offsetY * size.x;

                Vector3Int current = origin + new Vector3Int(offsetX, offsetY, offsetZ);
                if (self.HasTile(current))
                {
                    Hashtable tile_detail_hashtable = new Hashtable();

                    TileBase tileBase = self.GetTile(current);
                    string assetPath = tileBase.GetAssetPath();
                    string guid = AssetDatabase.AssetPathToGUID(assetPath);
                    long refId = AssetPathRefManager.instance.GetRefIdByGuid(guid);
                    tile_detail_hashtable[StringConst.String_tileBase_ref_id] = refId;
                    if (ref_id_hashtable != null)
                        ref_id_hashtable[refId] = true;

                    TileFlags tileFlags = self.GetTileFlags(current);
                    tile_detail_hashtable[StringConst.String_tileFlags] = (int) tileFlags;

                    tile_detail_hashtable[StringConst.String_transformMatrix] =
                        self.GetTransformMatrix(current).ToStringOrDefault(null, Matrix4x4.identity);

                    tileHashtable[current.ToString()] = tile_detail_hashtable;
                }
            }

            hashtable[StringConst.String_tile_hashtable] = tileHashtable;
            hashtable.Trim();
            return hashtable;
        }
#endif

        public static void LoadSerializeHashtable(this Tilemap self, Hashtable hashtable, ResLoad resLoad)
        {
            self.animationFrameRate = hashtable.Get<float>(StringConst.String_animationFrameRate);
            self.color = hashtable.Get<string>(StringConst.String_color).ToColorOrDefault();
            self.tileAnchor = hashtable.Get<string>(StringConst.String_tileAnchor).ToVector3OrDefault(null, new Vector3(0.5f, 0.5f, 0));
            self.orientation = hashtable.Get<int>(StringConst.String_orientation).ToEnum<Tilemap.Orientation>();

            Vector3Int size = hashtable.Get<string>(StringConst.String_size).ToVector3IntOrDefault();
            Vector3Int origin = hashtable.Get<string>(StringConst.String_origin).ToVector3IntOrDefault();
            self.size = size;
            self.origin = origin;
            Hashtable tileHashtable = hashtable.Get<Hashtable>(StringConst.String_tile_hashtable);


            foreach (string cellPositionString in tileHashtable.Keys)
            {
                Vector3Int cellPos = cellPositionString.ToVector3().ToVector3Int();
                Hashtable tileDetailHashtable = tileHashtable.Get<Hashtable>(cellPositionString);
                long tileBaseRefId = tileDetailHashtable.Get<long>(StringConst.String_tileBase_ref_id);
                string assetPath = tileBaseRefId.GetAssetPathByRefId();
                resLoad.GetOrLoadAsset(assetPath, assetCat =>
                {
                    TileBase tileBase = assetCat.Get<TileBase>();
                    SetTile(self, cellPos, tileBase, tileDetailHashtable);
                }, null, null, self);
            }
        }
    }
}