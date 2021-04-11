using System.Collections;
using UnityEngine;

namespace CsCat
{
  public static partial class BoxColliderExtension
  {
    

    public static Hashtable GetSerializeHashtable(this BoxCollider self)
    {
      Hashtable hashtable = new Hashtable();
      hashtable["isTrigger"] = self.isTrigger;
      hashtable["center"] = self.center.ToStringOrDefault();
      hashtable["size"] = self.size.ToStringOrDefault(null, Vector3.one);
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this BoxCollider self, Hashtable hashtable)
    {
      self.isTrigger = hashtable.Get<bool>("isTrigger");
      self.center = hashtable.Get<string>("center").ToVector3OrDefault();
      self.size = hashtable.Get<string>("size").ToVector3OrDefault(null, Vector3.one);
    }


  }
}