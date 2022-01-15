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

		public static void SafeDoString(this LuaEnv self, string luaContent, string chunkName = "chunk")
		{
			self.SafeDoAction(() => self.DoString(luaContent, chunkName));
		}

		public static T DoString<T>(this LuaEnv self, string chunk, string chunkName = "chunk", LuaTable env = null,
			params object[] args)
		{
			var results = self.DoString(chunk, chunkName, env, args);
			return results == null ? default : (T) results[0];
		}

		public static object[] DoString(this LuaEnv self, string chunk, string chunkName = "chunk", LuaTable env = null,
			params object[] args)
		{
			for (var i = 0; i < args.Length; i++) self.Global.Set("arg" + i, args[i]);

			var results = self.DoString(chunk, chunkName, env);
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

		public static void LoadLua(this LuaEnv self, string luaName, string chunkName = "chunk")
		{
			self.SafeDoString(string.Format("require('{0}')", luaName), chunkName);
		}

		public static void ReloadLua(this LuaEnv self, string luaName, string chunkName = "chunk")
		{
			self.SafeDoString(string.Format("package.loaded['{0}'] = nil", luaName), chunkName);
			self.LoadLua(luaName, chunkName);
		}


		public static void ReloadAll(this LuaEnv self)
		{
			var reload_all = @"
        for key, value in pairs(package.loaded) do
            package.loaded[key] = nil
        end";
			self.DoString(reload_all);
		}


		public static LuaTable NewScriptEnvOfFilePath(this LuaEnv self, object obj, string luaFilePath)
		{
			var filePath = luaFilePath.WithoutAllSuffix().Replace("/", ".");
			return self.NewScriptEnv(obj, string.Format("require \"{0}\"", filePath));
		}

		public static LuaTable NewScriptEnv(this LuaEnv self, object obj, string luaFileContent)
		{
			var scriptEnv = self.NewTable();

			// 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
			var metatable = self.NewTable();
			metatable.Set("__index", self.Global);
			scriptEnv.SetMetaTable(metatable);
			metatable.Dispose();

			scriptEnv.Set("self", obj);
			self.DoString(luaFileContent, obj.GetType().Name, scriptEnv);
			return scriptEnv;
		}

		public static LuaTable NewScriptEnv(this LuaEnv self, object obj, TextAsset luaFile)
		{
			return self.NewScriptEnv(obj, luaFile.text);
		}
	}
}