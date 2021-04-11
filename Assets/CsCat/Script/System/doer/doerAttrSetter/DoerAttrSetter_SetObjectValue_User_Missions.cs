namespace CsCat
{
  public partial class DoerAttrSetter
  {
    public bool SetObjectValue_User_Missions(User user, string key, object object_value, bool is_add)
    {
      bool is_break = false;
      if (key.StartsWith("missions.")) //任务对象
      {
        key = key.Substring("missions.".Length);
        int pos = key.IndexOf(".");
        if (pos != -1)
        {
          string mission_id = key.Substring(0, pos);
          key = key.Substring(pos);
          if (mission_id.EndsWith("t")) //改变tmpValue
          {
            mission_id = mission_id.Substring(0, mission_id.Length - 1);
            key = "t" + key;
          }

          var mission = user.GetMission(mission_id);
          if (mission != null)
          {
            DoerAttrSetter doerAttrSetter = new DoerAttrSetter(desc);
            doerAttrSetter.SetU(mission);
            doerAttrSetter.SetObject("u" + key, object_value, is_add);
          }
        }

        return true;
      }

      return is_break;
    }
  }
}