using System;
using System.Collections.Generic;

namespace CsCat
{
	public class LuaPathMap
	{
		private string fileContent;
		protected Dictionary<string, string> luaName2LuaPathDict = new Dictionary<string, string>();

		public void SaveToDisk()
		{
			var path = BuildConst.Lua_Path_Map_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
			StdioUtil.WriteTextFile(path, fileContent);
		}


		public void Initialize(string content)
		{
			if (content.IsNullOrWhiteSpace())
			{
				LogCat.LogError("LuaPathMap empty!!");
				return;
			}

			fileContent = content;
			content = content.Replace("\r\n", "\n");
			var mapList = content.Split('\n');
			for (var i = 0; i < mapList.Length; i++)
			{
				var map = mapList[i];
				if (map.IsNullOrWhiteSpace())
					continue;

				var splits = map.Split(new[] {StringConst.String_Comma}, StringSplitOptions.None);
				if (splits.Length < 2)
				{
					LogCat.LogError("splitArr length < 2 : " + map);
					continue;
				}

				var luaName = splits[0];
				var luaPath = splits[1];

				luaName2LuaPathDict[luaName] = luaPath;
			}
		}


		public string GetLuaPath(string luaName)
		{
			return luaName2LuaPathDict[luaName];
		}
	}
}