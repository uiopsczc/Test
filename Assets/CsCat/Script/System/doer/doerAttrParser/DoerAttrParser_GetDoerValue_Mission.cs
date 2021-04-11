using System.Collections;

namespace CsCat
{
  public partial class DoerAttrParser
  {
    public bool GetDoerValue_Mission(Doer doer, string key, string type_string, out string result)
    {
      bool is_break = false;
      result = null;
      if (doer is Mission)
      {
        Mission mission = doer as Mission;
        if (key.Equals("status"))
        {
          if (mission.IsReady())
          {
            result = ConvertValue(3, type_string); //已就绪,可以被完成
            return true;
          }
          else
          {
            result = ConvertValue(2, type_string); //未完成
            return true;
          }
        }
        else if (key.StartsWith("items.")) // 物品
        {
          string item_id = key.Substring("items.".Length);
          Hashtable items = mission.Get<Hashtable>("items");
          if (items != null)
          {
            int count = items.Get<int>(item_id);
            result = ConvertValue(count, type_string);
            return true;
          }
        }
      }

      return is_break;
    }
  }
}