using System;
using System.Collections.Generic;

namespace CsCat
{
  public class TabGroup
  {
    private List<Tab> tab_list = new List<Tab>();
    public int selected_index
    {
      get
      {
        for (int i = 0; i < tab_list.Count; i++)
        {
          if (tab_list[i].is_selected)
            return i;
        }
        return -1;
      }
    }


    public TabGroup()
    {
    }

    public void AddTab(Tab tab)
    {
      tab_list.Add(tab);
    }

    public Tab GetTab(int i)
    {
      return tab_list[i];
    }

    public void ClearTabs()
    {
      tab_list.Clear();
    }

    public void TriggerTab(int index)
    {
      if (index == selected_index)
        return;
      int pre_selected_index = selected_index;
      int current_selected_index = index;
      if (pre_selected_index != -1)
        tab_list[pre_selected_index].UnSelect();
      tab_list[current_selected_index].Select();
    }
  }
}