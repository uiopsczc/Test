using System.Collections.Generic;

namespace CsCat
{
  public class MissionDefinition : ExcelAssetBase
  {
    protected override string path
    {
      get { return "data/excel_asset/MissionDefinition"; }
    }

    public MissionDefinition GetData(string id)
    {
      return GetData<MissionDefinition>(id);
    }

    public MissionDefinition GetData(int id)
    {
      return GetData<MissionDefinition>(id);
    }

    public bool is_auto_check_finish;
    public string finish_condition;
    public string onAccept_doerEvent_id;
    public string onFinish_doerEvent_id;
    public string onGiveUp_doerEvent_id;


    public Dictionary<string, string> find_item_dict;
    public Dictionary<string, string> reward_dict;
  }
}


  