using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class ItemBag
  {
    private Doer parent_doer;
    private string sub_doer_key;

    public ItemBag(Doer parent_doer, string sub_doer_key)
    {
      this.parent_doer = parent_doer;
      this.sub_doer_key = sub_doer_key;
    }

    ////////////////////DoXXX/////////////////////////////////
    //卸载
    public void DoRelease()
    {
      SubDoerUtil2.DoReleaseSubDoer<Item>(this.parent_doer, this.sub_doer_key);
    }

    //保存
    public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
    {
      save_key = save_key ?? "item_bag";
      var items = this.GetItems();
      var dict_items = new Hashtable();
      var dict_items_tmp = new Hashtable();
      foreach (var item in items)
      {
        var id = item.GetId();
        var rid = item.GetRid();
        var can_fold = item.CanFold();
        if (can_fold) //可折叠
        {
          dict_items[id] = item.GetCount();
        }
        else
        {
          var dict_item_list = dict_items.GetOrAddDefault(id, () => new ArrayList());
          var dict_item = new Hashtable();
          var dict_item_tmp = new Hashtable();
          item.PrepareSave(dict_item, dict_item_tmp);
          dict_item["rid"] = rid;
          dict_item_list.Add(dict_item);
          if (!dict_item_tmp.IsNullOrEmpty())
            dict_items_tmp[rid] = dict_item_tmp;
        }
      }

      if (!dict_items.IsNullOrEmpty())
        dict[save_key] = dict_items;
      if (!dict_items_tmp.IsNullOrEmpty())
        dict_tmp[save_key] = dict_items_tmp;
    }

    //还原
    public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
    {
      restore_key = restore_key ?? "item_bag";
      this.ClearItems();
      var dict_items = dict.Remove2<Hashtable>(restore_key);
      var dict_items_tmp = dict_tmp?.Remove2<Hashtable>(restore_key);
      if (!dict_items.IsNullOrEmpty())
      {
        Item item;
        foreach (var key in dict_items.Keys)
        {
          var id = key as string;
          var value = dict_items[id];
          var items = this.GetItems_ToEdit(id);
          if (value is double) //id情况，可折叠的item
          {
            var count = int.Parse(value.ToString());
            item = Client.instance.itemFactory.NewDoer(id) as Item;
            item.SetEnv(this.parent_doer);
            item.SetCount(count);
            items.Add(item);
          }
          else //不可折叠的情况
          {
            var dict_item_list = value as ArrayList;
            foreach (var _dict_item in dict_item_list)
            {
              var dict_item = _dict_item as Hashtable;
              var rid = dict_item.Remove2<string>("rid");
              item = Client.instance.itemFactory.NewDoer(id) as Item;
              item.SetEnv(this.parent_doer);
              Hashtable dict_item_tmp = null;
              if (dict_items_tmp != null && dict_items_tmp.ContainsKey(rid))
                dict_item_tmp = dict_items_tmp[rid] as Hashtable;
              item.FinishRestore(dict_item, dict_item_tmp);
              items.Add(item);
            }
          }
        }
      }
    }
    ///////////////////////////////OnXXX/////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////
    public bool __FilterType(Item equip, string type_1, string type_2 = null)
    {
      if (equip.GetType1() == type_1 && (type_2 == null || type_2.Equals(equip.GetType2())))
        return true;
      else
        return false;
    }


    public Item[] GetItems(string id = null)
    {
      return SubDoerUtil2.GetSubDoers<Item>(this.parent_doer, this.sub_doer_key, id, null);
    }

    public ArrayList GetItems_ToEdit(string id) //可以直接插入删除
    {
      return SubDoerUtil2.GetSubDoers_ToEdit(this.parent_doer, this.sub_doer_key, id);
    }

    //获得指定的镶物
    public Item GetItem(string id_or_rid)
    {
      return SubDoerUtil2.GetSubDoer<Item>(this.parent_doer, this.sub_doer_key, id_or_rid);
    }


    public Item[] GetItemsOfTypes(string type_1, string type_2 = null)
    {
      return SubDoerUtil2.GetSubDoers<Item>(this.parent_doer, this.sub_doer_key, null,
        (item) => this.__FilterType(item, type_1, type_2));
    }


    public string[] GetItemIds()
    {
      return SubDoerUtil2.GetSubDoerIds(this.parent_doer, this.sub_doer_key);
    }

    public int GetItemCount(string id)
    {
      return SubDoerUtil2.GetSubDoerCount<Item>(this.parent_doer, this.sub_doer_key, id);
    }

    public bool HasItem(string id)
    {
      return SubDoerUtil2.HasSubDoers<Item>(this.parent_doer, this.sub_doer_key, id);
    }

    // 放入物品
    // 对于可折叠物品则会替代已存在的物品对象并数量叠加
    // 对于不可折叠物品则直接加入到对象列表
    public List<Item> AddItems(string id, int count)
    {
      var cfgItemData = CfgItem.Instance.get_by_id(id);
      var can_fold = cfgItemData.can_fold;
      Item item = null;
      List<Item> result = new List<Item>();
      if (can_fold)
      {
        item = Client.instance.itemFactory.NewDoer(id) as Item;
        item.SetCount(count);
        this.AddItem(item);
        result.Add(item);
      }
      else
      {
        for (int i = 0; i < count; i++)
        {
          item = Client.instance.itemFactory.NewDoer(id) as Item;
          this.AddItem(item);
          result.Add(item);
        }
      }

      return result;
    }

    public void AddItem(Item item)
    {
      SubDoerUtil2.AddSubDoers(this.parent_doer, this.sub_doer_key, item);
    }

    public Item[] RemoveItems(string id, int count)
    {
      return SubDoerUtil2.RemoveSubDoers<Item>(this.parent_doer, this.sub_doer_key, id, count,
        Client.instance.itemFactory);
    }

    public bool CanRemoveItems(string id, int count)
    {
      return SubDoerUtil2.CanRemoveSubDoers(this.parent_doer, this.sub_doer_key, id, count);
    }

    public Item RemoveItem(string rid)
    {
      return SubDoerUtil2.RemoveSubDoer<Item>(this.parent_doer, this.sub_doer_key, rid);
    }

    public Item RemoveItem(Item item)
    {
      return SubDoerUtil2.RemoveSubDoer<Item>(this.parent_doer, this.sub_doer_key, item);
    }

    public void ClearItems()
    {
      SubDoerUtil2.ClearSubDoers<Scene>(this.parent_doer, this.sub_doer_key);
    }
  }
}