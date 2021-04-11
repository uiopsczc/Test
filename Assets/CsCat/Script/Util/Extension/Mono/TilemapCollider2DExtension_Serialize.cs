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
      Hashtable hashtable = new Hashtable();
      hashtable["maximumTileChangeCount"] = self.maximumTileChangeCount;
      hashtable["extrusionFactor"] = self.extrusionFactor;
      hashtable["isTrigger"] = self.isTrigger;
      hashtable["usedByEffector"] = self.usedByEffector;
      hashtable["usedByComposite"] = self.usedByComposite;
      hashtable["offset"] = self.offset.ToStringOrDefault();
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this TilemapCollider2D self, Hashtable hashtble)
    {
      self.maximumTileChangeCount = hashtble.Get<uint>("maximumTileChangeCount");
      self.extrusionFactor = hashtble.Get<float>("extrusionFactor");
      self.isTrigger = hashtble.Get<bool>("isTrigger");
      self.usedByEffector = hashtble.Get<bool>("usedByEffector");
      self.usedByComposite = hashtble.Get<bool>("usedByComposite");
      self.offset = hashtble.Get<string>("offset").ToVector2OrDefault();
    }
  }
}