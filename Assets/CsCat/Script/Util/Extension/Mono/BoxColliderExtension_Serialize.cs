using System.Collections;
using UnityEngine;

namespace CsCat
{
    public static partial class BoxColliderExtension
    {
        public static Hashtable GetSerializeHashtable(this BoxCollider self)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_isTrigger] = self.isTrigger,
                [StringConst.String_center] = self.center.ToStringOrDefault(),
                [StringConst.String_size] = self.size.ToStringOrDefault(null, Vector3.one)
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this BoxCollider self, Hashtable hashtable)
        {
            self.isTrigger = hashtable.Get<bool>(StringConst.String_isTrigger);
            self.center = hashtable.Get<string>(StringConst.String_center).ToVector3OrDefault();
            self.size = hashtable.Get<string>(StringConst.String_size).ToVector3OrDefault(null, Vector3.one);
        }
    }
}