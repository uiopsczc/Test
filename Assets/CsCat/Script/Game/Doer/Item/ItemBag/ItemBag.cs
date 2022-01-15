using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
	public class ItemBag
	{
		private Doer parentDoer;
		private string subDoerKey;

		public ItemBag(Doer parentDoer, string subDoerKey)
		{
			this.parentDoer = parentDoer;
			this.subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil2.DoReleaseSubDoer<Item>(this.parentDoer, this.subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "item_bag";
			var items = this.GetItems();
			var dictItems = new Hashtable();
			var dictItemsTmp = new Hashtable();
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				var id = item.GetId();
				var rid = item.GetRid();
				var isCanFold = item.CanFold();
				if (isCanFold) //可折叠
					dictItems[id] = item.GetCount();
				else
				{
					var dictItemList = dictItems.GetOrAddDefault2(id, () => new ArrayList());
					var dictItem = new Hashtable();
					var dictItemTmp = new Hashtable();
					item.PrepareSave(dictItem, dictItemTmp);
					dictItem["rid"] = rid;
					dictItemList.Add(dictItem);
					if (!dictItemTmp.IsNullOrEmpty())
						dictItemsTmp[rid] = dictItemTmp;
				}
			}

			if (!dictItems.IsNullOrEmpty())
				dict[saveKey] = dictItems;
			if (!dictItemsTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictItemsTmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "item_bag";
			this.ClearItems();
			var dictItems = dict.Remove3<Hashtable>(restoreKey);
			var dictItemsTmp = dictTmp?.Remove3<Hashtable>(restoreKey);
			if (!dictItems.IsNullOrEmpty())
			{
				Item item;
				foreach (var key in dictItems.Keys)
				{
					var id = key as string;
					var value = dictItems[id];
					var items = this.GetItems_ToEdit(id);
					if (value is double) //id情况，可折叠的item
					{
						var count = int.Parse(value.ToString());
						item = Client.instance.itemFactory.NewDoer(id) as Item;
						item.SetEnv(this.parentDoer);
						item.SetCount(count);
						items.Add(item);
					}
					else //不可折叠的情况
					{
						var dictItemList = value as ArrayList;
						for (var i = 0; i < dictItemList.Count; i++)
						{
							var curDictItem = dictItemList[i];
							var dictItem = curDictItem as Hashtable;
							var rid = dictItem.Remove3<string>("rid");
							item = Client.instance.itemFactory.NewDoer(id) as Item;
							item.SetEnv(this.parentDoer);
							Hashtable dictItemTmp = null;
							if (dictItemsTmp != null && dictItemsTmp.ContainsKey(rid))
								dictItemTmp = dictItemsTmp[rid] as Hashtable;
							item.FinishRestore(dictItem, dictItemTmp);
							items.Add(item);
						}
					}
				}
			}
		}
		///////////////////////////////OnXXX/////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		public bool __FilterType(Item equip, string type1, string type2 = null)
		{
			return equip.GetType1() == type1 && (type2 == null || type2.Equals(equip.GetType2()));
		}


		public Item[] GetItems(string id = null)
		{
			return SubDoerUtil2.GetSubDoers<Item>(this.parentDoer, this.subDoerKey, id, null);
		}

		public ArrayList GetItems_ToEdit(string id) //可以直接插入删除
		{
			return SubDoerUtil2.GetSubDoers_ToEdit(this.parentDoer, this.subDoerKey, id);
		}

		//获得指定的镶物
		public Item GetItem(string idOrRid)
		{
			return SubDoerUtil2.GetSubDoer<Item>(this.parentDoer, this.subDoerKey, idOrRid);
		}


		public Item[] GetItemsOfTypes(string type1, string type2 = null)
		{
			return SubDoerUtil2.GetSubDoers<Item>(this.parentDoer, this.subDoerKey, null,
			  (item) => this.__FilterType(item, type1, type2));
		}


		public string[] GetItemIds()
		{
			return SubDoerUtil2.GetSubDoerIds(this.parentDoer, this.subDoerKey);
		}

		public int GetItemCount(string id)
		{
			return SubDoerUtil2.GetSubDoerCount<Item>(this.parentDoer, this.subDoerKey, id);
		}

		public bool HasItem(string id)
		{
			return SubDoerUtil2.HasSubDoers<Item>(this.parentDoer, this.subDoerKey, id);
		}

		// 放入物品
		// 对于可折叠物品则会替代已存在的物品对象并数量叠加
		// 对于不可折叠物品则直接加入到对象列表
		public List<Item> AddItems(string id, int count)
		{
			var cfgItemData = CfgItem.Instance.get_by_id(id);
			var isCanFold = cfgItemData.can_fold;
			Item item = null;
			List<Item> result = new List<Item>();
			if (isCanFold)
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
			SubDoerUtil2.AddSubDoers(this.parentDoer, this.subDoerKey, item);
		}

		public Item[] RemoveItems(string id, int count)
		{
			return SubDoerUtil2.RemoveSubDoers<Item>(this.parentDoer, this.subDoerKey, id, count,
			  Client.instance.itemFactory);
		}

		public bool CanRemoveItems(string id, int count)
		{
			return SubDoerUtil2.CanRemoveSubDoers(this.parentDoer, this.subDoerKey, id, count);
		}

		public Item RemoveItem(string rid)
		{
			return SubDoerUtil2.RemoveSubDoer<Item>(this.parentDoer, this.subDoerKey, rid);
		}

		public Item RemoveItem(Item item)
		{
			return SubDoerUtil2.RemoveSubDoer<Item>(this.parentDoer, this.subDoerKey, item);
		}

		public void ClearItems()
		{
			SubDoerUtil2.ClearSubDoers<Scene>(this.parentDoer, this.subDoerKey);
		}
	}
}