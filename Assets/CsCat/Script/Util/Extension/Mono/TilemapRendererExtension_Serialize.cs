using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CsCat
{
    public static partial class TilemapRendererExtension
    {
        public static Hashtable GetSerializeHashtable(this TilemapRenderer self)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_mode] = (int) self.mode,
                [StringConst.String_detectChunkCullingBounds] = (int) self.detectChunkCullingBounds,
                [StringConst.String_sortOrder] = (int) self.sortOrder,
                [StringConst.String_sortingOrder] = self.sortingOrder,
                [StringConst.String_maskInteraction] = (int) self.maskInteraction
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this TilemapRenderer self, Hashtable hashtable)
        {
            self.mode = hashtable.Get<int>(StringConst.String_mode).ToEnum<TilemapRenderer.Mode>();
            self.detectChunkCullingBounds =
                hashtable.Get<int>(StringConst.String_detectChunkCullingBounds)
                    .ToEnum<TilemapRenderer.DetectChunkCullingBounds>();
            self.sortOrder = hashtable.Get<int>(StringConst.String_sortOrder).ToEnum<TilemapRenderer.SortOrder>();
            self.sortingOrder = hashtable.Get<int>(StringConst.String_sortingOrder);
            self.maskInteraction =
                hashtable.Get<int>(StringConst.String_maskInteraction).ToEnum<SpriteMaskInteraction>();
        }
    }
}