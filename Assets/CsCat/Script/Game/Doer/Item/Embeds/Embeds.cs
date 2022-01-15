using System.Collections;

namespace CsCat
{
	public class Embeds
	{
		private Doer parentDoer;
		private string subDoerKey;

		public Embeds(Doer parentDoer, string subDoerKey)
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
			saveKey = saveKey ?? "embed_ids";
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
			restoreKey = restoreKey ?? "embed_ids";
			this.ClearEmbeds();
			var dictEmbedIds = dict.Remove3<ArrayList>(restoreKey);
			if (!dictEmbedIds.IsNullOrEmpty())
			{
				var embeds = this.GetEmbeds_ToEdit();
				for (var i = 0; i < dictEmbedIds.Count; i++)
				{
					var curEmbedId = dictEmbedIds[i];
					var embedId = curEmbedId as string;
					var embed = this.parentDoer.factory.NewDoer(embedId) as Item;
					embed.SetEnv(this.parentDoer);
					embeds.Add(embed);
				}
			}
		}
		//////////////////////////OnXXX//////////////////////////////////////////////////


		////////////////////////////////////////////////////////////////////////////
		//获得指定的镶物
		public Item[] GetEmbeds(string id = null)
		{
			return SubDoerUtil1.GetSubDoers<Item>(this.parentDoer, this.subDoerKey, id, null);
		}

		public ArrayList GetEmbeds_ToEdit() //可以直接插入删除
		{
			return SubDoerUtil1.GetSubDoers_ToEdit(this.parentDoer, this.subDoerKey);
		}

		public bool HasEmbeds()
		{
			return SubDoerUtil1.HasSubDoers<Item>(this.parentDoer, this.subDoerKey);
		}

		public int GetEmbedsCount()
		{
			return SubDoerUtil1.GetSubDoersCount<Item>(this.parentDoer, this.subDoerKey);
		}

		//获得指定的镶物
		public Item GetEmbed(string idOrRid)
		{
			return SubDoerUtil1.GetSubDoer<Item>(this.parentDoer, this.subDoerKey, idOrRid);
		}

		//清除所有镶物
		public void ClearEmbeds()
		{
			SubDoerUtil1.ClearSubDoers<Item>(this.parentDoer, this.subDoerKey,
			  (embed) => { ((Item)this.parentDoer).EmbedOff(embed); });
		}
	}
}