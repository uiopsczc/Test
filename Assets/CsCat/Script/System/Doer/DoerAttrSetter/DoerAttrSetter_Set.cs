namespace CsCat
{
	public partial class DoerAttrSetter
	{
		public void Set(string key, string valueExpression, bool isAdd)
		{
			if (this.doerAttrParser != null && key.IndexOf(CharConst.Char_LeftCurlyBrackets) != -1)
				key = doerAttrParser.ParseString(key);

			object objectValue = doerAttrParser.Parse(valueExpression);
			SetObject(key, objectValue, isAdd);
		}

		public void SetObject(string key, object objectValue, bool isAdd)
		{
			if (key.StartsWith(StringConst.String_u_dot)) //主动对象属性
			{
				key = key.Substring(StringConst.String_u_dot.Length);
				if (u != null)
					SetObjectValue(u, key, objectValue, isAdd);
				return;
			}

			if (key.StartsWith(StringConst.String_ut_dot)) //主动对象临时属性
			{
				key = key.Substring(StringConst.String_ut_dot.Length);
				if (u != null)
					SetObjectTmpValue(u, key, objectValue, isAdd);
				return;
			}

			if (key.StartsWith(StringConst.String_o_dot)) //被动对象属性
			{
				key = key.Substring(StringConst.String_o_dot.Length);
				if (u != null)
					SetObjectValue(o, key, objectValue, isAdd);
				return;
			}

			if (key.StartsWith(StringConst.String_ot_dot)) //被动对象临时属性
			{
				key = key.Substring(StringConst.String_ot_dot.Length);
				if (u != null)
					SetObjectTmpValue(o, key, objectValue, isAdd);
				return;
			}

			if (key.StartsWith(StringConst.String_e_dot)) //中间对象属性
			{
				key = key.Substring(StringConst.String_e_dot.Length);
				if (u != null)
					SetObjectValue(e, key, objectValue, isAdd);
				return;
			}

			if (key.StartsWith(StringConst.String_et_dot)) //中间对象临时属性
			{
				key = key.Substring(StringConst.String_et_dot.Length);
				if (u != null)
					SetObjectTmpValue(e, key, objectValue, isAdd);
				return;
			}


			if (key.StartsWith(StringConst.String_m_dot)) // 当前或中间对象
			{
				key = key.Substring(StringConst.String_m_dot.Length);
				if (m != null)
				{
					if (objectValue is int)
					{
						if (isAdd)
							m[key] = m.Get<int>(key) + objectValue.To<int>();
						else
							m[key] = objectValue.To<int>();
					}

					if (objectValue is float)
					{
						if (isAdd)
							m[key] = m.Get<float>(key) + objectValue.To<float>();
						else
							m[key] = objectValue.To<float>();
					}

					if (objectValue is bool)
						m[key] = objectValue.To<bool>();
					if (objectValue is string)
					{
						if (objectValue.Equals(StringConst.String_nil))
							m.Remove(key);
						else if (isAdd)
							m[key] = m.GetOrAddDefault2(key, () => StringConst.String_Empty) + objectValue.To<string>();
						else
							m[key] = objectValue;
					}
				}

				return;
			}

			if (u != null)
				SetObject(StringConst.String_u_dot + key, objectValue, isAdd);
			else if (o != null)
				SetObject(StringConst.String_o_dot + key, objectValue, isAdd);
			else if (e != null)
				SetObject(StringConst.String_e_dot + key, objectValue, isAdd);
			else if (m != null)
				SetObject(StringConst.String_m_dot + key, objectValue, isAdd);
		}
	}
}