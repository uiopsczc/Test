using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace CsCat
{
    public static class ICollectionTExtension
    {
        #region 各种To ToXXX

        public static T[] ToArray<T>(this ICollection<T> self)
        {
            var result = new T[self.Count];
            self.CopyTo(result, 0);
            return result;
        }

        #endregion
    }
}