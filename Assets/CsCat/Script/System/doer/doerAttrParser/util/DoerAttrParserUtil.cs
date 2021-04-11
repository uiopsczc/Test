using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public static class DoerAttrParserUtil
  {
    public static object ConvertValue(object value, string type_string)
    {
      if (type_string.Equals(DoerAttrParserConst.Type_String_List[0]))
        return value.ToIntOrToDefault();
      if (type_string.Equals(DoerAttrParserConst.Type_String_List[1]))
        return value.ToFloatOrToDefault();
      if (type_string.Equals(DoerAttrParserConst.Type_String_List[2]))
        return value.ToBoolOrToDefault();
      if (type_string.Equals(DoerAttrParserConst.Type_String_List[3]))
        return value.ToStringOrToDefault("");

      LogCat.error(string.Format("没有处理该type_string[{0}]的方法", type_string));
      return null;
    }

    public static string GetTypeString(string value)
    {
      var value_first_char = (!value.IsNullOrWhiteSpace() && value.Length > 1) ? value[0].ToString() : "";
      for (int i = DoerAttrParserConst.Type_String_List.Count - 1; i >= 1; i--)
      {
        if (value_first_char.Equals(DoerAttrParserConst.Type_String_List[i]))
          return DoerAttrParserConst.Type_String_List[i];
      }

      return DoerAttrParserConst.Type_String_List[0];
    }

    public static object ConvertValueWithTypeString(string value)
    {
      string type_string = GetTypeString(value);
      int type_string_length = type_string.Length;
      string value_without_type_string = type_string_length == 0 ? value : value.Substring(type_string_length);
      return ConvertValue(value_without_type_string, type_string);
    }

    public static Hashtable ConvertTableWithTypeString(Dictionary<string, string> dict)
    {
      Hashtable result = new Hashtable();
      if (!dict.IsNullOrEmpty())
      {
        foreach (string key in dict.Keys)
        {
          string value = dict[key];
          result[key] = ConvertValueWithTypeString(value);
        }
      }

      return result;
    }

  }
}