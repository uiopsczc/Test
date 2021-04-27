using System.Collections.Generic;

namespace CsCat
{
  public static class UILayerConst
  {
    public static int Order_Per_Panel = 10;
    public static int Order_Per_Layer = 1000;

    private static LinkedDictionary<EUILayerName, UILayerConfig> _UILayerConfig_dict;

    public static LinkedDictionary<EUILayerName, UILayerConfig> UILayerConfig_dict
    {
      get
      {
        if (_UILayerConfig_dict == null)
        {
          _UILayerConfig_dict = new LinkedDictionary<EUILayerName, UILayerConfig>();
          int i = 0;
          _UILayerConfig_dict[EUILayerName.BlackMaskUILayer] = new UILayerConfig(EUILayerName.BlackMaskUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.SceneUILayer] = new UILayerConfig(EUILayerName.SceneUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint) (EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.HUDUILayer] = new UILayerConfig(EUILayerName.HUDUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.BackgroundUILayer] = new UILayerConfig(EUILayerName.BackgroundUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.FrontUILayer] = new UILayerConfig(EUILayerName.FrontUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Hide_BackgroundUILayer)));
          _UILayerConfig_dict[EUILayerName.PopUpUILayer] = new UILayerConfig(EUILayerName.PopUpUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Add_BlackMaskBehide)));
          _UILayerConfig_dict[EUILayerName.LoadingUILayer] = new UILayerConfig(EUILayerName.LoadingUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.FadeUILayer] = new UILayerConfig(EUILayerName.FadeUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.WaitingUILayer] = new UILayerConfig(EUILayerName.WaitingUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          _UILayerConfig_dict[EUILayerName.NotifyUILayer] = new UILayerConfig(EUILayerName.NotifyUILayer,
            i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
          }
        return _UILayerConfig_dict;
      }
    }
  }
}