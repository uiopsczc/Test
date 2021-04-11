namespace CsCat
{
  public partial class DoerAttrParser
  {
    private RandomManager _randomManager;

    private RandomManager randomManager
    {
      get { return this._randomManager == null ? Client.instance.randomManager : _randomManager; }
    }

    public bool ParseBoolean(string expression, bool dv = default(bool))
    {
      return ParseString(expression).ToBoolOrToDefault(dv);
    }

    public float ParseFloat(string expression, float dv = default(float))
    {
      return ParseString(expression).ToFloatOrToDefault(dv);
    }

    public int ParseInt(string expression, int dv = default(int))
    {
      return ParseString(expression).ToIntOrToDefault(dv);
    }

    public long ParseLong(string expression, long dv = default(long))
    {
      return ParseString(expression).ToLongOrToDefault(dv);
    }

    public string ParseString(string expression)
    {
      LogCat.log(string.Format("解析ing:{0}", expression));
      //决定解析的先后顺序
      if (expression.IndexOf("${") != -1)
      {
        if (expression.IndexOf("$${") != -1)
          expression = expression.ReplaceAll(DoerAttrParserConst.Pattern3, Replace);
        expression = expression.ReplaceAll(DoerAttrParserConst.Pattern2, Replace);
      }

      return expression.ReplaceAll(DoerAttrParserConst.Pattern1, Replace);
    }

    public string Replace(string expression)
    {
      if (expression.StartsWith("{"))
        expression = expression.Substring("{".Length, expression.Length - "{".Length - 1).Trim();
      else if (expression.StartsWith("${"))
        expression = expression.Substring("${".Length, expression.Length - "${".Length - 1).Trim();
      else if (expression.StartsWith("$${"))
        expression = expression.Substring("$${".Length, expression.Length - "$${".Length - 1).Trim();
      //type_string用于定位变量的类型，用于获取hatable.Get<T>中的值
      string type_string = "";
      if (expression.StartsWith(DoerAttrParserConst.Type_String_List[1]))
      {
        type_string = DoerAttrParserConst.Type_String_List[1];
        expression = expression.Substring(type_string.Length).Trim();
      }
      else if (expression.StartsWith(DoerAttrParserConst.Type_String_List[2]))
      {
        type_string = DoerAttrParserConst.Type_String_List[2];
        expression = expression.Substring(type_string.Length).Trim();
      }
      else if (expression.StartsWith(DoerAttrParserConst.Type_String_List[3]))
      {
        type_string = DoerAttrParserConst.Type_String_List[3];
        expression = expression.Substring(DoerAttrParserConst.Type_String_List[3].Length).Trim();
      }

      if (expression.StartsWith("u.")) // 主动对象属性
      {
        expression = expression.Substring("u.".Length);
        if (u != null)
          return GetDoerValue(u, expression, type_string);
        else
          return ConvertValue("", type_string);
      }

      if (expression.StartsWith("ut.")) // 主动对象临时属性
      {
        expression = expression.Substring("ut.".Length);
        if (u != null)
          return GetDoerTmpValue(u, expression, type_string);
        else
          return ConvertValue("", type_string);
      }


      if (expression.StartsWith("o.")) // 被动对象属性
      {
        expression = expression.Substring("o.".Length);
        if (o != null)
          return GetDoerValue(o, expression, type_string);
        else
          return ConvertValue("", type_string);
      }

      if (expression.StartsWith("ot.")) // 被动对象临时属性
      {
        expression = expression.Substring("ot.".Length);
        if (o != null)
          return GetDoerTmpValue(o, expression, type_string);
        else
          return ConvertValue("", type_string);
      }

      if (expression.StartsWith("e.")) // 中间对象属性
      {
        expression = expression.Substring("e.".Length);
        if (e != null)
          return GetDoerValue(e, expression, type_string);
        return ConvertValue("", type_string);
      }

      if (expression.StartsWith("et.")) // 中间对象临时属性
      {
        expression = expression.Substring("et.".Length);
        if (e != null)
          return GetDoerTmpValue(e, expression, type_string);
        else
          return ConvertValue("", type_string);
      }

      if (expression.StartsWith("m.")) // 当前或中间对象
      {
        expression = expression.Substring("m.".Length);
        if (m != null)
          return ConvertValue(m.Get<object>(expression), type_string);
        else
          return ConvertValue(type_string, "");
      }

      if (expression.StartsWith("definition.")) // 定义数据
      {
        expression = expression.Substring("definition.".Length);
        int pos0 = expression.IndexOf('.');
        string definition_name = expression.Substring(0, pos0);
        expression = expression.Substring(pos0 + 1);
        int pos1 = expression.IndexOf('.');
        string id = expression.Substring(0, pos1);
        string attr = expression.Substring(pos1 + 1);
        ExcelAssetBase definition = null;
        if (definition_name.EqualsIgnoreCase("ItemDefinition"))
          definition = Client.instance.itemFactory.GetDefinition(id);
        return ConvertValue(definition.GetFieldValue(attr), type_string);
      }

      if (expression.StartsWith("eval(")) // 求表达式值
      {
        expression = expression.Substring("eval(".Length).Trim();
        int pos = expression.QuoteEndIndex("(", ")");
        if (pos != -1)
        {
          string exp = expression.Substring(0, pos).Trim();
          string end = pos == exp.Length - 1 ? "" : expression.Substring(pos + 1).Trim();
          string v = Parse(exp) + end; // 计算结果
          return ConvertValue(v, type_string);
        }

        return ConvertValue("", type_string);
      }

      if (expression.StartsWith("hasSubString(")) // 是否有子字符串查找
      {
        expression = expression.Substring("hasSubString(".Length);
        if (expression.EndsWith(")"))
          expression = expression.Substring(0, expression.Length - 1);
        int pos = expression.LastIndexOf('|');
        if (pos == -1)
          pos = expression.LastIndexOf(',');
        if (pos != -1)
        {
          string src = expression.Substring(0, pos);
          string dst = expression.Substring(pos + 1).Trim();
          bool v = (src.IndexOf(dst) != -1);
          return ConvertValue(v, type_string);
        }

        return ConvertValue("", type_string);
      }

      if (expression.StartsWith("random(")) // 随机数
      {
        expression = expression.Substring("random(".Length);
        int pos0 = expression.QuoteEndIndex("(", ")");
        string random_expression = expression.Substring(0, pos0).Trim();
        string end = pos0 == expression.Length - 1 ? "" : expression.Substring(pos0 + 1).Trim();
        int pos1 = random_expression.IndexOf(",");
        int random_arg0 = random_expression.Substring(0, pos1).To<int>();
        int random_arg1 = random_expression.Substring(pos1 + 1).To<int>();
        return ConvertValue(randomManager.RandomInt(random_arg0, random_arg1) + end, type_string);
      }


      //默认的处理
      if (m != null)
        return ConvertValue(m.Get<object>(expression), type_string);
      if (u != null)
        return ConvertValue(u.Get<object>(expression), type_string);
      if (o != null)
        return ConvertValue(o.Get<object>(expression), type_string);
      return ConvertValue("", type_string);
    }





    public string ConvertValue(object value, string type_string)
    {
      return DoerAttrParserUtil.ConvertValue(value, type_string).ToString();
    }

  }
}
