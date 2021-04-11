namespace CsCat
{
  public partial class DoerAttrParser
  {
    public bool GetDoerValue_User(Doer doer, string key, string type_string, out string result)
    {
      bool is_break = false;
      result = null;
      if (doer is User)
      {
        User user = doer as User;
        if (GetDoerValue_User_Missions(user, key, type_string, out result))
          return true;
        if (GetDoerValue_User_Items(user, key, type_string, out result))
          return true;
      }

      return is_break;
    }
  }
}