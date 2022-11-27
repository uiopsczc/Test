using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class UILayerManager : UIObject
	{
		public Dictionary<EUILayerName, UILayer> uiLayerDict = new Dictionary<EUILayerName, UILayer>();

		public UIManager uiManager;

		protected void _Init(UIManager uiManager)
		{
			base._Init();
			this.uiManager = uiManager;
			for (int i = 0; i < UILayerConst.uiLayerConfigDict.Values.Count; i++)
			{
				UILayerConfig uiLayerConfig = UILayerConst.uiLayerConfigDict.Values[i];
				string name = uiLayerConfig.name.ToString();
				Transform layerTransform = uiManager.GetTransform().Find("UICanvas/" + name);
				GameObject layerGameObject = layerTransform != null ? layerTransform.gameObject : new GameObject(name);
				layerGameObject.transform.SetParent(uiManager.uiCanvas.transform);
				layerGameObject.transform.SetSiblingIndex(i);
				var layer = this.AddChild<UILayer>(null, layerGameObject, uiLayerConfig);
				uiLayerDict[uiLayerConfig.name] = layer;
			}

			AddListener<EUILayerName, bool>(null, UIEventNameConst.SetIsHideUILayer, SetIsHideUILayer);
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
			GetUILayer(eUILayerName).SetIsShow(!isHide);
		}
	}
}