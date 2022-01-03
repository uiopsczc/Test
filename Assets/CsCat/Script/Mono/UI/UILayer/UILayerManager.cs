using System.Collections.Generic;
using UnityEngine;
using RectTransform = UnityEngine.RectTransform;

namespace CsCat
{
	public class UILayerManager : UIObject
	{
		public Dictionary<EUILayerName, UILayer> uiLayer_dict = new Dictionary<EUILayerName, UILayer>();

		public UIManager uiManager;

		public void Init(UIManager uiManager)
		{
			base.Init();
			this.uiManager = uiManager;
			for (int i = 0; i < UILayerConst.uiLayerConfig_dict.Values.Count; i++)
			{
				UILayerConfig uiLayerConfig = UILayerConst.uiLayerConfig_dict.Values[i];
				string name = uiLayerConfig.name.ToString();
				Transform layer_transform = uiManager.graphicComponent.transform.Find("UICanvas/" + name);
				GameObject layer_gameObject = layer_transform != null ? layer_transform.gameObject : new GameObject(name);
				layer_gameObject.transform.SetParent(uiManager.uiCanvas.transform);
				layer_gameObject.transform.SetSiblingIndex(i);
				var layer = this.AddChild<UILayer>(null, layer_gameObject, uiLayerConfig);
				uiLayer_dict[uiLayerConfig.name] = layer;
			}

			this.AddListener<EUILayerName, bool>(null, UIEventNameConst.SetIsHideUILayer, SetIsHideUILayer);
		}

		//供lua端调用，不要删除
		public UILayer GetUILayer(string uiLayer_name)
		{
			EUILayerName eUILayerName = uiLayer_name.ToEnum<EUILayerName>();
			return GetUILayer(eUILayerName);
		}

		public UILayer GetUILayer(EUILayerName eUILayerName)
		{
			return uiLayer_dict[eUILayerName];
		}

		void SetIsHideUILayer(EUILayerName eUILayerName, bool is_hide)
		{
			GetUILayer(eUILayerName).graphicComponent.SetIsShow(!is_hide);
		}
	}
}