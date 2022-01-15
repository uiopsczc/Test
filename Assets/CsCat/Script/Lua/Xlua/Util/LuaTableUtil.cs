using XLua;

namespace CsCat
{
	public static class LuaTableUtil
	{
		private static LuaFunction luaFunction;

		static (LuaTable, string) GetTargetLuaTableAndMethodName(LuaTable luaTable, string methodPath)
		{
			int index = methodPath.LastIndexOf('.');
			if (index == -1)
				index = methodPath.LastIndexOf(':');
			LuaTable targetLuaTable = index == -1 ? luaTable : luaTable.GetInPath<LuaTable>(methodPath.Substring(0, index));
			string methodName = index == -1 ? methodPath : methodPath.Substring(index + 1);
			return (targetLuaTable, methodName);
		}

		public static void InvokeAction(LuaTable luaTable, string methodPath)
		{
			var (targetLuaTable, methodName) = GetTargetLuaTableAndMethodName(luaTable, methodPath);
			targetLuaTable.Get(methodName, out luaFunction);
			luaFunction.Action(targetLuaTable);
			luaFunction.Dispose();
		}

		public static void InvokeAction<T0>(LuaTable luaTable, string methodPath, T0 arg0)
		{
			var (targetLuaTable, methodName) = GetTargetLuaTableAndMethodName(luaTable, methodPath);
			targetLuaTable.Get(methodName, out luaFunction);
			luaFunction.Action(targetLuaTable, arg0);
			luaFunction.Dispose();
		}

		public static TResult InvokeFunc<TResult>(LuaTable luaTable, string methodPath)
		{
			var (targetLuaTable, methodName) = GetTargetLuaTableAndMethodName(luaTable, methodPath);
			targetLuaTable.Get(methodName, out luaFunction);
			TResult result = luaFunction.Func<LuaTable, TResult>(targetLuaTable);
			luaFunction.Dispose();
			return result;
		}


		public static TResult InvokeFunc<T0, TResult>(LuaTable luaTable, string methodPath, T0 arg0)
		{
			var (targetLuaTable, methodName) = GetTargetLuaTableAndMethodName(luaTable, methodPath);
			targetLuaTable.Get(methodName, out luaFunction);
			TResult result = luaFunction.Func<LuaTable, T0, TResult>(targetLuaTable, arg0);
			luaFunction.Dispose();
			return result;
		}

	}
}