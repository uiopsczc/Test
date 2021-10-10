using System;
using System.Collections;

namespace CsCat
{
  public class SceneItems
  {
    private Doer parent_doer;
    private string sub_doer_key;

    public SceneItems(Doer parent_doer, string sub_doer_key)
    {
      this.parent_doer = parent_doer;
      this.sub_doer_key = sub_doer_key;
    }

    ////////////////////DoXXX/////////////////////////////////
    //卸载
    public void DoRelease()
    {
      SubDoerUtil3.DoReleaseSubDoer<Item>(this.parent_doer, this.sub_doer_key);
    }

    //保存
    public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
    {
      save_key = save_key ?? "scene_items";
      var items = this.GetItems();
      var dict_items = new Hashtable();
      var dict_items_tmp = new Hashtable();
      foreach (var item in items)
      {
        var dict_item = new Hashtable();
        var dict_item_tmp = new Hashtable();
        item.PrepareSave(dict_item, dict_item_tmp);
        string rid = item.GetRid();
        dict_items[rid] = dict_item;
        if (!dict_item_tmp.IsNullOrEmpty())
          dict_items_tmp[rid] = dict_item_tmp;
      }

      if (!dict_items.IsNullOrEmpty())
        dict[save_key] = dict_items;
      if (!dict_items_tmp.IsNullOrEmpty())
        dict_tmp[save_key] = dict_items_tmp;
    }

    //还原
    public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
    {
      restore_key = restore_key ?? "scene_items";
      this.ClearItems();
      var dict_items = dict.Remove3<Hashtable>(restore_key);
      var dict_items_tmp = dict_tmp?.Remove3<Hashtable>(restore_key);
      if (!dict_items.IsNullOrEmpty())
      {
        foreach (string rid in dict_items.Keys)
        {
          var item_dict = this.GetItemDict_ToEdit();
          Hashtable dict_item = dict_items[rid] as Hashtable;
          Item item = Client.instance.itemFactory.NewDoer(rid) as Item;
          item.SetEnv(this.parent_doer);
          Hashtable dict_item_tmp = null;
          if (dict_items_tmp != null && dict_items_tmp.ContainsKey(rid))
            dict_item_tmp = dict_items_tmp[rid] as Hashtable;
          item.FinishRestore(dict_item, dict_item_tmp);
          item_dict[rid] = item;
        }
      }
    }
    ///////////////////////////////OnXXX/////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////
    public Item[] GetItems(string id = null, Func<Item, bool> filter_func = null)
    {
      return SubDoerUtil3.GetSubDoers(parent_doer, sub_doer_key, id, filter_func);
    }

    public Hashtable GetItemDict_ToEdit() //可以直接插入删除
    {
      return SubDoerUtil3.GetSubDoerDict_ToEdit(parent_doer, sub_doer_key);
    }


    public Item GetItem(string id_or_rid)
    {
      return SubDoerUtil3.GetSubDoer<Item>(parent_doer, sub_doer_key, id_or_rid);
    }

    public void ClearItems()
    {
      SubDoerUtil3.ClearSubDoers<Item>(this.parent_doer, this.sub_doer_key, item => { });
    }
  }
}