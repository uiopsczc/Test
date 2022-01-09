namespace CsCat
{
	public partial class DoerAttrSetter
	{
		public void SetObjectValue(Doer doer, string key, object objectValue, bool isAdd)
		{
			if (SetObjectValue_User(doer, key, objectValue, isAdd))
				return;

			if (SetObjectValue_Doer(doer, key, objectValue, isAdd))
				return;

			if (StringConst.String_nil.Equals(objectValue))
			{
				doer.Remove<object>(key);
				return;
			}

			if (!isAdd)
			{
				doer.Set(key, objectValue);
				return;
			}

			// add
			if (objectValue is int i)
			{
				doer.Add(key, i);
				return;
			}

			if (objectValue is float f)
			{
				doer.Add(key, f);
				return;
			}

			if (objectValue is string s)
			{
				doer.Add(key, s);
				return;
			}
		}

		public void SetObjectTmpValue(Doer doer, string key, object objectValue, bool isAdd)
		{
			if (StringConst.String_nil.Equals(objectValue))
			{
				doer.RemoveTmp<object>(key);
				return;
			}

			if (!isAdd)
			{
				doer.SetTmp(key, objectValue);
				return;
			}

			// add
			if (objectValue is int i)
			{
				doer.AddTmp(key, i);
				return;
			}

			if (objectValue is float f)
			{
				doer.AddTmp(key, f);
				return;
			}

			if (objectValue is string s)
			{
				doer.AddTmp(key, s);
				return;
			}
		}
	}
}