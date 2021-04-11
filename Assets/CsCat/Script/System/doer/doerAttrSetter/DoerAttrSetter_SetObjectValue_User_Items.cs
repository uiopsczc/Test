namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public bool SetObjectValue_User_Items(User user, string key, string object_value, bool is_add)
    {
      bool is_break = false;
      if (key.StartsWith("items.")) //物品对象
      {
        key = key.Substring("items.".Length);
        int pos = key.IndexOf(".");
        if (pos != -1)
        {
          string item_id = key.Substring(0, pos);
          key = key.Substring(pos);
          if (item_id.EndsWith("t")) //改变tmpValue
          {
            item_id = item_id.Substring(0, item_id.Length - 1);
            key = "t" + key;
          }

          var item = user.GetItem(item_id);
          if (item != null)
          {
            DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc);
            doerAttrSetter.SetU(item);
            doerAttrSetter.SetObject("u" + key, object_value, is_add);
          }
        }

        return true;
      }

      return is_break;
    }



  }
}