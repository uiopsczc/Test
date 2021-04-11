using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public bool SetObjectValue_User_AddAttrEquip(User user, string key, string object_value, bool is_add)
    {
      bool is_break = false;
      if (key.StartsWith("add_attr_equip.")) //增加带属性道具
      {
        string item_id = key.Substring("add_attr_equip.".Length).Trim();
        int pos = object_value.IndexOf(';'); // count;pz:2,jl:3
        if (pos != -1)
        {
          string count_string = object_value.Substring(0, pos).Trim();
          int count_value = doerAttrParser.ParseInt(count_string, 0);
          Dictionary<string, string> attrs = object_value.Substring(pos + 1).Trim().ToDictionary<string, string>();
          Hashtable attr_dict = new Hashtable();
          foreach (string attr_key in attrs.Keys)
          {
            string attr_value = attrs[attr_key];
            string attr_key_post_parse = this.doerAttrParser.ParseString(attr_key);
            object attr_value_post_parse = this.doerAttrParser.Parse(attr_value);
            attr_dict[attr_key_post_parse] = attr_value_post_parse;
          }

          for (int i = 0; i < count_value; i++)
          {
            Item equip = Client.instance.itemFactory.NewDoer(item_id) as Item;
            equip.AddAll(attr_dict);
            user.AddItem(equip);
          }
        }

        return true;
      }

      return is_break;
    }



  }
}