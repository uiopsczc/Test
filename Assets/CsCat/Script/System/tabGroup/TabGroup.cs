using System;
using System.Collections.Generic;

namespace CsCat
{
  public class TabGroup
  {
    private List<Tab> tab_list = new List<Tab>();
    public int cur_selected_index = -1;//默认全部不选中
    private int pre_selected_index = -1;
    public TabGroup()
    {
    }

    public void AddTab(Tab tab)
    {
      tab_list.Add(tab);
    }

    public Tab GetTab(int index)
    {
      return tab_list[index];
    }

    public void ClearTabs()
    {
      tab_list.Clear();
    }

    public void TriggerTab(int index)
    {
      if (index == cur_selected_index)
        return;
      if (pre_selected_index != -1)
        tab_list[pre_selected_index].UnSelect();
      pre_selected_index = cur_selected_index;

      cur_selected_index = index;
      tab_list[cur_selected_index].Select();
    }
  }
}