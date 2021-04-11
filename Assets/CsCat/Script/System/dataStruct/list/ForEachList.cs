using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class ForeachList<V>
  {
    private List<V> list;
    private List<V> tmp_to_check_value_list = new List<V>();
    private List<V> tmp_checked_value_list = new List<V>();

    public ForeachList(List<V> list)
    {
      this.list = list;
    }

    public IEnumerable<V> ForeachValues()
    {
      this.tmp_to_check_value_list.Clear();
      this.tmp_checked_value_list.Clear();

      this.tmp_to_check_value_list.AddRange(list);
      while (true)
      {
        while (tmp_to_check_value_list.Count > 0)
        {
          var tmp_to_check_value = tmp_to_check_value_list.RemoveFirst();
          if (!list.Contains(tmp_to_check_value)) //中途yield return value;可能有删除其他的节点，所以可能会出现null,所以要检测是否需要忽略
            continue;
          var value = tmp_to_check_value;
          yield return value;
          tmp_checked_value_list.Add(value);
        }

        //再次检测，是否有新生成的，如果有则加入tmp_to_check_value_list，然后再次调用上面的步骤，否则全部checked，跳出所有循环
        bool is_can_break = true;
        foreach (var value in list)
        {
          if (tmp_checked_value_list.Contains(value))
            continue;
          tmp_to_check_value_list.Add(value);
          is_can_break = false;
        }

        if (is_can_break)
          break;
      }
    }
  }
}