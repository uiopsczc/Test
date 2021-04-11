namespace CsCat
{
  public partial class DoerAttrParser
  {
    public bool GetDoerValue_User_Items(User user, string key, string type_string, out string result)
    {
      bool is_break = false;
      result = null;
      if (key.StartsWith("items.")) //物品对象
      {
        key = key.Substring("items.".Length);
        int pos = key.IndexOf(".");
        if (pos != -1)
        {
          string item_id = key.Substring(0, pos);
          key = key.Substring(pos);
          if (item_id.EndsWith("t"))
          {
            item_id = item_id.Substring(0, item_id.Length - 1);
            key = "t" + key;
          }

          if (key.Equals(".count"))
          {
            result = ConvertValue(user.GetItemCount(item_id), type_string);
            return true;
          }

          Item item = user.GetItem(item_id);
          if (item != null) //身上有这个物品
          {
            var doerAttrParser = new DoerAttrParser(item);
            result = doerAttrParser.ParseString(type_string + "u" + key);
            return true;
          }
        }

        result = ConvertValue("", type_string);
        return true;
      }

      return is_break;
    }
  }
}