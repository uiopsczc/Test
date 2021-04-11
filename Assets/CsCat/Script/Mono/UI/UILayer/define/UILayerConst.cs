using System.Collections.Generic;

namespace CsCat
{
  public static class UILayerConst
  {
    public static int Order_Per_Panel = 10;

    private static LinkedDictionary<EUILayerName, UILayerConfig> _UILayerConfig_dict;

    public static LinkedDictionary<EUILayerName, UILayerConfig> UILayerConfig_dict
    {
      get
      {
        if (_UILayerConfig_dict == null)
        {
          _UILayerConfig_dict = new LinkedDictionary<EUILayerName, UILayerConfig>();
          List<EUILayerName> euiLayerName_list = EnumUtil.GetValues<EUILayerName>();
          for (int i = 0; i < euiLayerName_list.Count; i++)
          {
            EUILayerName euiLayerName = euiLayerName_list[i];
            _UILayerConfig_dict[euiLayerName] = new UILayerConfig(euiLayerName, 0 + i * 1000, 0);
          }
        }

        return _UILayerConfig_dict;
      }
    }
  }
}