using System.Collections.Generic;

namespace CsCat
{
  public partial class UINotifyManager
  {
    //走马灯效果
    private List<string> lantern_notify_desc_cache_list = new List<string>();

    public void LanternNotify(string desc)
    {
      lantern_notify_desc_cache_list.Add(desc);
      if (!Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel").graphicComponent.gameObject
        .activeInHierarchy)
        __LanternNotify();
    }

    public void __LanternNotify()
    {
      if (lantern_notify_desc_cache_list.Count > 0)
      {
        string desc = lantern_notify_desc_cache_list.RemoveFirst<string>();
        UILanternNotifyPanel panel =
          Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel");
        panel.Show(desc);
      }
    }


  }
}




