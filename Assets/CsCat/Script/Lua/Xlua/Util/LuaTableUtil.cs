using XLua;

namespace CsCat
{
  public static class LuaTableUtil
  {
    private static LuaFunction luaFunction;

    static (LuaTable,string) GetTargetLuaTableAndMethodName(LuaTable luaTable, string method_path)
    {
      int index = method_path.LastIndexOf('.');
      if (index == -1)
        index = method_path.LastIndexOf(':');
      LuaTable target_luaTable = index == -1 ? luaTable : luaTable.GetInPath<LuaTable>(method_path.Substring(0, index));
      string method_name = index == -1 ? method_path : method_path.Substring(index + 1);
      return (target_luaTable, method_name);
    }

    public static void InvokeAction(LuaTable luaTable, string method_path)
    {
      var (target_luaTable, method_name) = GetTargetLuaTableAndMethodName(luaTable, method_path);
      target_luaTable.Get(method_name, out luaFunction);
      luaFunction.Action(target_luaTable);
      luaFunction.Dispose();
    }

    public static void InvokeAction<T0>(LuaTable luaTable, string method_path, T0 arg0)
    {
      var (target_luaTable, method_name) = GetTargetLuaTableAndMethodName(luaTable, method_path);
      target_luaTable.Get(method_name, out luaFunction);
      luaFunction.Action(target_luaTable, arg0);
      luaFunction.Dispose();
    }

    public static TResult InvokeFunc<TResult>(LuaTable luaTable, string method_path)
    {
      var (target_luaTable, method_name) = GetTargetLuaTableAndMethodName(luaTable, method_path);
      target_luaTable.Get(method_name, out luaFunction);
      TResult result =  luaFunction.Func<LuaTable, TResult>(target_luaTable);
      luaFunction.Dispose();
      return result;
    }


    public static TResult InvokeFunc<T0,TResult>(LuaTable luaTable, string method_path, T0 arg0)
    {
      var (target_luaTable, method_name) = GetTargetLuaTableAndMethodName(luaTable, method_path);
      target_luaTable.Get(method_name, out luaFunction);
      TResult result = luaFunction.Func<LuaTable,T0, TResult>(target_luaTable, arg0);
      luaFunction.Dispose();
      return result;
    }

  }
}