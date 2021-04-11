using System;
using System.Collections.Generic;

namespace CsCat
{
  public partial class UIGMTestPanel2 : UIGMPanelBase
  {
    public override void InitConfigList()
    {
      base.InitConfigList();
      this.graphicComponent.gameObject.name = "test2";
      config_list.Add(new Dictionary<string, object>
      {
        {"type", "InputItem"},
        {"desc", "KKK"},
        {"yes_callback", new Action(() => LogCat.LogWarning("KKK"))},
      });
    }
  }
}