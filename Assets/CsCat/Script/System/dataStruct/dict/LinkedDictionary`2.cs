using System.Collections.Generic;
using System.Collections;

namespace CsCat
{
  public class LinkedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IGetLinkedHashtable
  {
    #region field

    LinkedHashtable table = new LinkedHashtable();

    #endregion

    #region property

    public new TValue this[TKey key]
    {
      get { return base[key]; }
      set
      {
        table[key] = value;
        base[key] = value;
      }
    }

    public new List<TKey> Keys
    {
      get { return (table.Keys).ToList<TKey>(); }
    }

    public new List<TValue> Values
    {
      get { return ((ArrayList)table.Values).ToList<TValue>(); }
    }

    public List<KeyValuePair<TKey, TValue>> KeyValues
    {
      get
      {
        List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
        foreach (TKey key in Keys)
        {
          list.Add(new KeyValuePair<TKey, TValue>(key, (TValue)table[key]));
        }

        return list;
      }
    }

    #endregion

    #region public method

    public new void Add(TKey key, TValue value)
    {
      this[key] = value;
    }

    public new void Clear()
    {
      base.Clear();
      table.Clear();
    }

    public new bool Remove(TKey key)
    {
      bool result = base.Remove(key);
      if (result)
      {
        table.Remove(key);
      }

      return result;
    }


    public new IDictionaryEnumerator GetEnumerator()
    {
      return table.GetEnumerator();
    }

    public LinkedHashtable GetLinkedHashtable()
    {
      return table;

    }

    #endregion



  }
}

