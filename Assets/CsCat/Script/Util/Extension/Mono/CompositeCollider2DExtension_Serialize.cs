using System.Collections;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
  public static partial class CompositeCollider2DExtension
  {
    public static Hashtable GetSerializeHashtable(this CompositeCollider2D self)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["isTrigger"] = self.isTrigger;
      hashtable["usedByEffector"] = self.usedByEffector;
      hashtable["offset"] = self.offset.ToStringOrDefault();
      hashtable["geometryType"] = (int)self.geometryType;
      hashtable["generationType"] = (int)self.generationType;
      hashtable["vertexDistance"] = self.vertexDistance;
      hashtable["offsetDistance"] = self.offsetDistance;
      hashtable["edgeRadius"] = self.edgeRadius;
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this CompositeCollider2D self, Hashtable hashtable)
    {
      self.isTrigger = hashtable.Get<bool>("isTrigger");
      self.usedByEffector = hashtable.Get<bool>("usedByEffector");
      self.offset = hashtable.Get<string>("offset").ToVector2OrDefault();
      self.geometryType = hashtable.Get<int>("geometryType").ToEnum<CompositeCollider2D.GeometryType>();
      self.generationType = hashtable.Get<int>("generationType").ToEnum<CompositeCollider2D.GenerationType>();
      self.vertexDistance = hashtable.Get<float>("vertexDistance");
      self.offsetDistance = hashtable.Get<float>("offsetDistance");
      self.edgeRadius = hashtable.Get<float>("edgeRadius");
    }
  }
}