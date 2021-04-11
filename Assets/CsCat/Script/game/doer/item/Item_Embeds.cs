namespace CsCat
{
  public partial class Item
  {


    ///////////////////////////////////////镶物容器//////////////////////////////
    //获得指定的镶物
    public Item[] GetEmbeds(string id = null)
    {
      return this.o_embeds.GetEmbeds(id);
    }

    //是否有镶物
    public bool HasEmbeds()
    {
      return this.o_embeds.HasEmbeds();
    }

    public int GetEmbedsCount()
    {
      return this.o_embeds.GetEmbedsCount();
    }

    //获得指定的镶物
    public Item GetEmbed(string id_or_rid)
    {
      return this.o_embeds.GetEmbed(id_or_rid);
    }

    //清除所有镶物
    public void ClearEmbeds(Item embed)
    {
      this.o_embeds.ClearEmbeds();
    }

    //检测镶入镶物
    public bool CheckEmbedOn(Item embed)
    {
      return this.OnCheckEmbedOn(embed) && embed.OnCheckEmbedOn(this);
    }

    //镶入镶物
    public bool EmbedOn(Item embed)
    {
      var env = embed.GetEnv();
      if (env != null)
      {
        LogCat.error(string.Format("{0} still in {1}", embed, env));
        return false;
      }

      var list = this.o_embeds.GetEmbeds_ToEdit();
      if (list.Contains(embed))
      {
        LogCat.error(string.Format("{0} already embed on {1}", this, embed));
        return false;
      }

      if (!(this.OnEmbedOn(embed) && embed.OnEmbedOn(this)))
        return false;

      embed.SetEnv(this);
      list.Add(embed);
      return true;
    }

    //检测卸下镶物
    public bool CheckEmbedOff(Item embed)
    {
      return this.OnCheckEmbedOff(embed) && embed.OnCheckEmbedOff(this);
    }

    //卸下镶物
    public bool EmbedOff(Item embed)
    {
      var list = this.o_embeds.GetEmbeds_ToEdit();
      if (!list.Contains(embed))
      {
        LogCat.error(string.Format("{0} not contains embed:{1}", this, embed));
        return false;
      }

      if (!(this.OnEmbedOff(embed) && embed.OnEmbedOff(this)))
        return false;

      list.Remove(embed);
      embed.SetEnv(null);
      return true;
    }

    ///////////////////////////////////////OnXXX//////////////////////////////
    public virtual bool OnCheckEmbedOn(Item embed)
    {
      return true;
    }

    public virtual bool OnEmbedOn(Item embed)
    {
      return true;
    }

    public virtual bool OnCheckEmbedOff(Item embed)
    {
      return true;
    }

    public virtual bool OnEmbedOff(Item embed)
    {
      return true;
    }
  }
}