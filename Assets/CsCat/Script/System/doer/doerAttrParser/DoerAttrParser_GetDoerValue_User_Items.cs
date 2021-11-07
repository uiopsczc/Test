namespace CsCat
{
    public partial class DoerAttrParser
    {
        public bool GetDoerValue_User_Items(User user, string key, string typeString, out string result)
        {
            bool isBreak = false;
            result = null;
            if (key.StartsWith(StringConst.String_items_dot)) //物品对象
            {
                key = key.Substring(StringConst.String_items_dot.Length);
                int pos = key.IndexOf(CharConst.Char_Dot);
                if (pos != -1)
                {
                    string itemId = key.Substring(0, pos);
                    key = key.Substring(pos);
                    if (itemId.EndsWith(StringConst.String_t))
                    {
                        itemId = itemId.Substring(0, itemId.Length - 1);
                        key = StringConst.String_t + key;
                    }

                    if (key.Equals(StringConst.String_dot_count))
                    {
                        result = ConvertValue(user.GetItemCount(itemId), typeString);
                        return true;
                    }

                    Item item = user.GetItem(itemId);
                    if (item != null) //身上有这个物品
                    {
                        var doerAttrParser = new DoerAttrParser(item);
                        result = doerAttrParser.ParseString(typeString + StringConst.String_u + key);
                        return true;
                    }
                }

                result = ConvertValue(StringConst.String_Empty, typeString);
                return true;
            }

            return isBreak;
        }
    }
}