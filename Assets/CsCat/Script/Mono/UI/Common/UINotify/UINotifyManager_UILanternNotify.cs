using System.Collections.Generic;

namespace CsCat
{
	public partial class UINotifyManager
	{
		//走马灯效果
		private List<string> lanternNotifyDescCacheList = new List<string>();

		public void LanternNotify(string desc)
		{
			lanternNotifyDescCacheList.Add(desc);
			if (!Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel").graphicComponent.gameObject
			  .activeInHierarchy)
				__LanternNotify();
		}

		public void __LanternNotify()
		{
			if (lanternNotifyDescCacheList.Count > 0)
			{
				string desc = lanternNotifyDescCacheList.RemoveFirst<string>();
				UILanternNotifyPanel panel =
				  Client.instance.uiManager.GetChildPanel<UILanternNotifyPanel>("UILanternNotifyPanel");
				panel.Show(desc);
			}
		}


	}
}




