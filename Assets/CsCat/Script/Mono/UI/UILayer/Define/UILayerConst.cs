using System.Collections.Generic;

namespace CsCat
{
	public static class UILayerConst
	{
		public static int Order_Per_Panel = 50;
		public static int Order_Per_Layer = 1000;

		private static LinkedDictionary<EUILayerName, UILayerConfig> _uiLayerConfigDict;

		public static LinkedDictionary<EUILayerName, UILayerConfig> uiLayerConfigDict
		{
			get
			{
				if (_uiLayerConfigDict == null)
				{
					_uiLayerConfigDict = new LinkedDictionary<EUILayerName, UILayerConfig>();
					int i = 0;
					_uiLayerConfigDict[EUILayerName.BlackMaskUILayer] = new UILayerConfig(EUILayerName.BlackMaskUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.SceneUILayer] = new UILayerConfig(EUILayerName.SceneUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.HUDUILayer] = new UILayerConfig(EUILayerName.HUDUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.BackgroundUILayer] = new UILayerConfig(EUILayerName.BackgroundUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.FrontUILayer] = new UILayerConfig(EUILayerName.FrontUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Hide_BackgroundUILayer)));
					_uiLayerConfigDict[EUILayerName.PopUpUILayer] = new UILayerConfig(EUILayerName.PopUpUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Add_BlackMaskBehind)));
					_uiLayerConfigDict[EUILayerName.LoadingUILayer] = new UILayerConfig(EUILayerName.LoadingUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.FadeUILayer] = new UILayerConfig(EUILayerName.FadeUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.WaitingUILayer] = new UILayerConfig(EUILayerName.WaitingUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
					_uiLayerConfigDict[EUILayerName.NotifyUILayer] = new UILayerConfig(EUILayerName.NotifyUILayer,
					  i++ * Order_Per_Layer, new UILayerRule((uint)(EUILayerRule.Null)));
				}
				return _uiLayerConfigDict;
			}
		}
	}
}