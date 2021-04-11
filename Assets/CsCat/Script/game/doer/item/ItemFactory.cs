namespace CsCat
{
  public class ItemFactory : DoerFactory
  {
    protected override string default_doer_class_path
    {
      get { return "CsCat.Item"; }
    }

    public override ExcelAssetBase GetDefinitions()
    {
      return DefinitionManager.instance.itemDefinition;
    }

    public override ExcelAssetBase GetDefinition(string id)
    {
      return GetDefinitions().GetData<ItemDefinition>(id);
    }

    public ItemDefinition GetItemDefinition(string id)
    {
      return GetDefinition(id) as ItemDefinition;
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new ItemDBase(id_or_rid);
    }

  }
}