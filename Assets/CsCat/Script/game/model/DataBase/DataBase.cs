using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class DataBase
  {
    protected Hashtable _data;

    public object this[object key]
    {
      get
      {
        if (key == null)
          return null;

        object result = null;

        if (this._data.ContainsKey(key))
          result = this._data[key];

        return result;
      }
      set
      {
          this._data[key] = value;
      }
    }
  }
}