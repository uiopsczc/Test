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
        public static bool isInited = false;

        public static Dictionary<string, Dictionary<string, string>> langDict =
            new Dictionary<string, Dictionary<string, string>>();

        public static string GetText(string lang, params object[] args)
        {
            Init();
            if (GameData.instance.langData.language == null)
                return lang;
            if (lang == null)
                return lang;
            string toLangEscape = lang.Replace("\r\n", "\n").Replace("\r", "\n");
            if (!langDict.ContainsKey(toLangEscape))
                return string.Format(lang, args);
            if (!langDict[toLangEscape].ContainsKey(GameData.instance.langData.language))
                return string.Format(lang, args);
            if (langDict[toLangEscape][GameData.instance.langData.language].IsNullOrWhiteSpace())
                return string.Format(lang, args);
            return string.Format(langDict[toLangEscape][GameData.instance.langData.language], args);
        }

        public static void Init()
        {
            isInited = true;
            langDict.Clear();
            Dictionary<string, bool> langFieldNameDict = new Dictionary<string, bool>();

            var properties = typeof(CfgLangData).GetProperties();
            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];
                if (!propertyInfo.Name.Equals("id"))
                    langFieldNameDict[propertyInfo.Name] = true;
            }

#if UNITY_EDITOR
            var jsonContent = AssetDatabase.LoadAssetAtPath<TextAsset>(LangConst.Json_File_Path).text;
            CfgLang.Instance.Parse(jsonContent);
#endif

            var allCfgLangDatas = CfgLang.Instance.All();
            for (var i = 0; i < allCfgLangDatas.Count; i++)
            {
                var cfgLangData = allCfgLangDatas[i];
                var id = cfgLangData.id;
                langDict[id] = new Dictionary<string, string>();
                foreach (var fieldName in langFieldNameDict.Keys)
                    langDict[id][fieldName] = cfgLangData.GetPropertyValue<string>(fieldName);
            }
        }

        public static void Refresh()
        {
            isInited = false;
            Init();
        }
    }
}