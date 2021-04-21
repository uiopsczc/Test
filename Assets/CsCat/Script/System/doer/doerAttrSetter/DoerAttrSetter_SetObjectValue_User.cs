namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public bool SetObjectValue_User(Doer doer, string key, object object_value, bool is_add)
    {
      bool is_break = false;
      if (doer is User)
      {
        User user = doer as User;
        if (object_value is string)
        {
          string value = (string)object_value;

          if (this.SetObjectValue_User_AddAttrEquip(user, key, value, is_add))
            return true;

          if (this.SetObjectValue_User_Missions(user, key, value, is_add))
            return true;

          if (this.SetObjectValue_User_Items(user, key, value, is_add))
            return true;
        }
      }

      return is_break;
    }



  }
}