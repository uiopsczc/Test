using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject
	{
		public Dictionary<string, UIPanel> child_panel_dict = new Dictionary<string, UIPanel>();

		public T __CreateChildPanel<T>(string key, T t, Transform parent_transform,
		  Action<UIPanel> init_callback) where T : UIPanel, new()
		{
			T child_panel = default(T);
			if (key != null)
				child_panel = this.GetChild<T>(key);
			if (child_panel != null)
				return child_panel;
			child_panel = this.AddChildWithoutInit<T>(key);
			init_callback(child_panel);
			child_panel.OnInitPanel(parent_transform);
			child_panel.PostInit();
			child_panel.SetIsEnabled(true, false);
			child_panel_dict[child_panel.key] = child_panel;
			return child_panel;
		}

		public virtual T CreateChildPanel<T>(string key, T t, Transform parent_transform = null,
		   params object[] args) where T : UIPanel, new()
		{
			return this.__CreateChildPanel(key, t, parent_transform,
			  (child_panel) => child_panel.InvokeMethod("Init", false, args));
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
			var child_panel = GetChildPanel(key);
			if (child_panel == null)
				return;
			if (child_panel_dict.ContainsKey(key))
				child_panel_dict.Remove(key);
			this.RemoveChild(key);
		}

		public void CloseAllChildPanels(bool is_reamin_resident_panels = false)
		{
			List<string> panel_name_list = new List<string>(child_panel_dict.Keys);
			foreach (string panel_name in panel_name_list)
			{
				UIPanel child_panel = this.child_panel_dict[panel_name];
				if (!child_panel.is_resident || !is_reamin_resident_panels)
				{
					child_panel.Close();
				}
			}
		}
	}
}



