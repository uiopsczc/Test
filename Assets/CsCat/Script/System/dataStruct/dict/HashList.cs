using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  //同HashSet一样，但有序
  public class HashList<T> : List<T>
  {
    private Dictionary<T, bool> dict = new Dictionary<T, bool>();
    

    public new  void Add(T item)
    {
      if (dict.ContainsKey(item))
        return;
      dict[item] = true;
      base.Add(item);
    }

    public new void Clear()
    {
      dict.Clear();
      base.Clear();
    }

    public new  bool Contains(T item)
    {
      return dict.ContainsKey(item);
    }
    

    public new  bool Remove(T item)
    {
      dict.Remove(item);
      return base.Remove(item);
    }
    

    public new  void Insert(int index, T item)
    {
      if (dict.ContainsKey(item))
        return;
      dict[item] = true;
      base.Insert(index,item);
    }

    public new  void RemoveAt(int index)
    {
      T item = base[index];
      dict.Remove(item);
      base.RemoveAt(index);

    }

    public new T this[int index]
    {
      get => base[index];
      set
      {
        var orgin_item = base[index];
        dict.Remove(orgin_item);
        dict[value] = true;
        base[index] = value;
      }
    }
  }
}

