using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public static class ValueParseUtil
  {
    private static readonly List<Hashtable> valueParse_list = new List<Hashtable>();

    private static void AddValueParseElement<T>(Func<string, T> func)
    {
      var hashtable = new Hashtable();
      hashtable["type"] = typeof(T);
      hashtable["parseFunc"] = func;
      valueParse_list.Add(hashtable);
    }

    public static List<Hashtable> GetValueParseList()
    {
      valueParse_list.Clear();
      AddValueParseElement(content => content);
      AddValueParseElement(content => bool.Parse(content));
      AddValueParseElement(content => char.Parse(content));
      AddValueParseElement(content => short.Parse(content));
      AddValueParseElement(content => int.Parse(content));
      AddValueParseElement(content => long.Parse(content));
      AddValueParseElement(content => content.ToVector3());
      AddValueParseElement(content => content.ToVector2());
      return valueParse_list;
    }
  }
}