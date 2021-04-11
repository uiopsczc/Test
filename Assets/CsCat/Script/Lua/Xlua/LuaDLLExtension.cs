using System;
using XLua;
using LuaDLL = XLua.LuaDLL.Lua;

namespace CsCat
{
  public static class LuaDLLExtension
  {
    public static int toluaL_exception(IntPtr L, Exception e)
    {
      return LuaDLL.luaL_error(L, e.Message);
    }

    public static string luaL_typename(IntPtr luaState, int stackPos)
    {
      var type = LuaDLL.lua_type(luaState, stackPos);
      return lua_typename(luaState, type);
    }

    public static string lua_typename(IntPtr luaState, LuaTypes type)
    {
      var t = (int) type;
      return type.ToString();
    }
  }
}