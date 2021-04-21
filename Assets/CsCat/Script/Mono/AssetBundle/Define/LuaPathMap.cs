using System;
using System.Collections.Generic;

namespace CsCat
{
  public class LuaPathMap
  {
    private string file_content;
    protected Dictionary<string, string> luaName_2_luaPath_Dict = new Dictionary<string, string>();

    public void SaveToDisk()
    {
      var path = BuildConst.Lua_Path_Map_File_Name.WithRootPath(FilePathConst.PersistentAssetBundleRoot);
      StdioUtil.WriteTextFile(path, file_content);
    }


    public void Initialize(string content)
    {
      if (content.IsNullOrWhiteSpace())
      {
        LogCat.LogError("LuaPathMap empty!!");
        return;
      }

      file_content = content;
      content = content.Replace("\r\n", "\n");
      var map_list = content.Split('\n');
      foreach (var map in map_list)
      {
        if (map.IsNullOrWhiteSpace())
          continue;

        var splits = map.Split(new[] { DictConst.Common_Pattren }, StringSplitOptions.None);
        if (splits.Length < 2)
        {
          LogCat.LogError("splitArr length < 2 : " + map);
          continue;
        }

        var lua_name = splits[0];
        var lua_path = splits[1];

        luaName_2_luaPath_Dict[lua_name] = lua_path;
      }
    }


    public string GetLuaPath(string lua_name)
    {
      return luaName_2_luaPath_Dict[lua_name];
    }
  }
}