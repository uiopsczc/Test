using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    public static string scripts_to_replace_path = Application.dataPath;//Application.dataPath+"/UI"
    public static Dictionary<string, Dictionary<string, string>> scripts_to_replace_dict
    {
      get
      {
        var result = new Dictionary<string, Dictionary<string, string>>();
        foreach (var scripts_to_replace_dict in scripts_to_replace_dict_list)
          result[scripts_to_replace_dict["old_fileId"]] = scripts_to_replace_dict;
        return result;
      }
    }
    private static ValueDictList<string, string> scripts_to_replace_dict_list = new ValueDictList<string, string>()
    {
      { new Dictionary<string, string>(){{"old_guid",""},{"old_fileId",""},{"old_type","type:0"},{"new_guid:",""},{"new_fileId:",""},{"new_type", "type:3" } }},
      { new Dictionary<string, string>(){{"old_guid",""},{"old_fileId",""},{"old_type","type:0"},{"new_guid:",""},{"new_fileId:",""},{"new_type", "type:3" } }},
    };


    [MenuItem(CZMToolConst.MenuRoot + "Relpace/Relpace Scripts")]
    public static void RelpaceScripts()
    {
      var root_prefab_path = scripts_to_replace_path;
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
              string matched_line_content = MetaConst.Sprite_Regex.Match(line).Value;
              if (!matched_line_content.IsNullOrEmpty())
              {
                string old_filedId = MetaConst.FileID_Regex.Match(matched_line_content).Value;
                string old_guid = MetaConst.Guid_Regex.Match(matched_line_content).Value;

                if (scripts_to_replace_dict.ContainsKey(old_filedId) && old_guid.Equals(scripts_to_replace_dict[old_filedId]["old_guid"]))
                {
                  is_changed = true;
                  var dict = scripts_to_replace_dict[old_filedId];
                  lines[i] = Regex.Replace(lines[i], old_filedId, dict["new_fileId"])
                    .Replace(old_guid, dict["new_guid"]).Replace(dict["old_type"],dict["new_type"]);
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