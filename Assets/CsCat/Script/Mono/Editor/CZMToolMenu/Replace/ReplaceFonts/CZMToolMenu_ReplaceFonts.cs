using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    public static string font_to_replace_path = Application.dataPath; //Application.dataPath+"/UI"

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


    [MenuItem(CZMToolConst.MenuRoot + "Relpace/Relpace Fonts")]
    public static void RelpaceFonts()
    {
      var root_prefab_path = font_to_replace_path;
      if (Directory.Exists(root_prefab_path))
      {
        string[] all_prefab_pathes = Directory.GetFiles(root_prefab_path, "*.prefab", SearchOption.AllDirectories);
        foreach (string prefab_path in all_prefab_pathes)
        {
          bool is_changed = false;
          var lines = File.ReadAllLines(prefab_path);
          for (int i = 0; i < lines.Length; i++)
          {
            var line = lines[i];
            string matched_line_content = MetaConst.font_regex.Match(line).Value;
            if (!matched_line_content.IsNullOrEmpty())
            {
              string old_filedId = MetaConst.fileID_regex.Match(matched_line_content).Value;
              string old_guid = MetaConst.guid_regex.Match(matched_line_content).Value;

              if (font_to_replace_dict.ContainsKey(old_filedId) &&
                  old_guid.Equals(font_to_replace_dict[old_filedId]["old_guid"]))
              {
                is_changed = true;
                var dict = font_to_replace_dict[old_filedId];
                lines[i] = Regex.Replace(lines[i], old_filedId, dict["new_fileId"])
                  .Replace(old_guid, dict["new_guid"]).Replace(dict["old_type"], dict["new_type"]);
              }
            }
          }

          if (is_changed)
            File.WriteAllLines(prefab_path, lines);
        }

        AssetDatabase.Refresh();
        EditorUtilityCat.DisplayDialog("Relpace Fonts finished");
      }
    }
  }
}