using System;
using System.Collections.Generic;

namespace CsCat
{
  public class ValueDictList<TKey, TValue> : List<Dictionary<TKey, TValue>>
  {
    public void Add(Dictionary<TKey, TValue> dict)
    {
      this.Add(dict);
    }

    public void Add(string dict_string)
    {
      var dict = dict_string.ToDictionary<TKey, TValue>();
      Add(dict);
    }

    public new void Clear()
    {
      base.Clear();
    }

    
  }
}