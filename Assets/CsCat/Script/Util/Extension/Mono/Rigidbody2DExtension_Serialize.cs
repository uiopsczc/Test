using System.Collections;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
  public static partial class Rigidbody2DExtension
  {
    public static Hashtable GetSerializeHashtable(this Rigidbody2D self)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["bodyType"] = (int)self.bodyType;
      hashtable["simulated"] = self.simulated;
      hashtable["useFullKinematicContacts"] = self.useFullKinematicContacts;
      hashtable["collisionDetectionMode"] = (int)self.collisionDetectionMode;
      hashtable["sleepMode"] = (int)self.sleepMode;
      hashtable["interpolation"] = (int)self.interpolation;
      hashtable["constraints"] = (int)self.constraints;
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this Rigidbody2D self, Hashtable hashtable)
    {
      self.bodyType = hashtable.Get<int>("bodyType").ToEnum<RigidbodyType2D>();
      self.simulated = hashtable.Get<bool>("bodyType");
      self.useFullKinematicContacts = hashtable.Get<bool>("useFullKinematicContacts");
      self.collisionDetectionMode = hashtable.Get<int>("collisionDetectionMode").ToEnum<CollisionDetectionMode2D>();
      self.sleepMode = hashtable.Get<int>("sleepMode").ToEnum<RigidbodySleepMode2D>();
      self.interpolation = hashtable.Get<int>("interpolation").ToEnum<RigidbodyInterpolation2D>();
      self.constraints = hashtable.Get<int>("constraints").ToEnum<RigidbodyConstraints2D>();
    }
  }
}