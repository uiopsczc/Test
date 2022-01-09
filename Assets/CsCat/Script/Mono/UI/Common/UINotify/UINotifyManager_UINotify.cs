using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UINotifyManager
	{
		private Dictionary<string, bool> uiNotifyPanelDict = new Dictionary<string, bool>();

		public void Notify(string desc, Transform parent = null, bool isAddToChildPanelStack = false)
		{
			PreNotify();
			UINotifyPanel panel = Client.instance.uiManager.CreateChildPanel(null, default(UINotifyPanel), parent,
			  isAddToChildPanelStack, desc);
			uiNotifyPanelDict[panel.key] = true;
			panel.destroyCallback += () => { uiNotifyPanelDict.Remove(panel.key); };
		}

		public void PreNotify()
		{
			foreach (string key in uiNotifyPanelDict.Keys)
			{
				UINotifyPanel panel = Client.instance.uiManager.GetChildPanel<UINotifyPanel>(key);
				panel.Rise();
			}
		}

	}
}




