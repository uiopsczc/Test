using System.Collections;
#if UNITY_EDITOR
#endif
using UnityEngine.Tilemaps;

namespace CsCat
{
    public static partial class TilemapCollider2DExtension
    {
        public static Hashtable GetSerializeHashtable(this TilemapCollider2D self)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_maximumTileChangeCount] = self.maximumTileChangeCount,
                [StringConst.String_extrusionFactor] = self.extrusionFactor,
                [StringConst.String_isTrigger] = self.isTrigger,
                [StringConst.String_usedByEffector] = self.usedByEffector,
                [StringConst.String_usedByComposite] = self.usedByComposite,
                [StringConst.String_offset] = self.offset.ToStringOrDefault()
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this TilemapCollider2D self, Hashtable hashtble)
        {
            self.maximumTileChangeCount = hashtble.Get<uint>(StringConst.String_maximumTileChangeCount);
            self.extrusionFactor = hashtble.Get<float>(StringConst.String_extrusionFactor);
            self.isTrigger = hashtble.Get<bool>(StringConst.String_isTrigger);
            self.usedByEffector = hashtble.Get<bool>(StringConst.String_usedByEffector);
            self.usedByComposite = hashtble.Get<bool>(StringConst.String_usedByComposite);
            self.offset = hashtble.Get<string>(StringConst.String_offset).ToVector2OrDefault();
        }
    }
}