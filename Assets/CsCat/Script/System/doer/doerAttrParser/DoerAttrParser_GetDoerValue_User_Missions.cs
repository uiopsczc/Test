namespace CsCat
{
  public partial class DoerAttrParser
  {
    public bool GetDoerValue_User_Missions(User user, string key, string type_string, out string result)
    {
      bool is_break = false;
      result = null;
      if (key.StartsWith("missions.")) //任务对象
      {
        key = key.Substring("missions.".Length);
        string mission_id;
        int pos = key.IndexOf(".");
        if (pos != -1)
        {
          mission_id = key.Substring(0, pos);
          key = key.Substring(pos);
          if (mission_id.EndsWith("t")) //修改tmpValue
          {
            mission_id = mission_id.Substring(0, mission_id.Length - 1);
            key = "t" + key;
          }
        }
        else
        {
          mission_id = key;
          key = "";
        }

        Mission mission = user.GetMission(mission_id);
        if (mission != null) //身上有这个任务
        {
          if (key.Length > 0)
          {
            DoerAttrParser doerAttrParser = new DoerAttrParser(mission);
            result = doerAttrParser.ParseString(type_string + "u" + key);
            return true;
          }
          else
          {
            result = ConvertValue("1", type_string);
            return true;
          }
        }
        else if (key.StartsWith(".status"))
        {
          if (user.GetFinishedMissionIds().Contains(mission_id))
          {
            result = ConvertValue(4, type_string); // 已完成
            return true;
          }
          else
          {
            result = ConvertValue(0, type_string); // 未接到
            return true;
          }
        }

        return true;
      }

      return is_break;
    }
  }
}