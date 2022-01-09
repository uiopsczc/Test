using System.Collections;

namespace CsCat
{
	public class Equips
	{
		private Doer parent_doer;
		private string sub_doer_key;

		public Equips(Doer parent_doer, string sub_doer_key)
		{
			this.parent_doer = parent_doer;
			this.sub_doer_key = sub_doer_key;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Item>(this.parent_doer, this.sub_doer_key);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dict_tmp, string save_key = null)
		{
			save_key = save_key ?? "equips";
			var equips = this.GetEquips();
			var dict_equips = new ArrayList();
			var dict_equips_tmp = new Hashtable();
			foreach (var equip in equips)
			{
				if (equip.CanFold()) // 可折叠
					dict_equips.Add(equip.GetId());
				else // 不可折叠，需存储数据
				{
					var dict_equip = new Hashtable();
					var dict_equip_tmp = new Hashtable();
					var rid = equip.GetRid();
					equip.PrepareSave(dict_equip, dict_equip_tmp);
					dict_equip["rid"] = rid;
					dict_equips.Add(dict_equip);
					if (!dict_equip_tmp.IsNullOrEmpty())
						dict_equips_tmp[rid] = dict_equip_tmp;
				}
			}

			if (!dict_equips.IsNullOrEmpty())
				dict[save_key] = dict_equips;
			if (!dict_equips_tmp.IsNullOrEmpty())
				dict_tmp[save_key] = dict_equips_tmp;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
		{
			restore_key = restore_key ?? "equips";
			this.ClearEquips();
			var dict_equips = dict.Remove3<ArrayList>(restore_key);
			var dict_equips_tmp = dict_tmp?.Remove3<Hashtable>(restore_key);
			var equips = this.GetEquips_ToEdit();
			if (!dict_equips.IsNullOrEmpty())
			{
				foreach (var value in dict_equips)
				{
					Item item;
					if (value is string) //id情况，可折叠的装备
					{
						var id = value.ToString();
						item = Client.instance.itemFactory.NewDoer(id) as Item;
					}
					else //不可折叠的情况
					{
						var dict_equip = value as Hashtable;
						var rid = dict_equip.Remove3<string>("rid");
						item = Client.instance.itemFactory.NewDoer(rid) as Item;
						item.SetEnv(this.parent_doer);
						Hashtable dict_equip_tmp = null;
						if (dict_equips_tmp != null && dict_equips_tmp.ContainsKey(rid))
							dict_equip_tmp = dict_equips_tmp[rid] as Hashtable;
						item.FinishRestore(dict_equip, dict_equip_tmp);
					}

					item.SetEnv(this.parent_doer);
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
			return SubDoerUtil1.GetSubDoers<Item>(this.parent_doer, this.sub_doer_key, id, null);
		}

		public ArrayList GetEquips_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parent_doer, this.sub_doer_key);
		}

		//是否有装备
		public bool HasEquips()
		{
			return SubDoerUtil1.HasSubDoers<Item>(this.parent_doer, this.sub_doer_key);
		}

		public int GetEquipsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this.parent_doer, this.sub_doer_key);
		}

		public bool __FilterType(Item equip, string type_1, string type_2 = null)
		{
			if (equip.GetType1() == type_1 && (type_2 == null || type_2.Equals(equip.GetType2())))
				return true;
			else
				return false;
		}

		//获得指定种类的装备
		public Item[] GetEquipsOfTypes(string type_1, string type_2 = null)
		{
			return SubDoerUtil1.GetSubDoers<Item>(this.parent_doer, this.sub_doer_key, null,
			  (equip) => this.__FilterType(equip, type_1, type_2));
		}

		//是否有指定种类装备
		public bool HasEquipsOfTypes(string type_1, string type_2 = null)
		{
			return SubDoerUtil1.HasSubDoers<Item>(this.parent_doer, this.sub_doer_key, null,
			  (equip) => this.__FilterType(equip, type_1, type_2));
		}


		//是否有指定种类装备
		public int GetEquipsCountOfTypes(string type_1, string type_2 = null)
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this.parent_doer, this.sub_doer_key, null,
			  (equip) => this.__FilterType(equip, type_1, type_2));
		}

		//获得指定的装备
		public Item GetEquip(string id_or_rid)
		{
			return SubDoerUtil1.GetSubDoer<Item>(this.parent_doer, this.sub_doer_key, id_or_rid);
		}

		//获得指定的装备
		public Item GetEquipOfTypes(string type_1, string type_2 = null)
		{
			var equips = this.GetEquipsOfTypes(type_1, type_2);
			if (equips.IsNullOrEmpty())
				return null;
			return equips[0];
		}

		//清除所有镶物
		public void ClearEquips()
		{
			SubDoerUtil1.ClearSubDoers<Item>(this.parent_doer, this.sub_doer_key,
			  (equip) => { ((Critter)this.parent_doer).TakeOffEquip(equip); });
		}
	}
}