using System.Collections.Generic;

namespace CsCat
{
  // З­вы
  public class Translation
  {
    private static bool is_inited = false;

    public static Dictionary<string, Dictionary<string, string>> translation_dict =
      new Dictionary<string, Dictionary<string, string>>();

    public static string GetText(string to_translate,params object[] args)
    {
      Init();
      if (GameData.instance.translationData.language == null)
        return to_translate;
      if (to_translate == null)
        return to_translate;
      string to_translate_escape = to_translate.Replace("\r\n", "\n").Replace("\r", "\n");
      if (!translation_dict.ContainsKey(to_translate_escape))
        return string.Format(to_translate,args);
      if (!translation_dict[to_translate_escape].ContainsKey(GameData.instance.translationData.language))
        return string.Format(to_translate, args); ;
      if (translation_dict[to_translate_escape][GameData.instance.translationData.language].IsNullOrWhiteSpace())
        return string.Format(to_translate, args);
      return string.Format(translation_dict[to_translate_escape][GameData.instance.translationData.language],args);
    }

    public static void Init()
    {
      if (!is_inited)
      {
        is_inited = true;
        translation_dict.Clear();
        Dictionary<string, bool> translationAsset_fieldName_dict = new Dictionary<string, bool>();

        foreach (var fieldInfo in typeof(CfgTranslationData).GetFields())
        {
          if (!fieldInfo.Name.Equals("id"))
            translationAsset_fieldName_dict[fieldInfo.Name] = true;
        }

        foreach (var cfgTranslation in CfgTranslation.Instance.All())
        {
          var id = cfgTranslation.id;
          translation_dict[id] = new Dictionary<string, string>();
          foreach (var fieldName in translationAsset_fieldName_dict.Keys)
          {
            translation_dict[id][fieldName] = cfgTranslation.GetFieldValue<string>(fieldName);
          }
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