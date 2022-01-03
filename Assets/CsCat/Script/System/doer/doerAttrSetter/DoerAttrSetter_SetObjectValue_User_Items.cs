namespace CsCat
{
	public partial class DoerAttrSetter
	{
		public bool SetObjectValue_User_Items(User user, string key, string objectValue, bool isAdd)
		{
			bool isBreak = false;
			if (key.StartsWith(StringConst.String_items_dot)) //物品对象
			{
				key = key.Substring(StringConst.String_items_dot.Length);
				int pos = key.IndexOf(CharConst.Char_Dot);
				if (pos != -1)
				{
					string itemId = key.Substring(0, pos);
					key = key.Substring(pos);
					if (itemId.EndsWith(StringConst.String_t)) //改变tmpValue
					{
						itemId = itemId.Substring(0, itemId.Length - 1);
						key = StringConst.String_t + key;
					}

					var item = user.GetItem(itemId);
					if (item != null)
					{
						DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc);
						doerAttrSetter.SetU(item);
						doerAttrSetter.SetObject(StringConst.String_u + key, objectValue, isAdd);
					}
				}

				return true;
			}

			return isBreak;
		}
	}
}