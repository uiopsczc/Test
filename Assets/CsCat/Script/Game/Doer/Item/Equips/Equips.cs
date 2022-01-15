using System.Collections;

namespace CsCat
{
	public class Equips
	{
		private Doer parentDoer;
		private string subDoerKey;

		public Equips(Doer parentDoer, string subDoerKey)
		{
			this.parentDoer = parentDoer;
			this.subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Item>(this.parentDoer, this.subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "equips";
			var equips = this.GetEquips();
			var dictEquips = new ArrayList();
			var dictEquipsTmp = new Hashtable();
			for (var i = 0; i < equips.Length; i++)
			{
				var equip = equips[i];
				if (equip.CanFold()) // 可折叠
					dictEquips.Add(equip.GetId());
				else // 不可折叠，需存储数据
				{
					var dictEquip = new Hashtable();
					var dictEquipTmp = new Hashtable();
					var rid = equip.GetRid();
					equip.PrepareSave(dictEquip, dictEquipTmp);
					dictEquip["rid"] = rid;
					dictEquips.Add(dictEquip);
					if (!dictEquipTmp.IsNullOrEmpty())
						dictEquipsTmp[rid] = dictEquipTmp;
				}
			}

			if (!dictEquips.IsNullOrEmpty())
				dict[saveKey] = dictEquips;
			if (!dictEquipsTmp.IsNullOrEmpty())
				dictTmp[saveKey] = dictEquipsTmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "equips";
			this.ClearEquips();
			var dictEquips = dict.Remove3<ArrayList>(restoreKey);
			var dictEquipsTmp = dictTmp?.Remove3<Hashtable>(restoreKey);
			var equips = this.GetEquips_ToEdit();
			if (!dictEquips.IsNullOrEmpty())
			{
				for (var i = 0; i < dictEquips.Count; i++)
				{
					var value = dictEquips[i];
					Item item;
					if (value is string) //id情况，可折叠的装备
					{
						var id = value.ToString();
						item = Client.instance.itemFactory.NewDoer(id) as Item;
					}
					else //不可折叠的情况
					{
						var dictEquip = value as Hashtable;
						var rid = dictEquip.Remove3<string>("rid");
						item = Client.instance.itemFactory.NewDoer(rid) as Item;
						item.SetEnv(this.parentDoer);
						Hashtable dictEquipTmp = null;
						if (dictEquipsTmp != null && dictEquipsTmp.ContainsKey(rid))
							dictEquipTmp = dictEquipsTmp[rid] as Hashtable;
						item.FinishRestore(dictEquip, dictEquipTmp);
					}

					item.SetEnv(this.parentDoer);
					item.SetIsPutOn(true);
					equips.Add(item);
				}
			}
		}
		////////////////////////////OnXXX////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		//获得指定的装备
		public Item[] GetEquips(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Item>(this.parentDoer, this.subDoerKey, id, null);
		}

		public ArrayList GetEquips_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parentDoer, this.subDoerKey);
		}

		//是否有装备
		public bool HasEquips()
		{
			return SubDoerUtil1.HasSubDoers<Item>(this.parentDoer, this.subDoerKey);
		}

		public int GetEquipsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this.parentDoer, this.subDoerKey);
		}

		public bool __FilterType(Item equip, string type1, string type2 = null)
		{
			return equip.GetType1() == type1 && (type2 == null || type2.Equals(equip.GetType2()));
		}

		//获得指定种类的装备
		public Item[] GetEquipsOfTypes(string type1, string type2 = null)
		{
			return SubDoerUtil1.GetSubDoers<Item>(this.parentDoer, this.subDoerKey, null,
			  (equip) => this.__FilterType(equip, type1, type2));
		}

		//是否有指定种类装备
		public bool HasEquipsOfTypes(string type1, string type2 = null)
		{
			return SubDoerUtil1.HasSubDoers<Item>(this.parentDoer, this.subDoerKey, null,
			  (equip) => this.__FilterType(equip, type1, type2));
		}


		//是否有指定种类装备
		public int GetEquipsCountOfTypes(string type1, string type2 = null)
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this.parentDoer, this.subDoerKey, null,
			  (equip) => this.__FilterType(equip, type1, type2));
		}

		//获得指定的装备
		public Item GetEquip(string idOrRid)
		{
			return SubDoerUtil1.GetSubDoer<Item>(this.parentDoer, this.subDoerKey, idOrRid);
		}

		//获得指定的装备
		public Item GetEquipOfTypes(string type1, string type2 = null)
		{
			var equips = this.GetEquipsOfTypes(type1, type2);
			return equips.IsNullOrEmpty() ? null : equips[0];
		}

		//清除所有镶物
		public void ClearEquips()
		{
			SubDoerUtil1.ClearSubDoers<Item>(this.parentDoer, this.subDoerKey,
			  (equip) => { ((Critter)this.parentDoer).TakeOffEquip(equip); });
		}
	}
}