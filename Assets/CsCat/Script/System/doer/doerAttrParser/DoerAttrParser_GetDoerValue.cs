namespace CsCat
{
  public partial class DoerAttrParser
  {
    public string GetDoerValue(Doer doer, string key, string type_string)
    {
      string result;
      if (GetDoerValue_User(doer, key, type_string, out result))
        return result;
      if (GetDoerValue_Mission(doer, key, type_string, out result))
        return result;
      if (GetDoerValue_Doer(doer, key, type_string, out result))
        return result;
      return DoerAttrParserUtil.ConvertValue(doer.Get<object>(key), type_string).ToString();
    }

    public string GetDoerTmpValue(Doer doer, string key, string type_string)
    {
      return ConvertValue(doer.GetTmp<object>(key), type_string);
    }
  }
}