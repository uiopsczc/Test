using System.Collections.Generic;
using System.Text;

namespace CsCat
{
  public class FindItemMission : Mission
  {
    public override bool OnAccept(User user)
    {
      if (!base.OnAccept(user))
        return false;
      DoerAttrParser doerAttrParser = new DoerAttrParser(Client.instance.user, this, this.GetOwner(), null);
      Dictionary<string, string> find_item_dict = GetMissionDefinition().find_item_dict;
      Dictionary<string, int> _find_item_dict = new Dictionary<string, int>();
      foreach (var item_id in find_item_dict.Keys)
      {
        string item_count_string = find_item_dict[item_id];
        int item_count = doerAttrParser.ParseInt(item_count_string, 0);
        _find_item_dict[item_id] = item_count;
      }

      this.Set("find_item_dict", _find_item_dict);
      return true;
    }

    public override bool IsReady()
    {
      if (base.IsReady())
        return true;
      Dictionary<string, int> find_item_dict = this.Get<Dictionary<string, int>>("find_item_dict");
      foreach (var item_id in find_item_dict.Keys)
      {
        var user = this.GetOwner() as User;
        if (user.GetItemCount(item_id) < find_item_dict[item_id])
          return false;
      }

      return this.Get<bool>("is_ready");
    }

    public override string GetStatusString()
    {
      var user = this.GetOwner() as User;
      Dictionary<string, int> find_item_dict = this.Get<Dictionary<string, int>>("find_item_dict");
      bool is_finished = true;
      StringBuilder sb = new StringBuilder();
      foreach (var item_id in find_item_dict.Keys)
      {
        string item_name = DefinitionManager.instance.itemDefinition.GetData(item_id).name;
        int cur_count = user.GetItemCount(item_id);
        int need_count = find_item_dict[item_id];
        if (cur_count > need_count)
          cur_count = need_count;
        if (cur_count < need_count)
          is_finished = false;
        sb.Append(string.Format("{0}:{1}/{2}\n", item_name, cur_count, need_count)); // {name}: {cur}/{total};
      }

      if (is_finished || this.CheckFinishCondition())
        sb.Append(Translation.GetText("已完成") + "\n");
      string result = sb.ToString();
      return result.Substring(0, result.Length - 1); //去掉最后一个\n
    }
  }
}