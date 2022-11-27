using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UINotifyManager
	{
		private readonly Dictionary<string, bool> _uiNotifyPanelDict = new Dictionary<string, bool>();

		public void Notify(string desc, Transform parent = null, bool isAddToChildPanelStack = false)
		{
			PreNotify();
			UINotifyPanel panel = Client.instance.uiManager.CreateChildPanel(null, default(UINotifyPanel), parent,
			  isAddToChildPanelStack, desc);
			_uiNotifyPanelDict[panel.GetId()] = true;
			panel.postDestroyCallback += () => { _uiNotifyPanelDict.Remove(panel.GetId()); };
		}

		public void PreNotify()
		{
			foreach (var kv in _uiNotifyPanelDict)
			{
				var key = kv.Key;
				UINotifyPanel panel = Client.instance.uiManager.GetChildPanel<UINotifyPanel>(key);
				panel.Rise();
			}
		}

	}
}




