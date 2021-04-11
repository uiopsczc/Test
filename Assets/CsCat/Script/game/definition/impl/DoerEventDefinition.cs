namespace CsCat
{
  public class DoerEventDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/DoerEventDefinition"; }
    }

    public DoerEventDefinition GetData(string id)
    {
      return GetData<DoerEventDefinition>(id);
    }

    public DoerEventDefinition GetData(int id)
    {
      return GetData<DoerEventDefinition>(id);
    }

    public bool is_not_talk;
    public bool is_not_open;
    public string trigger_condition;
    public string trigger_desc;
    public string can_not_trigger_desc;
    public string[] step_ids;

    public bool is_open
    {
      get { return !is_not_open; }
    }
  }
}

  