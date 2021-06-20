using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CsCat
{
  // 
  public class Lang
  {
    public static bool is_inited = false;

    public static Dictionary<string, Dictionary<string, string>> lang_dict =
      new Dictionary<string, Dictionary<string, string>>();

    public static string GetText(string lang, params object[] args)
    {
      Init();
      if (GameData.instance.langData.language == null)
        return lang;
      if (lang == null)
        return lang;
      string to_lang_escape = lang.Replace("\r\n", "\n").Replace("\r", "\n");
      if (!lang_dict.ContainsKey(to_lang_escape))
        return string.Format(lang, args);
      if (!lang_dict[to_lang_escape].ContainsKey(GameData.instance.langData.language))
        return string.Format(lang, args);
      ;
      if (lang_dict[to_lang_escape][GameData.instance.langData.language].IsNullOrWhiteSpace())
        return string.Format(lang, args);
      return string.Format(lang_dict[to_lang_escape][GameData.instance.langData.language], args);
    }

    public static void Init()
    {
        is_inited = true;
        lang_dict.Clear();
        Dictionary<string, bool> lang_fieldName_dict = new Dictionary<string, bool>();

        foreach (var propertyInfo in typeof(CfgLangData).GetProperties())
        {
          if (!propertyInfo.Name.Equals("id"))
            lang_fieldName_dict[propertyInfo.Name] = true;
        }

#if UNITY_EDITOR
      var json_content = AssetDatabase.LoadAssetAtPath<TextAsset>(LangConst.Json_File_Path).text;
      CfgLang.Instance.Parse(json_content);
#endif

      foreach (var cfgLangData in CfgLang.Instance.All())
        {
          var id = cfgLangData.id;
          lang_dict[id] = new Dictionary<string, string>();
          foreach (var fieldName in lang_fieldName_dict.Keys)
          {
            lang_dict[id][fieldName] = cfgLangData.GetPropertyValue<string>(fieldName);
          }
        }
    }

    public static void Refresh()
    {
      is_inited = false;
      Init();
    }
  }
}