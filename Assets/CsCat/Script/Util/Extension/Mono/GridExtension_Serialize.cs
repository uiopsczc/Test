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
      Hashtable hashtable = new Hashtable();
      hashtable["cellSize"] = self.cellSize.ToStringOrDefault();
      hashtable["cellGap"] = self.cellGap.ToStringOrDefault();
      hashtable["cellLayout"] = (int) self.cellLayout;
      hashtable["cellSwizzle"] = (int) self.cellSwizzle;
      hashtable.Trim();
      return hashtable;
    }

    public static void LoadSerializeHashtable(this Grid self, Hashtable hashtable)
    {
      self.cellSize = hashtable.Get<string>("cellSize").ToVector3OrDefault();
      self.cellGap = hashtable.Get<string>("cellGap").ToVector3OrDefault();
      self.cellLayout = hashtable.Get<int>("cellLayout").ToEnum<GridLayout.CellLayout>();
      self.cellSwizzle = hashtable.Get<int>("cellSwizzle").ToEnum<GridLayout.CellSwizzle>();
    }

  }
}