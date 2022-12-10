using System.Collections.Generic;
using UnityEngine;
using XLua;
using RectTransform = UnityEngine.RectTransform;

namespace CsCat
{
	public class  UILayer : UIObject
	{
		public UILayerConfig uiLayerConfig;
		public List<object> panelList = new List<object>();

		protected void _Init(GameObject gameObject, UILayerConfig uiLayerConfig)
		{
			base._Init();
			this.uiLayerConfig = uiLayerConfig;
			DoSetGameObject(gameObject);
			gameObject.name = uiLayerConfig.name.ToString();
			gameObject.layer = LayerMask.NameToLayer("UI");

			RectTransform rectTransform = this.GetGameObject().GetOrAddComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.anchorMax = new Vector2(1, 1);
			rectTransform.sizeDelta = new Vector2(0, 0);
			rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
			rectTransform.localScale = new Vector3(1, 1, 1);
		}


		protected override bool _Refresh(bool isInit = false)
		{
			if (!base._Refresh(isInit))
				return false;
			for (var i = 0; i < panelList.Count; i++)
			{
				object panel = panelList[i];
				int sortingOrder = uiLayerConfig.orderInLayer + (i + 1) * UILayerConst.Order_Per_Panel;

				if (panel is UIPanel uiPanel)
				{
					uiPanel.sortingOrder = sortingOrder;

					if (uiLayerConfig.uiLayerRule.IsHideLowerOrderUI())
						uiPanel.SetIsShow(i == panelList.Count - 1);
				}
				else //lua UIPanel
				{
					LuaTable panelLuaTable = (LuaTable) panel;
					panelLuaTable.InvokeAction("SetSortingOrder", sortingOrder);

					if (uiLayerConfig.uiLayerRule.IsHideLowerOrderUI())
						panelLuaTable.InvokeAction("SetIsShow", i == panelList.Count - 1);
				}
			}


			if (uiLayerConfig.uiLayerRule.IsHideBackgroundUILayer())
				this.FireEvent(null, UIEventNameConst.SetIsHideUILayer, EUILayerName.BackgroundUILayer,
					panelList.Count > 0);
			if (uiLayerConfig.uiLayerRule.IsHideFrontUILayer())
				this.FireEvent(null, UIEventNameConst.SetIsHideUILayer, EUILayerName.FrontUILayer, panelList.Count > 0);
			if (uiLayerConfig.uiLayerRule.IsAddBlackMaskBehind())
				_HandleLayerAddBlackMaskBehind();
			return true;
		}

		public void RemovePanel(object panel)
		{
			var index = this.panelList.IndexOf(panel);
			if (index == -1)
				return;
			this.panelList.RemoveAt(index);
			Refresh();
		}

		public void AddPanel(object panel)
		{
			panelList.Add(panel);
			Refresh();
		}

		public void SetPanelIndex(object panel, int newIndex)
		{
			var index = this.panelList.IndexOf(panel);
			if (index == -1 || index == newIndex)
				return;
			this.panelList.Insert(newIndex, panel);
			if (index < newIndex)
				this.panelList.RemoveAt(index);
			else
				this.panelList.RemoveAt(index + 1);
			Refresh();
		}

		public void SetPanelToTop(object panel)
		{
			var topIndex = panelList.Count;
			SetPanelIndex(panel, topIndex);
		}

		public void SetPanelToBottom(object panel)
		{
			SetPanelIndex(panel, 0);
		}


		void _HandleLayerAddBlackMaskBehind()
		{
			object targetPanel = null;
			int targetPanelSortingOrder = 0;
			for (int i = EnumUtil.GetCount<EUILayerName>() - 1; i >= 0; i--)
			{
				EUILayerName uiLayerName = (EUILayerName) i;
				var uiLayer = Client.instance.uiManager.uiLayerManager.GetUILayer(uiLayerName);
				if (uiLayer.IsShow() && uiLayer.uiLayerConfig.uiLayerRule.IsAddBlackMaskBehind())
				{
					for (int j = uiLayer.panelList.Count - 1; j >= 0; j--)
					{
						var panel = uiLayer.panelList[j];
						if (panel is UIPanel uiPanel)
						{
							if (!uiPanel.isHideBlackMaskBehind)
							{
								targetPanel = uiPanel;
								targetPanelSortingOrder = uiPanel.sortingOrder;
								break;
							}
						}
						else
						{
							LuaTable panelLuaTable = (LuaTable) panel;
							if (!panelLuaTable.InvokeFunc<bool>("_IsHideBlackMaskBehind"))
							{
								targetPanel = panel;
								targetPanelSortingOrder = panelLuaTable.InvokeFunc<int>("GetSortingOrder");
								break;
							}
						}
					}
				}
			}

			if (targetPanel == null)
				this.FireEvent(null, UIEventNameConst.HideUIBlackMask);
			else
				this.FireEvent(null, UIEventNameConst.ShowUIBlackMask, targetPanelSortingOrder, targetPanel);
		}

		protected override void _DestroyGameObject()
		{
		}
	}
}