using System.Collections;
using LitJson;

namespace CsCat
{
  public static class IsToString2
  {
    /// <summary>
    ///   用于Object的ToString2，有ToString2的类必须在这里添加对应的处理
    /// </summary>
    public static string ToString2(object o, bool is_fill_string_with_double_quote = false)
    {
      if (o is LitJson.JsonData jsonData)
        return jsonData.ToJsonWithUTF8();
      if (o is ICollection)
        return ((ICollection)o).ToString2(is_fill_string_with_double_quote);
      if (o is IToString2)
        return ((IToString2)o).ToString2(is_fill_string_with_double_quote);
      var resulut = o.ToString();
      if (o is string && is_fill_string_with_double_quote) resulut = resulut.QuoteWithDouble();
      return resulut;
    }
  }
}