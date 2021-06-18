using System;
using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public partial class User
  {
    ////////////////////////////背包////////////////////////
    public Item[] GetItems(string id = null)
    {
      return this.o_item_bag.GetItems(id);
    }


    public Item GetItem(string id_or_rid)
    {
      return this.o_item_bag.GetItem(id_or_rid);
    }


    public Item[] GetItemsOfTypes(string type_1, string type_2 = null)
    {
      return this.o_item_bag.GetItemsOfTypes(type_1, type_2);
    }

    public string[] GetItemIds()
    {
      return this.o_item_bag.GetItemIds();
    }


    public int GetItemCount(string id)
    {
      return this.o_item_bag.GetItemCount(id);
    }

    public bool HasItem(string id_or_rid)
    {
      return this.o_item_bag.HasItem(id_or_rid);
    }

    // 放入物品
    // 对于可折叠物品则会替代已存在的物品对象并数量叠加
    // 对于不可折叠物品则直接加入到对象列表
    public void AddItems(string id, int count)
    {
      var items = this.o_item_bag.AddItems(id, count);
      foreach (var item in items)
      {
        this.OnAddItem(item);
      }
    }

    public Item[] RemoveItems(string id, int count)
    {
      var items = this.o_item_bag.RemoveItems(id, count);
      foreach (var item in items)
      {
        this.OnRemoveItem(item);
      }

      return items;
    }

    public bool TryRemoveItems(string id, int count)
    {
      if (this.CanRemoveItems(id, count))
      {
        this.RemoveItems(id, count);
        return true;
      }
      else
        return false;
    }

    public void AddItem(Item item)
    {
      this.o_item_bag.AddItem(item);
      this.OnAddItem(item);
    }

    public Item RemoveItem(Item item)
    {
      var result = this.o_item_bag.RemoveItem(item);
      if (result != null)
        this.OnRemoveItem(item);
      return result;
    }

    public bool CanRemoveItems(string id, int count)
    {
      return this.o_item_bag.CanRemoveItems(id, count);
    }

    public void ClearItems()
    {
      this.o_item_bag.ClearItems();
    }

    //////////////////////OnXXX/////////////////////////////////////
    public virtual void OnAddItem(Item item)
    {
    }

    public virtual void OnRemoveItem(Item item)
    {
    }

    ///////////////////////Util////////////////////////////////
    public bool UseItem(string id_or_rid, Critter target)
    {
      var item = this.GetItem(id_or_rid);
      if (item == null)
      {
        LogCat.error(string.Format("UseItem error:do not has {0}", id_or_rid));
        return false;
      }

      if (!target.CheckUseItem(item))
        return false;
      if (item.CanFold())
        item = this.RemoveItems(item.GetId(), 1)[0];
      else
        item = this.RemoveItem(item);
      if (!target.UseItem(item))
      {
        //失败，加回去
        this.AddItem(item);
        return false;
      }

      item.Destruct();
      return true;
    }

    //可以增加或者删除物品(count是负数的时候),添加物品的时候可以在数量后面加"xxAttr1:4,xxAttr2:5"添加该物品的附加属性
    public bool DealItems(Dictionary<string, string> item_dict, DoerAttrParser doerAttrParser = null)
    {
      doerAttrParser = doerAttrParser ?? new DoerAttrParser(this);
      foreach (var item_id in item_dict.Keys)
      {
        string value = item_dict[item_id];
        Hashtable add_attr_dict = new Hashtable(); //带属性
        int pos = value.IndexOf("(");
        if (pos != -1)
        {
          string attr_string = value.Substring(pos + 1, value.Length - pos - 2); //最后一个)也要删除
          value = value.Substring(0, pos);

          add_attr_dict = attr_string.ToDictionary<string, string>().ToHashtable();

        }

        int count = doerAttrParser.ParseInt(value);
        if (count < 0) //remove Items
        {
          count = Math.Abs(count);
          Item[] items = this.RemoveItems(item_id, count);
          foreach (var item in items)
            item.Destruct();
        }
        else //add Items
        {
          Item item = Client.instance.itemFactory.NewDoer(item_id) as Item;
          for (int i = 0; i < count; i++)
          {
            if (!add_attr_dict.IsNullOrEmpty())
            {
              foreach (string attr_name in new ArrayList(add_attr_dict.Keys))
                add_attr_dict[attr_name] = doerAttrParser.Parse(add_attr_dict[attr_name] as string);
              item.AddAll(add_attr_dict);
            }

            bool can_fold = item.CanFold();
            if (can_fold)
            {
              item.SetCount(count);
              this.AddItem(item);
              break;
            }
            else
              this.AddItem(item);
          }
        }
      }

      return true;
    }


    /////////////////////////////////////////装备/////////////////////////////////
    public bool PutOnEquip(string id_or_rid, Critter target)
    {
      var item = this.GetItem(id_or_rid);
      if (item == null)
        return false;
      string type_1 = item.GetType1();
      string type_2 = item.GetType2();
      var old_equip = target.GetEquipOfTypes(type_1, type_2);
      if (old_equip != null)
      {
        if (!this.TakeOffEquip(old_equip, target))
          return false;
      }

      if (!target.CheckPutOnEquip(item))
        return false;
      if (item.CanFold())
        item = this.RemoveItems(item.GetId(), 1)[0];
      else
        item = this.RemoveItem(item);
      if (item == null)
      {
        LogCat.error(string.Format("PutOnEquip error:{0} do not has item:{1}", this, id_or_rid));
        return false;
      }


      if (!target.PutOnEquip(item))
      {
        //失败，加回去
        this.AddItem(item);
        return false;
      }

      item.Destruct();
      return true;
    }

    public bool TakeOffEquip(Item equip, Critter target)
    {
      if (equip == null)
      {
        LogCat.error(string.Format("TakeOffEquip error: equip is null"));
        return false;
      }

      if (!target.CheckTakeOffEquip(equip))
        return false;

      if (!target.TakeOffEquip(equip))
        return false;
      equip.SetEnv(this);
      this.AddItem(equip);
      return true;
    }


    /////////////////////////////////////////镶嵌物/////////////////////////////////
    public bool EmbedOn(Item item, Item embed)
    {
      if (item == null)
      {
        LogCat.error(string.Format("EmbedOn error: item is null"));
        return false;
      }

      if (embed == null)
      {
        LogCat.error(string.Format("EmbedOn error: embed is null"));
        return false;
      }

      if (!item.CheckEmbedOn(embed))
        return false;
      if (this.RemoveItem(embed) == null)
      {
        LogCat.error("EmbedOn error:can not remove item:{0}", item);
        return false;
      }

      if (!item.EmbedOn(embed))
      {
        //失败，加回去
        this.AddItem(embed);
        return false;
      }

      return true;
    }

    public bool EmbedOn(Item item, string embed_id_or_rid)
    {
      var embed = this.GetItem(embed_id_or_rid);
      return EmbedOn(item, embed);
    }

    public bool EmbedOn(string item_id_or_rid, Item embed)
    {
      var item = this.GetItem(item_id_or_rid);
      return EmbedOn(item, embed);
    }

    public bool EmbedOn(string item_id_or_rid, string embed_id_or_rid)
    {
      var item = this.GetItem(item_id_or_rid);
      var embed = this.GetItem(embed_id_or_rid);
      return EmbedOn(item, embed);
    }


    public bool EmbedOff(Item item, Item embed)
    {
      if (item == null)
      {
        LogCat.error(string.Format("EmbedOff error: item is null"));
        return false;
      }

      if (embed == null)
      {
        LogCat.error(string.Format("EmbedOff error: embed is null"));
        return false;
      }

      if (!item.CheckEmbedOff(embed))
        return false;
      if (!item.EmbedOff(embed))
        return false;
      embed.SetEnv(this);
      this.AddItem(embed);
      return true;
    }

    public bool EmbedOff(Item item, string embed_id_or_rid)
    {
      var embed = item.GetEmbed(embed_id_or_rid);
      return EmbedOff(item, embed);
    }

    public bool EmbedOff(string item_id_or_rid, Item embed)
    {
      var item = this.GetItem(item_id_or_rid);
      return EmbedOff(item, embed);
    }

    public bool EmbedOff(string item_id_or_rid, string embed_id_or_rid)
    {
      var item = this.GetItem(item_id_or_rid);
      var embed = item.GetEmbed(embed_id_or_rid);
      return EmbedOff(item, embed);
    }
  }
}