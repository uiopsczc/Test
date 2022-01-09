using System.Collections.Generic;
using UnityEngine;
using RectTransform = UnityEngine.RectTransform;

namespace CsCat
{
	public class UILayerManager : UIObject
	{
		public Dictionary<EUILayerName, UILayer> uiLayerDict = new Dictionary<EUILayerName, UILayer>();

		public UIManager uiManager;

		public void Init(UIManager uiManager)
		{
			base.Init();
			this.uiManager = uiManager;
			for (int i = 0; i < UILayerConst.uiLayerConfigDict.Values.Count; i++)
			{
				UILayerConfig uiLayerConfig = UILayerConst.uiLayerConfigDict.Values[i];
				string name = uiLayerConfig.name.ToString();
				Transform layerTransform = uiManager.graphicComponent.transform.Find("UICanvas/" + name);
				GameObject layerGameObject = layerTransform != null ? layerTransform.gameObject : new GameObject(name);
				layerGameObject.transform.SetParent(uiManager.uiCanvas.transform);
				layerGameObject.transform.SetSiblingIndex(i);
				var layer = this.AddChild<UILayer>(null, layerGameObject, uiLayerConfig);
				uiLayerDict[uiLayerConfig.name] = layer;
			}

			this.AddListener<EUILayerName, bool>(null, UIEventNameConst.SetIsHideUILayer, SetIsHideUILayer);
		}

		//供lua端调用，不要删除
		public UILayer GetUILayer(string uiLayerName)
		{
			EUILayerName eUILayerName = uiLayerName.ToEnum<EUILayerName>();
			return GetUILayer(eUILayerName);
		}

		public UILayer GetUILayer(EUILayerName eUILayerName)
		{
			return uiLayerDict[eUILayerName];
		}

		void SetIsHideUILayer(EUILayerName eUILayerName, bool isHide)
		{
			GetUILayer(eUILayerName).graphicComponent.SetIsShow(!isHide);
		}
	}
}