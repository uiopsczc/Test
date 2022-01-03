using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UINotifyManager
	{
		private Dictionary<string, bool> uiNotifyPanel_dict = new Dictionary<string, bool>();

		public void Notify(string desc, Transform parent = null, bool is_add_to_child_panel_stack = false)
		{
			PreNotify();
			UINotifyPanel panel = Client.instance.uiManager.CreateChildPanel(null, default(UINotifyPanel), parent,
			  is_add_to_child_panel_stack, desc);
			uiNotifyPanel_dict[panel.key] = true;
			panel.destroyCallback += () => { uiNotifyPanel_dict.Remove(panel.key); };
		}

		public void PreNotify()
		{
			foreach (string key in uiNotifyPanel_dict.Keys)
			{
				UINotifyPanel panel = Client.instance.uiManager.GetChildPanel<UINotifyPanel>(key);
				panel.Rise();
			}
		}

	}
}




