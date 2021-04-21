
using System;
using UnityEngine;

namespace CsCat
{
  public class TimelinableItemInfoLibraryBase : ScriptableObject
  {
    public virtual TimelinableItemInfoBase[] itemInfoes
    {
      get { return null; }
      set { }
    }

    public void AddItemInfo(TimelinableItemInfoBase timelinableItemInfo)
    {
      itemInfoes = ArrayUtil.AddLast(itemInfoes, timelinableItemInfo) as TimelinableItemInfoBase[];
    }

    public void RemoveItemInfo(TimelinableItemInfoBase timelinableItemInfo)
    {
      itemInfoes = ArrayUtil.Remove(itemInfoes, timelinableItemInfo) as TimelinableItemInfoBase[];
    }

    public void RemoveItemInfoAt(int index)
    {
      itemInfoes = ArrayUtil.RemoveAt(itemInfoes, index) as TimelinableItemInfoBase[];
    }
  }
}



