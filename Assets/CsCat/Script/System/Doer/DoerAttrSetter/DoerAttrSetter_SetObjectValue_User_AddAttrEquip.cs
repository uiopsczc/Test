using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public partial class DoerAttrSetter
	{
		public bool SetObjectValue_User_AddAttrEquip(User user, string key, string objectValue, bool isAdd)
		{
			bool isBreak = false;
			if (key.StartsWith(StringConst.String_add_attr_equip_dot)) //增加带属性道具
			{
				string itemId = key.Substring(StringConst.String_add_attr_equip_dot.Length).Trim();
				int pos = objectValue.IndexOf(CharConst.Char_Semicolon); // count;pz:2,jl:3
				if (pos != -1)
				{
					string countString = objectValue.Substring(0, pos).Trim();
					int countValue = doerAttrParser.ParseInt(countString, 0);
					Dictionary<string, string> attrs = objectValue.Substring(pos + 1).Trim()
						.ToDictionary<string, string>();
					Hashtable attrDict = new Hashtable();
					foreach (string attrKey in attrs.Keys)
					{
						string attrValue = attrs[attrKey];
						string attrKeyPostParse = this.doerAttrParser.ParseString(attrKey);
						object attrValuePostParse = this.doerAttrParser.Parse(attrValue);
						attrDict[attrKeyPostParse] = attrValuePostParse;
					}

					for (int i = 0; i < countValue; i++)
					{
						Item equip = Client.instance.itemFactory.NewDoer(itemId) as Item;
						equip.AddAll(attrDict);
						user.AddItem(equip);
					}
				}

				return true;
			}

			return isBreak;
		}
	}
}