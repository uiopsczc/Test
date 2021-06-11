using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public static class CZMToolMenu_ReplaceConst
  {
    public static string sprite_to_replace_path = Application.dataPath;//Application.dataPath+"/UI"
    public static Dictionary<string, Dictionary<string, string>> sprite_to_replace_dict
    {
      get
      {
        var result = new Dictionary<string, Dictionary<string, string>>();
        foreach (var sprite_to_replace_dict in sprite_to_replace_dict_list)
          result[sprite_to_replace_dict["old_fileId"]] = sprite_to_replace_dict;
        return result;
      }
    }
    private static ValueDictList<string, string> sprite_to_replace_dict_list = new ValueDictList<string, string>()
    {
      { new Dictionary<string, string>(){{"old_guid:",""},{"old_fileId:",""},{"new_guid:",""},{"new_fileId:",""}}},
      { new Dictionary<string, string>(){{"old_guid:",""},{"old_fileId:",""},{"new_guid:",""},{"new_fileId:",""}}},
    };


    public static string font_to_replace_path = Application.dataPath;//Application.dataPath+"/UI"
    public static Dictionary<string, Dictionary<string, string>> font_to_replace_dict
    {
      get
      {
        var result = new Dictionary<string, Dictionary<string, string>>();
        foreach (var font_to_replace_dict in font_to_replace_dict_list)
          result[font_to_replace_dict["old_fileId"]] = font_to_replace_dict;
        return result;
      }
    }
    private static ValueDictList<string, string> font_to_replace_dict_list = new ValueDictList<string, string>()
    {
      { new Dictionary<string, string>(){{"old_guid",""},{"old_fileId",""},{"old_type","type:0"},{"new_guid:",""},{"new_fileId:",""},{"new_type", "type:3" } }},
      { new Dictionary<string, string>(){{"old_guid",""},{"old_fileId",""},{"old_type","type:0"},{"new_guid:",""},{"new_fileId:",""},{"new_type", "type:3" } }},
    };

  }
}