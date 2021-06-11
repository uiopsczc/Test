using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu
  {
    [MenuItem(CZMToolConst.MenuRoot + "Relpace/Relpace Sprites")]
    public static void RelpaceSprites()
    {
      var root_prefab_path = CZMToolMenu_ReplaceConst.sprite_to_replace_path;
      if (Directory.Exists(root_prefab_path))
      {
        string[] all_prefab_pathes = Directory.GetFiles(root_prefab_path, "*.prefab", SearchOption.AllDirectories);
        if (all_prefab_pathes != null)
        {
          foreach (string prefab_path in all_prefab_pathes)
          {
            bool is_changed = false;
            var lines = File.ReadAllLines(prefab_path);
            for (int i = 0; i < lines.Length; i++)
            {
              var line = lines[i];
              string matched_line_content = MetaConst.sprite_regex.Match(line).Value;
              if (!matched_line_content.IsNullOrEmpty())
              {
                string old_filedId = MetaConst.fileID_regex.Match(matched_line_content).Value;
                string old_guid = MetaConst.guid_regex.Match(matched_line_content).Value;

                if (CZMToolMenu_ReplaceConst.sprite_to_replace_dict.ContainsKey(old_filedId) && old_guid.Equals(CZMToolMenu_ReplaceConst.sprite_to_replace_dict[old_filedId]["old_guid"]))
                {
                  is_changed = true;
                  var dict = CZMToolMenu_ReplaceConst.sprite_to_replace_dict[old_filedId];
                  lines[i] = Regex.Replace(lines[i], old_filedId, dict["new_fileId"])
                    .Replace(old_guid, dict["new_guid"]);
                }
              }
            }
            if (is_changed)
              File.WriteAllLines(prefab_path, lines);
          }
        }
        AssetDatabase.Refresh();
        EditorUtilityCat.DisplayDialog("Relpace Sprites finished");
      }
    }


    [MenuItem(CZMToolConst.MenuRoot + "Relpace/Relpace Fonts")]
    public static void RelpaceFonts()
    {
      var root_prefab_path = CZMToolMenu_ReplaceConst.font_to_replace_path;
      if (Directory.Exists(root_prefab_path))
      {
        string[] all_prefab_pathes = Directory.GetFiles(root_prefab_path, "*.prefab", SearchOption.AllDirectories);
        if (all_prefab_pathes != null)
        {
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

                if (CZMToolMenu_ReplaceConst.font_to_replace_dict.ContainsKey(old_filedId) && old_guid.Equals(CZMToolMenu_ReplaceConst.font_to_replace_dict[old_filedId]["old_guid"]))
                {
                  is_changed = true;
                  var dict = CZMToolMenu_ReplaceConst.font_to_replace_dict[old_filedId];
                  lines[i] = Regex.Replace(lines[i], old_filedId, dict["new_fileId"])
                    .Replace(old_guid, dict["new_guid"]).Replace(dict["old_type"],dict["new_type"]);
                }
              }
            }
            if (is_changed)
              File.WriteAllLines(prefab_path, lines);
          }
        }
        AssetDatabase.Refresh();
        EditorUtilityCat.DisplayDialog("Relpace Fonts finished");
      }
    }

  }
}