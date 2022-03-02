using System.Collections;

namespace CsCat
{
	public partial class Item : Thing
	{
		private Embeds oEmbeds;

		public override void Init()
		{
			base.Init();
			SetCount(1);
			oEmbeds = new Embeds(this, "o_embeds");
		}

		public ItemFactory GetItemFactory()
		{
			return this.factory as ItemFactory;
		}

		public CfgItemData GetCfgItemData()
		{
			return CfgItem.Instance.GetById(this.GetId());
		}

		///////////////////////////////////////DoXXX//////////////////////////////
		//卸载
		public override void DoRelease()
		{
			// 销毁镶物
			this.oEmbeds.DoRelease();
			base.DoRelease();
		}

		//保存
		public override void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			base.DoSave(dict, dictTmp);
			// 存储镶物
			this.oEmbeds.DoSave(dict, dictTmp);
		}

		//还原
		public override void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			// 还原镶物
			this.oEmbeds.DoRestore(dict, dictTmp);
			base.DoRestore(dict, dictTmp);
		}


		///////////////////////////////////////OnXXX//////////////////////////////

		public virtual bool OnCheckUseItem(Critter critter)
		{
			return true;
		}

		public virtual bool OnUseItem(Critter critter)
		{
			return true;
		}



		/////////////////////////////Util/////////////////////////////////////////
		public void SetGroup(string group)
		{
			SetTmp("group", group);
		}

		public string GetGroup()
		{
			return GetTmp("group", "");
		}

		public bool IsWeapon()
		{
			return this.IsType1(ItemConst.Item_Type1_Weapon);
		}

		public bool IsArmor()
		{
			return this.IsType1(ItemConst.Item_Type1_Armor);
		}

		public bool IsEmb()
		{
			return this.IsType1(ItemConst.Item_Type1_Embed);
		}

		public bool IsEquip()
		{
			return this.IsWeapon() || this.IsArmor();
		}

		public bool IsType1(string type1)
		{
			return type1.Equals(this.GetType1());
		}

		public bool IsType2(string type2)
		{
			return type2.Equals(this.GetType2());
		}

		public bool IsPutOn()
		{
			return this.GetTmp("o_isPutOn", false);
		}

		public void SetIsPutOn(bool isPutOn)
		{
			this.SetTmp("o_isPutOn", isPutOn);
		}

		public string GetType1()
		{
			return this.GetCfgItemData().type1;
		}

		public string GetType2()
		{
			return this.GetCfgItemData().type2;
		}

		public bool IsCanFold()
		{
			return this.GetCfgItemData().isCanFold;
		}

		public string GetName()
		{
			return this.GetCfgItemData().name;
		}

	}
}