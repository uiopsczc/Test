namespace CsCat
{
  public class ItemFactory : DoerFactory
  {
    protected override string default_doer_class_path => "CsCat.Item";

    protected override string GetClassPath(string id)
    {
      return this.GetCfgItemData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgItemData(id).class_path_cs;
    }

    public CfgItemData GetCfgItemData(string id)
    {
      return CfgItem.Instance.get_by_id(id);
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new ItemDBase(id_or_rid);
    }

  }
}