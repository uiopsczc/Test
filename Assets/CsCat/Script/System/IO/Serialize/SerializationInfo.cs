using System;
using System.Collections;

namespace CsCat
{
  public struct SerializationInfo
  {
    #region field

    private readonly ArrayList list;
    private readonly object context;

    #endregion

    #region ctor

    internal SerializationInfo(ArrayList list, object context)
    {
      this.list = list;
      this.context = context;
    }

    #endregion

    #region public method

    public T GetValue<T>(string name)
    {
      var value = GetValue(name, typeof(T));
      if (value == null) return default;
      return (T)value;
    }

    public object GetValue(string name, Type type)
    {
      var count = list.Count;
      for (var i = 0; i < count; i++)
      {
        var enumerator = (list[i] as Hashtable).GetEnumerator();
        enumerator.MoveNext();
        var key = enumerator.Key.ToString();
        if (name == key) return JsonSerializer.Deserialize(enumerator.Value, type, context);
      }

      return null;
    }

    public void SetValue(string name, object value)
    {
      if (value == null) return;
      var value2 = JsonSerializer.SerializeObject(value, context);
      var hashtable = new Hashtable();
      hashtable[name] = value2;
      list.Add(hashtable);
    }

    #endregion
  }
}