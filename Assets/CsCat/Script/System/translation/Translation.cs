using System.Collections.Generic;

namespace CsCat
{
  // З­вы
  public class Translation
  {
    private static bool is_inited = false;

    public static Dictionary<string, Dictionary<string, string>> translation_dict =
      new Dictionary<string, Dictionary<string, string>>();

    public static string GetText(string to_translate)
    {
      Init();
      if (GameData.instance.translationData.language == null)
        return to_translate;
      if (to_translate == null)
        return to_translate;
      string to_translate_escape = to_translate.Replace("\r\n", "\n").Replace("\r", "\n");
      if (!translation_dict.ContainsKey(to_translate_escape))
        return to_translate;
      if (!translation_dict[to_translate_escape].ContainsKey(GameData.instance.translationData.language))
        return to_translate;
      if (translation_dict[to_translate_escape][GameData.instance.translationData.language].IsNullOrWhiteSpace())
        return to_translate;
      return translation_dict[to_translate_escape][GameData.instance.translationData.language];
    }

    public static void Init()
    {
      if (!is_inited)
      {
        is_inited = true;
        translation_dict.Clear();
        TranslationDefinition translationDefinition = new TranslationDefinition();
        Dictionary<string, bool> translationAsset_fieldName_dict = new Dictionary<string, bool>();

        foreach (var fieldInfo in typeof(TranslationDefinition).GetFields())
        {
          if (fieldInfo.DeclaringType == typeof(TranslationDefinition))
            translationAsset_fieldName_dict[fieldInfo.Name] = true;
        }

        foreach (string id in translationDefinition.GetIdList())
        {
          translation_dict[id] = new Dictionary<string, string>();
          foreach (var fieldName in translationAsset_fieldName_dict.Keys)
          {
            translation_dict[id][fieldName] = translationDefinition.GetData(id).GetFieldValue<string>(fieldName);
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