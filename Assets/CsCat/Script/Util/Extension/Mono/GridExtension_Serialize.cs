using System.Collections;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace CsCat
{
    public static partial class GridExtension
    {
        public static Hashtable GetSerializeHashtable(this Grid self)
        {
            Hashtable hashtable = new Hashtable
            {
                [StringConst.String_cellSize] = self.cellSize.ToStringOrDefault(),
                [StringConst.String_cellGap] = self.cellGap.ToStringOrDefault(),
                [StringConst.String_cellLayout] = (int) self.cellLayout,
                [StringConst.String_cellSwizzle] = (int) self.cellSwizzle
            };
            hashtable.Trim();
            return hashtable;
        }

        public static void LoadSerializeHashtable(this Grid self, Hashtable hashtable)
        {
            self.cellSize = hashtable.Get<string>(StringConst.String_cellSize).ToVector3OrDefault();
            self.cellGap = hashtable.Get<string>(StringConst.String_cellGap).ToVector3OrDefault();
            self.cellLayout = hashtable.Get<int>(StringConst.String_cellLayout).ToEnum<GridLayout.CellLayout>();
            self.cellSwizzle = hashtable.Get<int>(StringConst.String_cellSwizzle).ToEnum<GridLayout.CellSwizzle>();
        }
    }
}