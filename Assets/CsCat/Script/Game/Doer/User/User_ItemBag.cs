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
			return this.oItemBag.GetItems(id);
		}


		public Item GetItem(string idOrRid)
		{
			return this.oItemBag.GetItem(idOrRid);
		}


		public Item[] GetItemsOfTypes(string type1, string type2 = null)
		{
			return this.oItemBag.GetItemsOfTypes(type1, type2);
		}

		public string[] GetItemIds()
		{
			return this.oItemBag.GetItemIds();
		}


		public int GetItemCount(string id)
		{
			return this.oItemBag.GetItemCount(id);
		}

		public bool HasItem(string idOrRid)
		{
			return this.oItemBag.HasItem(idOrRid);
		}

		// 放入物品
		// 对于可折叠物品则会替代已存在的物品对象并数量叠加
		// 对于不可折叠物品则直接加入到对象列表
		public void AddItems(string id, int count)
		{
			var items = this.oItemBag.AddItems(id, count);
			for (var i = 0; i < items.Count; i++)
			{
				var item = items[i];
				this.OnAddItem(item);
			}
		}

		public Item[] RemoveItems(string id, int count)
		{
			var items = this.oItemBag.RemoveItems(id, count);
			for (var i = 0; i < items.Length; i++)
			{
				var item = items[i];
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

			return false;
		}

		public void AddItem(Item item)
		{
			this.oItemBag.AddItem(item);
			this.OnAddItem(item);
		}

		public Item RemoveItem(Item item)
		{
			var result = this.oItemBag.RemoveItem(item);
			if (result != null)
				this.OnRemoveItem(item);
			return result;
		}

		public bool CanRemoveItems(string id, int count)
		{
			return this.oItemBag.CanRemoveItems(id, count);
		}

		public void ClearItems()
		{
			this.oItemBag.ClearItems();
		}

		//////////////////////OnXXX/////////////////////////////////////
		public virtual void OnAddItem(Item item)
		{
		}

		public virtual void OnRemoveItem(Item item)
		{
		}

		///////////////////////Util////////////////////////////////
		public bool UseItem(string idOrRid, Critter target)
		{
			var item = this.GetItem(idOrRid);
			if (item == null)
			{
				LogCat.error(string.Format("UseItem error:do not has {0}", idOrRid));
				return false;
			}

			if (!target.CheckUseItem(item))
				return false;
			item = item.IsCanFold() ? this.RemoveItems(item.GetId(), 1)[0] : this.RemoveItem(item);
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
		public bool DealItems(Dictionary<string, string> itemDict, DoerAttrParser doerAttrParser = null)
		{
			doerAttrParser = doerAttrParser ?? new DoerAttrParser(this);
			foreach (var keyValue in itemDict)
			{
				var itemId = keyValue.Key;
				string value = itemDict[itemId];
				Hashtable addAttrDict = new Hashtable(); //带属性
				int pos = value.IndexOf("(");
				if (pos != -1)
				{
					string attrString = value.Substring(pos + 1, value.Length - pos - 2); //最后一个)也要删除
					value = value.Substring(0, pos);

					addAttrDict = attrString.ToDictionary<string, string>().ToHashtable();

				}

				int count = doerAttrParser.ParseInt(value);
				if (count < 0) //remove Items
				{
					count = Math.Abs(count);
					Item[] items = this.RemoveItems(itemId, count);
					for (var i = 0; i < items.Length; i++)
					{
						var item = items[i];
						item.Destruct();
					}
				}
				else //add Items
				{
					Item item = Client.instance.itemFactory.NewDoer(itemId) as Item;
					for (int i = 0; i < count; i++)
					{
						if (!addAttrDict.IsNullOrEmpty())
						{
							var list = new ArrayList(addAttrDict.Keys);
							for (var j = 0; j < list.Count; j++)
							{
								var attrName = (string)list[j];
								addAttrDict[attrName] = doerAttrParser.Parse(addAttrDict[attrName] as string);
							}

							item.AddAll(addAttrDict);
						}

						bool isCanFold = item.IsCanFold();
						if (isCanFold)
						{
							item.SetCount(count);
							this.AddItem(item);
							break;
						}

						this.AddItem(item);
					}
				}
			}

			return true;
		}


		/////////////////////////////////////////装备/////////////////////////////////
		public bool PutOnEquip(string idOrRid, Critter target)
		{
			var item = this.GetItem(idOrRid);
			if (item == null)
				return false;
			string type1 = item.GetType1();
			string type2 = item.GetType2();
			var oldEquip = target.GetEquipOfTypes(type1, type2);
			if (oldEquip != null)
			{
				if (!this.TakeOffEquip(oldEquip, target))
					return false;
			}

			if (!target.CheckPutOnEquip(item))
				return false;
			item = item.IsCanFold() ? this.RemoveItems(item.GetId(), 1)[0] : this.RemoveItem(item);
			if (item == null)
			{
				LogCat.error(string.Format("PutOnEquip error:{0} do not has item:{1}", this, idOrRid));
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

		public bool EmbedOn(Item item, string embedIdOrRid)
		{
			var embed = this.GetItem(embedIdOrRid);
			return EmbedOn(item, embed);
		}

		public bool EmbedOn(string itemIdOrRid, Item embed)
		{
			var item = this.GetItem(itemIdOrRid);
			return EmbedOn(item, embed);
		}

		public bool EmbedOn(string itemIdOrRid, string embedIdOrRid)
		{
			var item = this.GetItem(itemIdOrRid);
			var embed = this.GetItem(embedIdOrRid);
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

		public bool EmbedOff(Item item, string embedIdOrRid)
		{
			var embed = item.GetEmbed(embedIdOrRid);
			return EmbedOff(item, embed);
		}

		public bool EmbedOff(string itemIdOrRid, Item embed)
		{
			var item = this.GetItem(itemIdOrRid);
			return EmbedOff(item, embed);
		}

		public bool EmbedOff(string itemIdOrRid, string embedIdOrRid)
		{
			var item = this.GetItem(itemIdOrRid);
			var embed = item.GetEmbed(embedIdOrRid);
			return EmbedOff(item, embed);
		}
	}
}