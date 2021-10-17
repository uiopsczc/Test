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
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_bodyType] = (int) self.bodyType,
                [StringConst.String_simulated] = self.simulated,
                [StringConst.String_useFullKinematicContacts] = self.useFullKinematicContacts,
                [StringConst.String_collisionDetectionMode] = (int) self.collisionDetectionMode,
                [StringConst.String_sleepMode] = (int) self.sleepMode,
                [StringConst.String_interpolation] = (int) self.interpolation,
                [StringConst.String_constraints] = (int) self.constraints
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this Rigidbody2D self, Hashtable hashtable)
        {
            self.bodyType = hashtable.Get<int>(StringConst.String_bodyType).ToEnum<RigidbodyType2D>();
            self.simulated = hashtable.Get<bool>(StringConst.String_bodyType);
            self.useFullKinematicContacts = hashtable.Get<bool>(StringConst.String_useFullKinematicContacts);
            self.collisionDetectionMode =
                hashtable.Get<int>(StringConst.String_collisionDetectionMode).ToEnum<CollisionDetectionMode2D>();
            self.sleepMode = hashtable.Get<int>(StringConst.String_sleepMode).ToEnum<RigidbodySleepMode2D>();
            self.interpolation =
                hashtable.Get<int>(StringConst.String_interpolation).ToEnum<RigidbodyInterpolation2D>();
            self.constraints = hashtable.Get<int>(StringConst.String_constraints).ToEnum<RigidbodyConstraints2D>();
        }
    }
}