using System.Collections.Generic;

namespace CsCat
{
	public static class UILayerConst
	{
		public static int Order_Per_Panel = 50;
		public static int Order_Per_Layer = 1000;

		private static LinkedDictionary<EUILayerName, UILayerConfig> _uiLayerConfig_dict;

		public static LinkedDictionary<EUILayerName, UILayerConfig> uiLayerConfig_dict
		{
			get
			{
				if (_uiLayerConfig_dict == null)
				{
					_uiLayerConfig_dict = new LinkedDictionary<EUILayerName, UILayerConfig>();
					int i = 0;
					_uiLayerConfig_dict[EUILayerName.BlackMaskUILayer] = new UILayerConfig(EUILayerName.BlackMaskUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.SceneUILayer] = new UILayerConfig(EUILayerName.SceneUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.HUDUILayer] = new UILayerConfig(EUILayerName.HUDUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.BackgroundUILayer] = new UILayerConfig(EUILayerName.BackgroundUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.FrontUILayer] = new UILayerConfig(EUILayerName.FrontUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Hide_BackgroundUILayer)));
					_uiLayerConfig_dict[EUILayerName.PopUpUILayer] = new UILayerConfig(EUILayerName.PopUpUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Add_BlackMaskBehide)));
					_uiLayerConfig_dict[EUILayerName.LoadingUILayer] = new UILayerConfig(EUILayerName.LoadingUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.FadeUILayer] = new UILayerConfig(EUILayerName.FadeUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.WaitingUILayer] = new UILayerConfig(EUILayerName.WaitingUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfig_dict[EUILayerName.NotifyUILayer] = new UILayerConfig(EUILayerName.NotifyUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
				}
				return _uiLayerConfig_dict;
			}
		}
	}
}