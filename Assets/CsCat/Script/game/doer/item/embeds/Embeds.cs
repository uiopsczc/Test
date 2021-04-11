using System.Collections;

namespace CsCat
{
  public class Embeds
  {
    private Doer parent_doer;
    private string sub_doer_key;

    public Embeds(Doer parent_doer, string sub_doer_key)
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
      save_key = save_key ?? "embed_ids";
      var embeds = this.GetEmbeds();

      var dict_embed_ids = new ArrayList();
      foreach (var embed in embeds)
      {
        dict_embed_ids.Add(embed.GetId());
      }

      if (!dict_embed_ids.IsNullOrEmpty())
        dict[save_key] = dict_embed_ids;
    }

    //还原
    public void DoRestore(Hashtable dict, Hashtable dict_tmp, string restore_key = null)
    {
      restore_key = restore_key ?? "embed_ids";
      this.ClearEmbeds();
      var dict_embed_ids = dict.Remove2<ArrayList>(restore_key);
      if (!dict_embed_ids.IsNullOrEmpty())
      {
        var embeds = this.GetEmbeds_ToEdit();
        foreach (var _embed_id in dict_embed_ids)
        {
          var embed_id = _embed_id as string;
          var embed = this.parent_doer.factory.NewDoer(embed_id) as Item;
          embed.SetEnv(this.parent_doer);
          embeds.Add(embed);
        }
      }
    }
    //////////////////////////OnXXX//////////////////////////////////////////////////


    ////////////////////////////////////////////////////////////////////////////
    //获得指定的镶物
    public Item[] GetEmbeds(string id = null)
    {
      return SubDoerUtil1.GetSubDoers<Item>(this.parent_doer, this.sub_doer_key, id, null);
    }

    public ArrayList GetEmbeds_ToEdit() //可以直接插入删除
    {
      return SubDoerUtil1.GetSubDoers_ToEdit(this.parent_doer, this.sub_doer_key);
    }

    public bool HasEmbeds()
    {
      return SubDoerUtil1.HasSubDoers<Item>(this.parent_doer, this.sub_doer_key);
    }

    public int GetEmbedsCount()
    {
      return SubDoerUtil1.GetSubDoersCount<Item>(this.parent_doer, this.sub_doer_key);
    }

    //获得指定的镶物
    public Item GetEmbed(string id_or_rid)
    {
      return SubDoerUtil1.GetSubDoer<Item>(this.parent_doer, this.sub_doer_key, id_or_rid);
    }

    //清除所有镶物
    public void ClearEmbeds()
    {
      SubDoerUtil1.ClearSubDoers<Item>(this.parent_doer, this.sub_doer_key,
        (embed) => { ((Item) this.parent_doer).EmbedOff(embed); });
    }
  }
}