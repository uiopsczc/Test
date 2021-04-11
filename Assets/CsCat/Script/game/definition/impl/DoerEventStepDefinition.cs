using System.Collections.Generic;

namespace CsCat
{
  public class DoerEventStepDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/DoerEventStepDefinition"; }
    }

    public DoerEventStepDefinition GetData(string id)
    {
      return GetData<DoerEventStepDefinition>(id);
    }

    public DoerEventStepDefinition GetData(int id)
    {
      return GetData<DoerEventStepDefinition>(id);
    }

    public string trigger_condition;
    public string trigger_desc;
    public string can_not_trigger_desc;
    public string execute_condition;
    public string execute_desc;
    public string can_not_execute_desc;
    public bool is_stop_here;
    public Dictionary<string, string> set_attr_dict;
    public Dictionary<string, string> add_attr_dict;
    public Dictionary<string, string> deal_item_dict;
    public string[] give_up_mission_ids;
    public string[] accept_mission_ids;
    public string[] finish_mission_ids;
    public string[] add_finished_mission_ids;
    public string[] remove_finished_mission_ids;

  }
}


  