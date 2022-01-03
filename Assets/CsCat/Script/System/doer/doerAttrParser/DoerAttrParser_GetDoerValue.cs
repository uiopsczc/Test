namespace CsCat
{
	public partial class DoerAttrParser
	{
		public string GetDoerValue(Doer doer, string key, string typeString)
		{
			string result;
			if (GetDoerValue_User(doer, key, typeString, out result))
				return result;
			if (GetDoerValue_Mission(doer, key, typeString, out result))
				return result;
			if (GetDoerValue_Doer(doer, key, typeString, out result))
				return result;
			return DoerAttrParserUtil.ConvertValue(doer.Get<object>(key), typeString).ToString();
		}

		public string GetDoerTmpValue(Doer doer, string key, string typeString)
		{
			return ConvertValue(doer.GetTmp<object>(key), typeString);
		}
	}
}