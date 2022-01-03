using System;
using System.IO;
using LuaInterface;
using UnityEngine;
#if USE_UNI_LUA
using LuaDLL = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;

#endif


namespace CsCat
{
	/// <summary>
	///   对xlua中的东西进行修改， XLuaFix中的函数的命名规范：{0}_{1}_{2}  {0}表示修改的类，{1}表示修改类中的函数，{2}表示修改的变量
	/// </summary>
	public static class XLuaFix
	{
		public static string HotFix_HotfixInject_inject_tool_path()
		{
			return Path.Combine(Application.dataPath.Replace("Assets", ""), "Tools/XLuaHotfixInject.exe");
		}

		public static string GetWhere(RealStatePtr L, int n)
		{
			var s = "";
#if UNITY_EDITOR
			LuaAPI.luaL_where(L, 1);
			var fileLine = LuaAPI.lua_tostring(L, -1);
			fileLine = fileLine.Substring(0, fileLine.LastIndexOf(":")); //删除最后的:之后的字符串
			var line = fileLine.Substring(fileLine.LastIndexOf(":") + 1);
			var fileName = fileLine.Substring(0, fileLine.LastIndexOf(":"));
			LuaAPI.lua_settop(L, n);
			var offset = fileName[0] == '@' ? 1 : 0;

			if (!fileName.Contains("."))
				s = string.Format("[{0}{1}:{2}]:", fileName.Substring(offset, fileName.Length - offset), BuildConst.Lua_Suffix,
				  line);
			else
				s = string.Format("[{0}{1}:{2}]:", fileName.Substring(offset, fileName.Length - offset), "", line);

#endif
			return s;
		}

		//抄自tolua的print处理
		public static int StaticLuaCallbacks_Print(RealStatePtr L)
		{
			try
			{
				var n = LuaAPI.lua_gettop(L);
				var s = string.Empty;
				if (0 != LuaAPI.xlua_getglobal(L, "tostring")) return LuaAPI.luaL_error(L, "can not get tostring in print:");

				for (var i = 1; i <= n; i++)
				{
					LuaAPI.lua_pushvalue(L, -1); /* function to be called */
					LuaAPI.lua_pushvalue(L, i); /* value to print */
					if (0 != LuaAPI.lua_pcall(L, 1, 1, 0)) return LuaAPI.lua_error(L);

					s += LuaAPI.lua_tostring(L, -1);

					if (i != n) s += "\t";

					LuaAPI.lua_pop(L, 1); /* pop result */
				}

				s = GetWhere(L, n) + s;
				Debugger.Log(s);
				return 0;
			}
			catch (Exception e)
			{
				return LuaAPI.luaL_error(L, "c# exception in print:" + e);
			}
		}
	}
}