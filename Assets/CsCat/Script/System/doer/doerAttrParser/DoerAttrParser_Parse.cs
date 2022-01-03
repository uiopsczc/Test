namespace CsCat
{
	public partial class DoerAttrParser
	{
		private RandomManager _randomManager;

		private RandomManager randomManager => _randomManager ?? Client.instance.randomManager;

		public bool ParseBoolean(string expression, bool dv = default)
		{
			return ParseString(expression).ToBoolOrToDefault(dv);
		}

		public float ParseFloat(string expression, float dv = default)
		{
			return ParseString(expression).ToFloatOrToDefault(dv);
		}

		public int ParseInt(string expression, int dv = default)
		{
			return ParseString(expression).ToIntOrToDefault(dv);
		}

		public long ParseLong(string expression, long dv = default)
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
			if (expression.StartsWith(StringConst.String_LeftCurlyBrackets))
				expression = expression.Substring(StringConst.String_LeftCurlyBrackets.Length, expression.Length - StringConst.String_LeftCurlyBrackets.Length - 1).Trim();
			else if (expression.StartsWith("${"))
				expression = expression.Substring("${".Length, expression.Length - "${".Length - 1).Trim();
			else if (expression.StartsWith("$${"))
				expression = expression.Substring("$${".Length, expression.Length - "$${".Length - 1).Trim();
			//type_string用于定位变量的类型，用于获取hatable.Get<T>中的值
			string typeString = StringConst.String_Empty;
			if (expression.StartsWith(DoerAttrParserConst.Type_String_List[1]))
			{
				typeString = DoerAttrParserConst.Type_String_List[1];
				expression = expression.Substring(typeString.Length).Trim();
			}
			else if (expression.StartsWith(DoerAttrParserConst.Type_String_List[2]))
			{
				typeString = DoerAttrParserConst.Type_String_List[2];
				expression = expression.Substring(typeString.Length).Trim();
			}
			else if (expression.StartsWith(DoerAttrParserConst.Type_String_List[3]))
			{
				typeString = DoerAttrParserConst.Type_String_List[3];
				expression = expression.Substring(DoerAttrParserConst.Type_String_List[3].Length).Trim();
			}

			if (expression.StartsWith(StringConst.String_u_dot)) // 主动对象属性
			{
				expression = expression.Substring(StringConst.String_u_dot.Length);
				if (u != null)
					return GetDoerValue(u, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith(StringConst.String_ut_dot)) // 主动对象临时属性
			{
				expression = expression.Substring(StringConst.String_ut_dot.Length);
				if (u != null)
					return GetDoerTmpValue(u, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}


			if (expression.StartsWith(StringConst.String_o_dot)) // 被动对象属性
			{
				expression = expression.Substring(StringConst.String_o_dot.Length);
				if (o != null)
					return GetDoerValue(o, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith(StringConst.String_ot_dot)) // 被动对象临时属性
			{
				expression = expression.Substring(StringConst.String_ot_dot.Length);
				if (o != null)
					return GetDoerTmpValue(o, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith(StringConst.String_e_dot)) // 中间对象属性
			{
				expression = expression.Substring(StringConst.String_e_dot.Length);
				if (e != null)
					return GetDoerValue(e, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith(StringConst.String_et_dot)) // 中间对象临时属性
			{
				expression = expression.Substring(StringConst.String_et_dot.Length);
				if (e != null)
					return GetDoerTmpValue(e, expression, typeString);
				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith(StringConst.String_m_dot)) // 当前或中间对象
			{
				expression = expression.Substring(StringConst.String_m_dot.Length);
				if (m != null)
					return ConvertValue(m.Get<object>(expression), typeString);
				return ConvertValue(typeString, StringConst.String_Empty);
			}

			if (expression.StartsWith("cfgData.")) // 定义数据
			{
				expression = expression.Substring("cfgData.".Length);
				int pos0 = expression.IndexOf(CharConst.Char_Dot);
				string cfgDataName = expression.Substring(0, pos0);
				expression = expression.Substring(pos0 + 1);
				int pos1 = expression.IndexOf(CharConst.Char_Dot);
				string id = expression.Substring(0, pos1);
				string attr = expression.Substring(pos1 + 1);
				if (cfgDataName.EqualsIgnoreCase("cfgItemData"))
					return ConvertValue(Client.instance.itemFactory.GetCfgItemData(id).GetFieldValue(attr),
						typeString);
				return null;
			}

			if (expression.StartsWith("eval(")) // 求表达式值
			{
				expression = expression.Substring("eval(".Length).Trim();
				int pos = expression.WrapEndIndex(StringConst.String_LeftRoundBrackets, StringConst.String_RightRoundBrackets);
				if (pos != -1)
				{
					string exp = expression.Substring(0, pos).Trim();
					string end = pos == exp.Length - 1 ? StringConst.String_Empty : expression.Substring(pos + 1).Trim();
					string v = Parse(exp) + end; // 计算结果
					return ConvertValue(v, typeString);
				}

				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith("hasSubString(")) // 是否有子字符串查找
			{
				expression = expression.Substring("hasSubString(".Length);
				if (expression.EndsWith(StringConst.String_RightRoundBrackets))
					expression = expression.Substring(0, expression.Length - 1);
				int pos = expression.LastIndexOf(CharConst.Char_Vertical);
				if (pos == -1)
					pos = expression.LastIndexOf(CharConst.Char_Comma);
				if (pos != -1)
				{
					string src = expression.Substring(0, pos);
					string dst = expression.Substring(pos + 1).Trim();
					bool v = (src.IndexOf(dst) != -1);
					return ConvertValue(v, typeString);
				}

				return ConvertValue(StringConst.String_Empty, typeString);
			}

			if (expression.StartsWith("random(")) // 随机数
			{
				expression = expression.Substring("random(".Length);
				int pos0 = expression.WrapEndIndex(StringConst.String_LeftRoundBrackets, StringConst.String_RightRoundBrackets);
				string randomExpression = expression.Substring(0, pos0).Trim();
				string end = pos0 == expression.Length - 1 ? StringConst.String_Empty : expression.Substring(pos0 + 1).Trim();
				int pos1 = randomExpression.IndexOf(CharConst.Char_Comma);
				int randomArg0 = randomExpression.Substring(0, pos1).To<int>();
				int randomArg1 = randomExpression.Substring(pos1 + 1).To<int>();
				return ConvertValue(randomManager.RandomInt(randomArg0, randomArg1) + end, typeString);
			}


			//默认的处理
			if (m != null)
				return ConvertValue(m.Get<object>(expression), typeString);
			if (u != null)
				return ConvertValue(u.Get<object>(expression), typeString);
			if (o != null)
				return ConvertValue(o.Get<object>(expression), typeString);
			return ConvertValue(StringConst.String_Empty, typeString);
		}


		public string ConvertValue(object value, string typeString)
		{
			return DoerAttrParserUtil.ConvertValue(value, typeString).ToString();
		}
	}
}