using System;
using System.Collections.Generic;
using System.Linq;

namespace CsCat
{
  /// <summary>
  /// WeakReferenceList
  /// </summary>
  /// <typeparam name="V"></typeparam>
  public class WeakReferenceList<V>
  {
    #region field

    private List<WeakReference> list = new List<WeakReference>();

    #endregion

    #region property

    public int Count
    {
      get { return this.list.Count; }
    }

    public V this[int index]
    {
      get
      {
        var valueResult = list[index].GetValueResult<V>();
        return valueResult.GetValue();
      }
      set { this.Set(index, value); }
    }

    #endregion

    #region public method

    public V Add(V value)
    {
      this.list.Add(new WeakReference(value));
      return value;
    }

    public void Clear()
    {
      this.list.Clear();
    }

    public bool Contains(V value)
    {
      return this.list.Any(a =>
      {
        var valueResult = a.GetValueResult<V>();
        if (valueResult.GetIsHasValue() && ObjectUtil.Equals(value, valueResult.GetValue()))
          return true;
        else
          return false;
      });
    }

    public void RemoveAt(int index)
    {
      this.list.RemoveAt(index);
    }

    public void Set(int index, V value)
    {
      this.list[index] = new WeakReference(value);
    }

    public void GC()
    {
      List<WeakReference> to_remove_list = new List<WeakReference>();
      foreach (var e in list)
      {
        if (!e.IsAlive)
          to_remove_list.Add(e);
      }

      if (to_remove_list.Count > 0)
      {
        foreach (var e in to_remove_list)
          list.Remove(e);
        System.GC.Collect(0);
      }
    }

    #endregion

  }
}