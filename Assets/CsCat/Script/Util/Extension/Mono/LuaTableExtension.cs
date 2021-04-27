using UnityEngine;
using XLua;

namespace CsCat
{
  public static class LuaTableExtension
  {
    public static void InvokeAction(this LuaTable self, string method)
    {
      LuaTableUtil.InvokeAction(self, method);
    }

    public static void InvokeAction<T0>(this LuaTable self, string method, T0 arg0)
    {
      LuaTableUtil.InvokeAction(self, method, arg0);
    }

    public static TResult InvokeFunc<TResult>(this LuaTable self, string method)
    {
      return LuaTableUtil.InvokeFunc<TResult>(self, method);
    }


    public static TResult InvokeFunc<T0, TResult>(this LuaTable self, string method, T0 arg0)
    {
      return LuaTableUtil.InvokeFunc<T0, TResult>(self, method, arg0);
    }
  }
}