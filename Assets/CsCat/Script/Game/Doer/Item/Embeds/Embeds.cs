using System.Collections;

namespace CsCat
{
	public class Embeds
	{
		private readonly Doer _parentDoer;
		private readonly string _subDoerKey;

		public Embeds(Doer parentDoer, string subDoerKey)
		{
			this._parentDoer = parentDoer;
			this._subDoerKey = subDoerKey;
		}

		////////////////////DoXXX/////////////////////////////////
		//卸载
		public void DoRelease()
		{
			SubDoerUtil1.DoReleaseSubDoer<Item>(this._parentDoer, this._subDoerKey);
		}

		//保存
		public void DoSave(Hashtable dict, Hashtable dictTmp, string saveKey = null)
		{
			saveKey = saveKey ?? "embedIds";
			var embeds = this.GetEmbeds();

			var dictEmbedIds = new ArrayList();
			for (var i = 0; i < embeds.Length; i++)
			{
				var embed = embeds[i];
				dictEmbedIds.Add(embed.GetId());
			}

			if (!dictEmbedIds.IsNullOrEmpty())
				dict[saveKey] = dictEmbedIds;
		}

		//还原
		public void DoRestore(Hashtable dict, Hashtable dictTmp, string restoreKey = null)
		{
			restoreKey = restoreKey ?? "embedIds";
			this.ClearEmbeds();
			var dictEmbedIds = dict.Remove3<ArrayList>(restoreKey);
			if (!dictEmbedIds.IsNullOrEmpty())
			{
				var embeds = this.GetEmbeds_ToEdit();
				for (var i = 0; i < dictEmbedIds.Count; i++)
				{
					var curEmbedId = dictEmbedIds[i];
					var embedId = curEmbedId as string;
					var embed = this._parentDoer.factory.NewDoer(embedId) as Item;
					embed.SetEnv(this._parentDoer);
					embeds.Add(embed);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		//获得指定的镶物
		public Item[] GetEmbeds(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Item>(this._parentDoer, this._subDoerKey, id, null);
		}

		public ArrayList GetEmbeds_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this._parentDoer, this._subDoerKey);
		}

		public bool IsHasEmbeds()
		{
			return SubDoerUtil1.HasSubDoers<Item>(this._parentDoer, this._subDoerKey);
		}

		public int GetEmbedsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this._parentDoer, this._subDoerKey);
		}

		//获得指定的镶物
		public Item GetEmbed(string idOrRid)
		{
			return SubDoerUtil1.GetSubDoer<Item>(this._parentDoer, this._subDoerKey, idOrRid);
		}

		//清除所有镶物
		public void ClearEmbeds()
		{
			SubDoerUtil1.ClearSubDoers<Item>(this._parentDoer, this._subDoerKey,
			  (embed) => { ((Item)this._parentDoer).EmbedOff(embed); });
		}
	}
}