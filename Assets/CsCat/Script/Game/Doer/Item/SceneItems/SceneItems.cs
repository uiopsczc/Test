using System;
using System.Collections;

namespace CsCat
{
	public class SceneItems
	{
		private readonly Doer _parentDoer;
		private readonly string _subDoerKey;

		public SceneItems(Doer parentDoer, string subDoerKey)
		{
			this._parentDoer = parentDoer;
			this._subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil3.DoReleaseSubDoer<Item>(this._parentDoer, this._subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "sceneItems";
			var items = this.GetItems();
			var dictItems = new Hashtable();
			var dictItemsTmp = new Hashtable();
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
				var dictItem = new Hashtable();
				var dictItemTmp = new Hashtable();
				item.PrepareSave(dictItem, dictItemTmp);
				string rid = item.GetRid();
				dictItems[rid] = dictItem;
				if (!dictItemTmp.IsNullOrEmpty())
					dictItemsTmp[rid] = dictItemTmp;
			}

			if (!dictItems.IsNullOrEmpty())
				dict[saveKey] = dictItems;
			if (!dictItemsTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictItemsTmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "sceneItems";
			this.ClearItems();
			var dictItems = dict.Remove3<Hashtable>(restoreKey);
			var dictItemsTmp = dictTmp?.Remove3<Hashtable>(restoreKey);
			if (!dictItems.IsNullOrEmpty())
			{
				foreach (string rid in dictItems.Keys)
				{
					var itemDict = this.GetItemDict_ToEdit();
					Hashtable dictItem = dictItems[rid] as Hashtable;
					Item item = Client.instance.itemFactory.NewDoer(rid) as Item;
					item.SetEnv(this._parentDoer);
					Hashtable dictItemTmp = null;
					if (dictItemsTmp != null && dictItemsTmp.ContainsKey(rid))
						dictItemTmp = dictItemsTmp[rid] as Hashtable;
					item.FinishRestore(dictItem, dictItemTmp);
					itemDict[rid] = item;
				}
			}
		}
		///////////////////////////////OnXXX/////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		public Item[] GetItems(string id = null, Func<Item, bool> filterFunc = null)
		{
			return SubDoerUtil3.GetSubDoers(_parentDoer, _subDoerKey, id, filterFunc);
		}

		public Hashtable GetItemDict_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil3.GetSubDoerDict_ToEdit(_parentDoer, _subDoerKey);
		}


		public Item GetItem(string idOrRid)
		{
			return SubDoerUtil3.GetSubDoer<Item>(_parentDoer, _subDoerKey, idOrRid);
		}

		public void ClearItems()
		{
			SubDoerUtil3.ClearSubDoers<Item>(this._parentDoer, this._subDoerKey, item => { });
		}
	}
}