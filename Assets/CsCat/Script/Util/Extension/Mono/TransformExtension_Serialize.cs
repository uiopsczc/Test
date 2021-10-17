using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
    public static partial class TransformExtension
    {
        public static Hashtable GetSerializeHashtable(this Transform self)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_localPosition] = self.localPosition.ToStringOrDefault(),
                [StringConst.String_localEulerAngles] = self.localEulerAngles.ToStringOrDefault(),
                [StringConst.String_localScale] = self.localScale.ToStringOrDefault(null, Vector3.one)
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this Transform self, Hashtable hashtable)
        {
            self.localPosition = hashtable.Get<string>(StringConst.String_localPosition).ToVector3OrDefault();
            self.localEulerAngles = hashtable.Get<string>(StringConst.String_localEulerAngles).ToVector3OrDefault();
            self.localScale = hashtable.Get<string>(StringConst.String_localScale)
                .ToVector3OrDefault(null, Vector3.one);
        }
    }
}