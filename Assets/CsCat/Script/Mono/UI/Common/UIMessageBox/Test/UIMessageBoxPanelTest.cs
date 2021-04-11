using System.Collections.Generic;

namespace CsCat
{
  public class UIMessageBoxPanelTest
  {
    public static void Test()
    {
      UIMessageBoxPanel panel = Client.instance.uiManager.CreateChildPanel(null, default(UIMessageBoxPanel));
      panel.InvokeAfterAllAssetsLoadDone( () =>
      {
        //panel.Show("Hello",null, "world2222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222");
        List<Dictionary<string, int>> item_dict_list = new List<Dictionary<string, int>>();
        for (int i = 1; i < 100; i++)
        {
          item_dict_list.Add(new Dictionary<string, int>()
          {
            {"id", 1},
            {"count", i}
          });
        }

        panel.Show("Hello", "world", null, item_dict_list, "确定1", () => { LogCat.LogWarning("hello"); }, "确定2",
          () => { LogCat.LogWarning("world"); });
      });
    }
  }
}