using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
    public static class DoerAttrParserUtil
    {
        public static object ConvertValue(object value, string typeString)
        {
            if (typeString.Equals(DoerAttrParserConst.Type_String_List[0]))
                return value.ToIntOrToDefault();
            if (typeString.Equals(DoerAttrParserConst.Type_String_List[1]))
                return value.ToFloatOrToDefault();
            if (typeString.Equals(DoerAttrParserConst.Type_String_List[2]))
                return value.ToBoolOrToDefault();
            if (typeString.Equals(DoerAttrParserConst.Type_String_List[3]))
                return value.ToStringOrToDefault("");

            LogCat.error(string.Format("没有处理该type_string[{0}]的方法", typeString));
            return null;
        }

        public static string GetTypeString(string value)
        {
            var valueFirstChar = (!value.IsNullOrWhiteSpace() && value.Length > 1)
                ? value[0].ToString()
                : StringConst.String_Empty;
            for (int i = DoerAttrParserConst.Type_String_List.Count - 1; i >= 1; i--)
            {
                if (valueFirstChar.Equals(DoerAttrParserConst.Type_String_List[i]))
                    return DoerAttrParserConst.Type_String_List[i];
            }

            return DoerAttrParserConst.Type_String_List[0];
        }

        public static object ConvertValueWithTypeString(string value)
        {
            string typeString = GetTypeString(value);
            int typeStringLength = typeString.Length;
            string valueWithoutTypeString = typeStringLength == 0 ? value : value.Substring(typeStringLength);
            return ConvertValue(valueWithoutTypeString, typeString);
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