using System;
using UnityEngine;
using XLua;

namespace CsCat
{
	public static class LuaEnvExtension
	{
		public static void SafeDoAction(this LuaEnv self, Action action)
		{
			if (self == null)
				return;
			try
			{
				action();
			}
			catch (Exception ex)
			{
				LogCat.LogError(string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace));
			}
		}

		public static void SafeDoString(this LuaEnv self, string lua_content, string chunk_name = "chunk")
		{
			self.SafeDoAction(() => self.DoString(lua_content, chunk_name));
		}

		public static T DoString<T>(this LuaEnv self, string chunk, string chunk_name = "chunk", LuaTable env = null,
		  params object[] args)
		{
			var results = self.DoString(chunk, chunk_name, env, args);
			return results == null ? default : (T)results[0];
		}

		public static object[] DoString(this LuaEnv self, string chunk, string chunk_name = "chunk", LuaTable env = null,
		  params object[] args)
		{
			for (var i = 0; i < args.Length; i++) self.Global.Set("arg" + i, args[i]);

			var results = self.DoString(chunk, chunk_name, env);
			for (var i = 0; i < args.Length; i++)
				self.DoString(string.Format("arg{0}=nil", i));
			return results;
		}

		public static void SafeDispose(this LuaEnv self)
		{
			self.SafeDoAction(() =>
			{
				self.Dispose();
				self = null;
			});
		}

		public static void LoadLua(this LuaEnv self, string lua_name, string chunk_name = "chunk")
		{
			self.SafeDoString(string.Format("require('{0}')", lua_name), chunk_name);
		}

		public static void ReloadLua(this LuaEnv self, string lua_name, string chunk_name = "chunk")
		{
			self.SafeDoString(string.Format("package.loaded['{0}'] = nil", lua_name), chunk_name);
			self.LoadLua(lua_name, chunk_name);
		}


		public static void ReloadAll(this LuaEnv self)
		{
			var reload_all = @"
        for key, value in pairs(package.loaded) do
            package.loaded[key] = nil
        end";
			self.DoString(reload_all);
		}


		public static LuaTable NewScriptEnvOfFilePath(this LuaEnv self, object obj, string lua_file_path)
		{
			var file_path = lua_file_path.WithoutAllSuffix().Replace("/", ".");
			return self.NewScriptEnv(obj, string.Format("require \"{0}\"", file_path));
		}

		public static LuaTable NewScriptEnv(this LuaEnv self, object obj, string lua_file_content)
		{
			var script_env = self.NewTable();

			// 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
			var metatable = self.NewTable();
			metatable.Set("__index", self.Global);
			script_env.SetMetaTable(metatable);
			metatable.Dispose();

			script_env.Set("self", obj);
			self.DoString(lua_file_content, obj.GetType().Name, script_env);
			return script_env;
		}

		public static LuaTable NewScriptEnv(this LuaEnv self, object obj, TextAsset lua_file)
		{
			return self.NewScriptEnv(obj, lua_file.text);
		}
	}
}