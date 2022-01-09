using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject
	{
		public Dictionary<string, UIPanel> childPanelDict = new Dictionary<string, UIPanel>();

		public T __CreateChildPanel<T>(string key, T t, Transform parentTransform,
		  Action<UIPanel> initCallback) where T : UIPanel, new()
		{
			T childPanel = default(T);
			if (key != null)
				childPanel = this.GetChild<T>(key);
			if (childPanel != null)
				return childPanel;
			childPanel = this.AddChildWithoutInit<T>(key);
			initCallback(childPanel);
			childPanel.OnInitPanel(parentTransform);
			childPanel.PostInit();
			childPanel.SetIsEnabled(true, false);
			childPanelDict[childPanel.key] = childPanel;
			return childPanel;
		}

		public virtual T CreateChildPanel<T>(string key, T t, Transform parentTransform = null,
		   params object[] args) where T : UIPanel, new()
		{
			return this.__CreateChildPanel(key, t, parentTransform,
			  (childPanel) => childPanel.InvokeMethod("Init", false, args));
		}

		public UIPanel GetChildPanel(string key)
		{
			return this.GetChild<UIPanel>(key);
		}

		public T GetChildPanel<T>(string key) where T : UIPanel
		{
			return GetChildPanel(key) as T;
		}


		// 从Panle中Close，再调到这里来，不要直接使用这个
		public void CloseChildPanel(string key)
		{
			var childPanel = GetChildPanel(key);
			if (childPanel == null)
				return;
			if (childPanelDict.ContainsKey(key))
				childPanelDict.Remove(key);
			this.RemoveChild(key);
		}

		public void CloseAllChildPanels(bool isRemainResidentPanels = false)
		{
			List<string> panelNameList = new List<string>(childPanelDict.Keys);
			for (var i = 0; i < panelNameList.Count; i++)
			{
				string panelName = panelNameList[i];
				UIPanel childPanel = this.childPanelDict[panelName];
				if (!childPanel.isResident || !isRemainResidentPanels) childPanel.Close();
			}
		}
	}
}



