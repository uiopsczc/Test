using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject
	{
		private readonly Dictionary<string, UIPanel> _childPanelDict = new Dictionary<string, UIPanel>();

		public T _CreateChildPanel<T>(string key, T t,
			params object[] args) where T : UIPanel, new()
		{
			T childPanel = default(T);
			if (key != null)
				childPanel = this.GetChild<T>(key);
			if (childPanel != null)
				return childPanel;
			childPanel = this.AddChildWithoutInit<T>(key);
			if (childPanel == null) //没有加成功
				return null;
			childPanel.DoInit(args);
			_childPanelDict[childPanel.GetKey()] = childPanel;
			return childPanel;
		}

		public virtual T CreateChildPanel<T>(string key, T t,
		   params object[] args) where T : UIPanel, new()
		{
			return this._CreateChildPanel(key, t, args);
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
			if (childPanel.isResident)
				return;
			if (_childPanelDict.ContainsKey(key))
				_childPanelDict.Remove(key);
			this.RemoveChild(key);
		}

		public void CloseAllChildPanels()
		{
			string[] panelNames = _childPanelDict.Keys.ToArray();
			for (var i = 0; i < panelNames.Length; i++)
			{
				string panelName = panelNames[i];
				this.CloseChildPanel(panelName);
			}
		}
	}
}



