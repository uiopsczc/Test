using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;

#endif

namespace CsCat
{
    public static class IListExtension
    {
        public static bool ContainsIndex(this IList self, int index)
        {
            return index < self.Count && index >= 0;
        }

        /// <summary>
        ///   变为对应的ArrayList
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(this IList self)
        {
            var list = new ArrayList();
            foreach (var o in self)
                list.Add(o);
            return list;
        }

#if UNITY_EDITOR
        public static void ToReorderableList(this IList toReorderList, ref ReorderableList reorderableList)
        {
            ReorderableListUtil.ToReorderableList(toReorderList, ref reorderableList);
        }
#endif
    }
}